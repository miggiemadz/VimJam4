using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooler : MonoBehaviour
{
    public GameObject itemPrefab;

    public ushort count = 6;
    public float cooldown = 1.0f;
    float? cooldownTimer = null;

    void FixedUpdate()
    {
        if (count > 0)
        {
            if (cooldownTimer > 0.0f)
            {
                cooldownTimer -= Time.fixedDeltaTime;
            }
            else if (cooldownTimer != null)
            {
                SpawnItem();
            }
            else
            {
                if (!CheckNearbyItem())
                {
                    cooldownTimer = cooldown;
                }
            }
        }
    }

    void SpawnItem()
    {
        GameObject newItem = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        FloorItem newFloorItem = newItem.GetComponent<FloorItem>();
        newFloorItem.itemType = ItemType.Bottle;
        count--;
        cooldownTimer = null;
    }

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
