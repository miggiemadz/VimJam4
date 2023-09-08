using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public Rigidbody2D rb;

    public float moveSpeed;

    Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        // get input axis for player movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        movement.Normalize();
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }
}
