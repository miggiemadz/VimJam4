using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;

    public float boomBoxDistance;
    public GameObject boomBox;

    public float moveSpeed;
    public float pickUpDistance = 1.0f;

    public GameObject cursor;
    public Camera mainCamera;
    public GameObject thrownItemPrefab;

    public bool boomBoxOn;
    public bool isArrested;

    public Item? item = new(ItemType.ExplodingCat);

    Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boomBoxOn = false;
    }


    void Update()
    {
        // if the game is paused, stop everything
        if (Time.timeScale == 0.0f)
        {
            return;
        }
        animator.SetFloat("verticalMovment", movement.y);
        animator.SetFloat("horizontalMovement", Mathf.Abs(movement.x));

        // get input axis for player movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x < 0)
        {
            gameObject.transform.localScale = new Vector3(0.0858f, 0.0858f, 0.2145f);
        }
        if (movement.x > 0)
        {
            gameObject.transform.localScale = new Vector3(-0.0858f, 0.0858f, 0.2145f);
        }

        SceneChanger();

        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        cursor.transform.position = mousePos;

        if (isArrested)
        {
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (item == null)
            {
                Collider2D[] itemsNearby = Physics2D.OverlapCircleAll(transform.position, pickUpDistance);
                //Debug.Log(itemsNearby.Length);
                foreach (Collider2D collider in itemsNearby)
                {
                    if (collider.gameObject.TryGetComponent<FloorItem>(out var floorItem))
                    {
                        item = floorItem.item;
                        Destroy(collider.gameObject);
                        break;
                    }
                }
            }
            else
            {
                Vector3 thrownItemDirection = cursor.transform.position - transform.position;
                GameObject thrownItem = Instantiate(thrownItemPrefab, transform.position, Quaternion.identity);
                ThrownItem thrownItemScript = thrownItem.GetComponent<ThrownItem>();
                thrownItemScript.item = item;
                item = null;
                thrownItemScript.direction = thrownItemDirection;
            }
        }
    }

    void FixedUpdate()
    {
        if (isArrested)
        {
            return;
        }
        movement.Normalize();
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        boomBoxOnandOff();
        Debug.Log(boomBoxOn);
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

    private void boomBoxOnandOff()
    {
        if (boomBoxOn == false)
        {
            if (Vector2.Distance(transform.position, boomBox.transform.position) <= 2)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    boomBoxOn = true;
                }
            }
        }
    }
}
