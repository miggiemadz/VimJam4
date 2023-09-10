using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownItem : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public GameObject floorItemPrefab;
    public GameObject explosionPrefab;

    public Item item;

    public Vector3 direction;

    public float speed = 25.0f;

    private float distance;

    private float arc;

    // Start is called before the first frame update
    void Start()
    {
        distance = direction.magnitude;
        direction.Normalize();
        spriteRenderer.sprite = Item.GetSprite(item.type);
        //Debug.Log(spriteRenderer.sprite);
    }

    void FixedUpdate()
    {
        if (arc >= 2)
        {
            Land();
        }
        else
        {
            transform.position += direction * Time.fixedDeltaTime * speed;
            arc += Time.fixedDeltaTime / distance * speed * 2;
            transform.localScale = Vector3.one * (3 - 2 * Mathf.Pow(arc - 1, 2));
            if (item.type == ItemType.ExplodingCat)
            {
                transform.Rotate(new Vector3(0, 0, 20));
            }
        }
    }

    void Land()
    {
        if (item.type == ItemType.ExplodingCat)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        } else
        {
            GameObject floorItem = Instantiate(floorItemPrefab, transform.position, Quaternion.identity);
            floorItem.GetComponent<FloorItem>().item = item;
        }
        Destroy(gameObject);
    }
}
