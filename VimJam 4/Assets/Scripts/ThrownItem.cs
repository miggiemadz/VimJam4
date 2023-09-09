using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownItem : MonoBehaviour
{
    public Item item;

    public Vector3 direction;

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
            Destroy(gameObject);
        } else {
            transform.position += direction * Time.fixedDeltaTime * 5;
            arc += Time.fixedDeltaTime / distance * 10;
            transform.localScale = Vector3.one * (2 - Mathf.Pow(arc - 1, 2));
        }
    }
}
