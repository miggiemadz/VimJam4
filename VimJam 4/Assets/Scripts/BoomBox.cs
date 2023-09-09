using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBox : MonoBehaviour
{
    public static float tempo = 80.0f;
    float timer = 0.0f;

    void Start()
    {
        Debug.Log("BoomBox Placed");
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime * tempo / 60;
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
        float sound_radius = tempo * timer / 10;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, sound_radius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<PoliceBehavior>(out var cop))
            {
                Debug.Log("Found a cop!");
            }
        }
    }

    void OnDestroy()
    {
        Debug.Log("BoomBox Destroyed");
    }
}
