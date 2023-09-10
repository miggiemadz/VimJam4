using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoliceBehavior : MonoBehaviour
{
    public PlayerBehavior player;
    public GameObject boomBox;

    public float enemySpeed;

    private float playerDistance;
    private float boomBoxDistance;

    float boomBoxDestroyTimer = 2;
    float playerArrestTimer = 2;

    public float sightRange;

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 playerDirection = player.transform.position - transform.position;
        playerDirection.Normalize();
        float playerAngle = Mathf.Atan2(playerDirection.x, playerDirection.y) * Mathf.Rad2Deg;

        boomBoxDistance = Vector2.Distance(transform.position, boomBox.transform.position);
        Vector2 boomBoxDirection = boomBox.transform.position - transform.position;
        boomBoxDirection.Normalize();
        float boomBoxAngle = Mathf.Atan2(boomBoxDirection.x, boomBoxDirection.y) * Mathf.Rad2Deg;

        if (player.boomBoxOn)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, boomBox.transform.position, enemySpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, enemySpeed * Time.deltaTime);
        }
      
        if (boomBoxDistance == 0)
        {
            boomBoxDestroyTimer -= Time.fixedDeltaTime; 
            
            if(boomBoxDestroyTimer <= 0)
            {
                boomBox.SetActive(false);
                player.boomBoxOn = false;
            }
        }

        if (playerDistance <= 0.5)
        {
            playerArrestTimer -= Time.fixedDeltaTime;

            if (playerArrestTimer <= 0)
            {
                player.isArrested = true;
                playerArrestTimer = 2;
            }
        } else if (playerArrestTimer != 2)
        {
            playerArrestTimer = 2;
        }
    }
}
