using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    public Rigidbody2D rb;

    public float moveSpeed;

    public GameObject cursor;

    public Camera mainCamera;

    public GameObject thrownItemPrefab;

    public Item? item = new Item(ItemType.Rock);

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

        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        cursor.transform.position = mousePos;

        if (Input.GetButtonDown("Fire1")) {
            if (item == null) {
                Collider[] itemsNearby = Physics.OverlapSphere(transform.position, 10.0f, ~0);
                Debug.Log(itemsNearby.Length);
                foreach(Collider collider in itemsNearby) {
                    if (collider.gameObject.TryGetComponent<FloorItem>(out var floorItem)) {
                        item = floorItem.item;
                        Destroy(collider.gameObject);
                        break;
                    }
                }
            } else {
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
