using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{

    [HideInInspector]
    public float boomBoxDistance;
    [Header("Player Settings")]
    public float moveSpeed;
    public float pickUpDistance = 1.0f;
    [Header("Objects in the world (set in each scene)")]
    public Camera mainCamera;
    public GameObject cursor;
    public BoomBox boomBox;
    public MusicBar musicBar;
    public Image heldItemIndicator;
    [HideInInspector]
    public Rigidbody2D rb;
    [Header("Objects related to the player (set in prefab)")]
    public Animator animator;
    public Slider healthBar;
    public GameObject thrownItemPrefab;

    private Item? item;

    public float arrestTimer {
        get { return healthBar.value; }
        set {
            healthBar.value = value; 
            healthBar.gameObject.SetActive(value < 2);
        }
    }

    public Item Item { 
        get => item;
        set 
        { 
            item = value;
            if (value == null)
            {
                heldItemIndicator.gameObject.SetActive(false);
            } else
            {
                heldItemIndicator.gameObject.SetActive(true);
                heldItemIndicator.sprite = Item.GetSprite(value.type);
            }
        }
    }

    Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Item = new Item(ItemType.ExplodingCat);
    }


    void Update()
    {
        boomBoxDistance = Vector2.Distance(transform.position, boomBox.transform.position);

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
            if (Item == null)
            {
                Collider2D[] itemsNearby = Physics2D.OverlapCircleAll(transform.position, pickUpDistance);
                //Debug.Log(itemsNearby.Length);
                foreach (Collider2D collider in itemsNearby)
                {
                    if (collider.gameObject.TryGetComponent<FloorItem>(out var floorItem))
                    {
                        Item = floorItem.item;
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
                thrownItemScript.item = Item;
                Item = null;
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
        {
            if (Input.GetKey(KeyCode.E))
            {
                boomBox.boomboxOn = true;
            }
        }
    }


}
