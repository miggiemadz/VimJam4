using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] public BoomBox boomBox;

    public Rigidbody2D rb;

    public float boomBoxDistance;

    public float moveSpeed;
    public float pickUpDistance = 1.0f;

    public GameObject cursor;
    public Camera mainCamera;
    public GameObject thrownItemPrefab;

    public Item? item = new Item(ItemType.ExplodingCat);

    Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        boomBoxDistance = Vector2.Distance(rb.position, boomBox.transform.position);

        // get input axis for player movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        SceneChanger();

        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        cursor.transform.position = mousePos;

        if (Input.GetButtonDown("Fire1"))
        {
            if (item == null)
            {
                Collider2D[] itemsNearby = Physics2D.OverlapCircleAll(transform.position, pickUpDistance);
                Debug.Log(itemsNearby.Length);
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
        movement.Normalize();
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        boomBoxOnandOff();
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
        if (boomBoxDistance <= 1)
        {
            if (boomBox.boomboxOn == false)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    boomBox.boomboxOn = true;
                }
            }

        }
    }
}
