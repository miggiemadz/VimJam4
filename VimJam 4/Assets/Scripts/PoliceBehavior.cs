using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoliceBehavior : MonoBehaviour
{
    public PlayerBehavior player;
    public BoomBox BoomBox;
    public GameObject boomBoxPrefab;
    public Animator animator;

    public float enemySpeed;

    private float playerDistance;
    private float boomBoxDistance;

    float boomBoxDestroyTimer = 2;
    float playerArrestTimer = 2;

    public float sightRange;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        playerDistance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 playerDirection = player.transform.position - transform.position;
        playerDirection.Normalize();
        float playerAngle = Mathf.Atan2(playerDirection.x, playerDirection.y) * Mathf.Rad2Deg;

        boomBoxDistance = Vector2.Distance(transform.position, boomBoxPrefab.transform.position);
        Vector2 boomBoxDirection = boomBoxPrefab.transform.position - transform.position;
        boomBoxDirection.Normalize();
        float boomBoxAngle = Mathf.Atan2(boomBoxDirection.x, boomBoxDirection.y) * Mathf.Rad2Deg;



        Vector3 finalPosition;
        if (BoomBox.boomboxOn)
        {
            finalPosition = Vector2.MoveTowards(this.transform.position, boomBoxPrefab.transform.position, enemySpeed * Time.fixedDeltaTime);
        }
        else
        {
            finalPosition = Vector2.MoveTowards(this.transform.position, player.transform.position, enemySpeed * Time.fixedDeltaTime);
        }
        Vector2 motion = finalPosition - transform.position;
        // send motion data to animator
        transform.position = finalPosition;

        if (motion.x < 0)
        {
            gameObject.transform.localScale = new Vector3(0.104f, 0.09f, 1f);
        }
        if (motion.x > 0)
        {
            gameObject.transform.localScale = new Vector3(-0.104f, 0.09f, 1f);
        }

        if (boomBoxDistance == 0)
        {
            boomBoxDestroyTimer -= Time.fixedDeltaTime; 
            
            if(boomBoxDestroyTimer <= 0)
            {
                boomBoxPrefab.SetActive(false);
                BoomBox.boomboxOn = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LiquidPool"))
        {
            enemySpeed *= .5f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemySpeed = 4f;
    }
}
