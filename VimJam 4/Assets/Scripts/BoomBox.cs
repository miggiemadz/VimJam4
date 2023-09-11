using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoomBox : MonoBehaviour
{
    public GameObject player;

    public bool boomboxOn = false;
    public Slider healthBar;

    public float boomBoxHealth
    {
        get { return healthBar.value; }
        set { healthBar.value = value; if (value <= 0)
            {
                // get rid of the boombox if it's dead
                gameObject.SetActive(false);
                boomboxOn = false;
            }
        }
    }
}
