using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoliceBehavior : MonoBehaviour
{
    public PlayerBehavior player;
    public BoomBox boomBox;
    public Animator animator;
    public GameObject liquidPool;
    public EnemySpawner spawner;

    public float enemySpeed;

    private float playerDistance;
    private float boomBoxDistance;
    private float liquidPoolDistance;

    public float sightRange;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        liquidPoolDistance = Vector2.Distance(transform.position, liquidPool.transform.position);

        playerDistance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 playerDirection = player.transform.position - transform.position;
        playerDirection.Normalize();
        float playerAngle = Mathf.Atan2(playerDirection.x, playerDirection.y) * Mathf.Rad2Deg;

        boomBoxDistance = Vector2.Distance(transform.position, boomBox.transform.position);
        Vector2 boomBoxDirection = boomBox.transform.position - transform.position;
        boomBoxDirection.Normalize();
        float boomBoxAngle = Mathf.Atan2(boomBoxDirection.x, boomBoxDirection.y) * Mathf.Rad2Deg;

        Vector3 finalPosition;
        if (boomBox.boomboxOn)
        {
            finalPosition = Vector2.MoveTowards(this.transform.position, boomBox.transform.position, enemySpeed * Time.fixedDeltaTime);
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
            boomBox.boomBoxHealth -= Time.fixedDeltaTime;
        }
    }

    void OnDestroy()
    {
        spawner.enemiesInGame--;
    }
}
