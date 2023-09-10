using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class MusicBar : MonoBehaviour
{
    public Slider slider;
    public AudioSource audioSource;


    public float currentMusicValue { 
        set
        { 
            audioSource.time = value; 
            slider.value = value; 
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
    }
}
