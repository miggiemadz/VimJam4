using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorItem : MonoBehaviour
{
    public ItemType itemType;
    public Item item;
    public static float tempo = 80.0f;
    float timer = 0.0f;

    void Start() {
        if (item == null) {
            item = new Item(itemType);
        }
        if (item.type == ItemType.BoomBox) {
            Debug.Log("BoomBox Placed");
        }
    }

    void FixedUpdate() {
        if (item.type == ItemType.BoomBox) {
            timer += Time.fixedDeltaTime * tempo / 60;
            if (timer >= 1.0f) {
                Debug.Log("BOOM");
                transform.localScale = Vector3.one * 1.1f;
                timer = 0.0f;
            } else if (timer >= 0.25f) {
                transform.localScale = Vector3.one * 1.0f;
            }
        }
    }

    void OnDestroy() {
        if (item.type == ItemType.BoomBox) {
            Debug.Log("BoomBox Destroyed");
        }
    }
}
