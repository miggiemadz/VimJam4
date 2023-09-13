using UnityEngine;
using UnityEngine.UI;

public class MusicBar : MonoBehaviour
{
    [HideInInspector]
    public Slider slider;
    [HideInInspector]
    public AudioSource audioSource;
    public EnemySpawner spawner;

    bool songNegative = false;

    public float currentMusicValue
    {
        set
        {
            if (value < 0f)
            {
                audioSource.Stop();
                songNegative = true;
            }
            else
            {
                audioSource.time = value;

                // figure out how many cops need to spawn
                spawner.enemiesRemainingToSpawn = (int)(audioSource.clip.length / spawner.spawnCooldown);
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
        }
        else if (currentMusicValue >= audioSource.clip.length)
        {
            // TODO: The song finishes playing
        }
    }
}
