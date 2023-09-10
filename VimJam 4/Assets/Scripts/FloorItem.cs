using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorItem : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public ItemType itemType;
    public Item item;

    void Start()
    {
        if (item == null)
        {
            item = new Item(itemType);
        }

        spriteRenderer.sprite = Item.GetSprite(item.type);

        if (itemType == ItemType.ExplodingCat)
        {
            gameObject.GetComponent<ExplodingCat>().enabled = true;
        }
    }
}
