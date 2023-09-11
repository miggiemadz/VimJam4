using UnityEngine;
using UnityEngine.UI;

public class MusicBar : MonoBehaviour
{
    public Slider slider;
    public AudioSource audioSource;

    bool songNegative = false;

    public float currentMusicValue { 
        set
        { 
            if (value < 0f)
            {
                audioSource.Stop();
                songNegative = true;
            } else
            {
            audioSource.time = value;
            }
        }
        get { return audioSource.time; }
    }
    
    void Start()
    {
        slider = GetComponent<Slider>();
        audioSource = GetComponent<AudioSource>();

        slider.maxValue = audioSource.clip.length;
        currentMusicValue = 0;
        audioSource.Play();
    }

    public void Update()
    {
        slider.value = audioSource.time;
        if (songNegative)
        {
            // TODO: The song is set below zero
        } else if (currentMusicValue >= audioSource.clip.length)
        {
            // TODO: The song finishes playing
        }
    }
}
