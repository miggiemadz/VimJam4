using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooler : MonoBehaviour
{
    // Floor item prefab
    public GameObject itemPrefab;
    // Starting number of cans & bottles
    public ushort count = 6;
    // Time from an item being picked up to the next one being spawned
    public float cooldown = 1.0f;
    // cooldown timer. Null if there's an item near the cooler
    [HideInInspector]
    float? cooldownTimer = null;

    void FixedUpdate()
    {
        // don't do anything if the cooler's empty
        if (count > 0)
        {
            if (cooldownTimer > 0.0f)
            {
                // if we're waiting, tick down the timer
                cooldownTimer -= Time.fixedDeltaTime;
            }
            else if (cooldownTimer != null)
            {
                // if the timer is at zero, spawn an item
                SpawnItem();
            }
            else
            {
                // if the timer isn't set but there aren't any items nearby, start the timer
                if (!CheckNearbyItem())
                {
                    cooldownTimer = cooldown;
                }
            }
        }
    }

    // spawn a new item, decrement the count, and reset the cooldown timer
    void SpawnItem()
    {
        GameObject newItem = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        FloorItem newFloorItem = newItem.GetComponent<FloorItem>();
        // choose a random item type
        if ( Random.value > 0.5)
        {
            newFloorItem.itemType = ItemType.Can;
        } else
        {
            newFloorItem.itemType = ItemType.Bottle;
        }
        count--;
        cooldownTimer = null;
    }

    // Check if there's a floor item nearby.
    bool CheckNearbyItem()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2.0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent<FloorItem>(out var _))
            {
                return true;
            }
        }
        return false;
    }
}
