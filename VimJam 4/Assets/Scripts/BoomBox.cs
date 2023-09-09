using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBox : MonoBehaviour
{
    public GameObject soundWavePrefab;
    public float tempo = 80.0f;
    public float repulsiveForce = 0.1f;
    float timer = 0.0f;
    GameObject soundWave;

    void Start()
    {
        Debug.Log("BoomBox Placed");
        soundWave = Instantiate(soundWavePrefab, transform.position, Quaternion.identity);
        soundWave.transform.localScale = Vector3.zero;
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime * tempo / 60;
        soundWave.transform.localScale = Vector3.one * timer * 50;
        if (timer >= 1.0f)
        {
            Debug.Log("BOOM");
            transform.localScale = Vector3.one * 1.1f;
            timer = 0.0f;
        }
        else if (timer >= 0.25f)
        {
            transform.localScale = Vector3.one * 1.0f;
        }
        float sound_radius = tempo * timer / 20;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, sound_radius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<PoliceBehavior>(out var cop))
            {
                Debug.Log("Found a cop!");
                // this is zero when the cop is directly on the boombox
                Vector3 copPosRelative = collider.transform.position - transform.position;
                // cop moves more when closer to boombox, so movement is divided by magnitude
                Vector3 copMovement = copPosRelative.normalized / copPosRelative.magnitude * repulsiveForce;
                collider.gameObject.GetComponent<Rigidbody2D>().MovePosition(collider.transform.position + copMovement);
            }
        }
    }

    void OnDestroy()
    {
        Destroy(soundWave);
        Debug.Log("BoomBox Destroyed");
    }
}
