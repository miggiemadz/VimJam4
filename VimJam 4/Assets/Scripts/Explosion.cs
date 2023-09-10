using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (timer > 0.5f)
        {
            Destroy(gameObject);
        }
        timer += Time.fixedDeltaTime;
        transform.localScale = Vector3.one * (1 + 100 * timer);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, (1 + 100 * timer));
        foreach (Collider2D collider in colliders) { 
            if (collider.TryGetComponent<PoliceBehavior>(out var police))
            {
                Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D>();
                rb.AddForce((rb.transform.position - transform.position) * 10);
            }
        }
    }
}
