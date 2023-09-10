using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBox : MonoBehaviour
{
    public GameObject player;

    public bool boomboxOn;

    void Start()
    {
        boomboxOn = false;
    }

    void FixedUpdate()
    {
        Debug.Log(boomboxOn);
    }
}
