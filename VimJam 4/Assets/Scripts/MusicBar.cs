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

    public float currentMusicValue;

    void Start()
    {
        slider = GetComponent<Slider>();
        SetMusicValue(0);
    }


    void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("World1"))
        {
            SetMaxMusic(180);
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("World2"))
        {
            SetMaxMusic(200);
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("World3"))
        {
            SetMaxMusic(150);
        }
    }

    private void FixedUpdate()
    {
        currentMusicValue += 1 * Time.deltaTime;
        SetMusicValue(currentMusicValue);
    }

    public void SetMaxMusic(float music)
    {
        slider.maxValue = music;
    }

    public void SetMusicValue(float music)
    {
        slider.value = music;
    }
}
