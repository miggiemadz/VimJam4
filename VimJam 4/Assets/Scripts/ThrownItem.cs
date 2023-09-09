using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownItem : MonoBehaviour
{
    public GameObject floorItemPrefab;

    public Item item;

    public Vector3 direction;

    public float speed = 25.0f;

    private float distance;

    private float arc;

    // Start is called before the first frame update
    void Start()
    {
        distance = direction.magnitude;
        direction = direction.normalized;
    }

    void FixedUpdate()
    {
        if (arc >= 2) {
            Land();
        } else {
            transform.position += direction * Time.fixedDeltaTime * speed;
            arc += Time.fixedDeltaTime / distance * speed * 2;
            transform.localScale = Vector3.one * (2 - Mathf.Pow(arc - 1, 2));
        }
    }

    void Land() {
        GameObject floorItem = Instantiate(floorItemPrefab, transform.position, Quaternion.identity);
        floorItem.GetComponent<FloorItem>().item = item;
        Destroy(gameObject);
    }
}
