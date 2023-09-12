using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;

    public float boomBoxDistance;
    public BoomBox boomBox;

    public float moveSpeed;
    public float pickUpDistance = 1.0f;

    public GameObject cursor;
    public Camera mainCamera;
    public GameObject thrownItemPrefab;

    public bool isArrested;
    public MusicBar musicBar;
    public Slider healthBar;

    public Item? item = new(ItemType.ExplodingCat);

    public float arrestTimer {
        get { return healthBar.value; }
        set {
            healthBar.value = value; 
            healthBar.gameObject.SetActive(value < 2);
        }
    }

    Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        boomBoxDistance = Vector2.Distance(transform.position, boomBox.transform.position);

        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        cursor.transform.position = mousePos;
        // if the game is paused, stop everything
        if (Time.timeScale == 0.0f)
        {
            return;
        }
        SceneChanger();

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
        boomBoxOnandOff();
    }

    void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.0f);
        bool isArrested = false;
        foreach(Collider2D collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<PoliceBehavior>(out var policeBehavior))
            {
                arrestTimer -= Time.fixedDeltaTime;
                isArrested = true;
            }
        }
        if (isArrested)
        {
            if (arrestTimer <= 0)
            {
                // TODO: Explode on death?
                // revert the music by 10s
                musicBar.currentMusicValue -= 10;
                isArrested = false;
                arrestTimer = 2f;
                return;
            }
        } else
        {
            arrestTimer += 2 * Time.fixedDeltaTime;
        }
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("BoomBox"))
        if (!boomBox.boomboxOn && boomBox.boomBoxHealth == 5)
        {
            if (Input.GetKey(KeyCode.E))
            {
                boomBox.boomboxOn = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    boomBox.boomboxOn = true;
                }
            }
        }
    }


}
