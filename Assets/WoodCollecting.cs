using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodCollecting : MonoBehaviour
{
    public WoodHealthBarScript _WoodScript; // Reference to the WoodHealthBarScript
    public float increaseInterval = 1.0f;
    private float timer = 0.0f;
    private bool isColliding = false;
    public AudioClip soundClip;
    private bool isPlaying = false;
    private AudioSource audioSource;

    private void Start()
    {
        timer = 0.0f;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true; // Set audio source to loop
    }

    private void Update()
    {
        if (isColliding)
        {
            timer += Time.deltaTime;

            if (timer >= increaseInterval)
            {
                _WoodScript.SetHealth(_WoodScript.GetHealth() + 1);
                timer = 0.0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Farmer"))
        {
            PlaySound();
            isColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Farmer"))
        {
            StopSound();
            isColliding = false;
            timer = 0.0f;
        }
    }

    private void PlaySound()
    {
        if (!isPlaying)
        {
            audioSource.clip = soundClip;
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

    public void DescreaseFeedHealth(int amount)
    {
        _WoodScript.SetHealth(_WoodScript.GetHealth() - amount);
    }
}
