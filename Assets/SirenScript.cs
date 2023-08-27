using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class SirenScript : MonoBehaviour
{
    public AudioClip soundClip;
    private bool isPlaying = false; // Track whether the sound is playing
    private AudioSource audioSource;
    public ReverseTimerController timerController;
    public TextMeshProUGUI canvasText;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true; // Set audio source to loop
        canvasText.enabled = false;
    }

    void Update()
    {
        float currentTimer = timerController.GetTimer();

        if (currentTimer <= 3.0f && currentTimer > 0.0f)
        {
            PlaySound();
            canvasText.enabled = true;
        }
        else
        {
            StopSound();
            canvasText.enabled = false;
        }
    }

    private void PlaySound()
    {
        if (!isPlaying)
        {
            audioSource.clip = soundClip; // Set the clip to be played
            audioSource.Play();
            isPlaying = true;
        }
    }

    private void StopSound()
    {
        if (isPlaying)
        {
            audioSource.Stop();
            isPlaying = false;
        }
    }
}
