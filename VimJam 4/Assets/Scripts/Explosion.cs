using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2);
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent<PoliceBehavior>(out var _))
            {
                Destroy(collider.gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        if (timer > 0.5f)
        {
            Destroy(gameObject);
        }
        timer += Time.fixedDeltaTime;
        transform.localScale = Vector3.one * (1 + 100 * timer);
    }
}
