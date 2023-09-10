using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceBehavior : MonoBehaviour
{
    public GameObject player;
    public BoomBox boomBox;

    public float enemySpeed;

    private float playerDistance;
    private float boomBoxDistance;

    public float sightRange;


    // Start is called before the first frame update
    void Start()
    {
        
    }

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

        if (boomBox.boomboxOn)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, boomBox.transform.position, enemySpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, enemySpeed * Time.deltaTime);
        }


    }


}
