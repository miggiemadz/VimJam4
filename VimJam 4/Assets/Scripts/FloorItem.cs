using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorItem : MonoBehaviour
{
    public ItemType itemType;
    public Item item;

    void Start()
    {
        if (item == null)
        {
            item = new Item(itemType);
        }

        if (itemType == ItemType.ExplodingCat) {
            gameObject.GetComponent<ExplodingCat>().enabled = true;
        }
    }
}
