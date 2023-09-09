using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

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

        SceneChanger();
    }

    void FixedUpdate()
    {
        movement.Normalize();
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    public void SceneChanger()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("World1"))
            {
                SceneManager.LoadScene(1);
            }
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("World2"))
            {
                SceneManager.LoadScene(2);
            }
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("World3"))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
