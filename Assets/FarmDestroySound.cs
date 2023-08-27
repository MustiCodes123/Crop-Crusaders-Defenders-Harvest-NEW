using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmDestroySound : MonoBehaviour
{
    public HealthBarScript[] healthbar;
    public AudioClip DestroySound;
    private bool hasPlayedDestroySound = false;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        healthbar = new HealthBarScript[3];
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((healthbar[0].slider.value == 0 || healthbar[1].slider.value == 0 || healthbar[2].slider.value == 0 || healthbar[3].slider.value == 0) && !hasPlayedDestroySound) // Check if the destroy sound has not been played yet
        {
            PlayDestroySound();
            hasPlayedDestroySound = true; // Set the flag to true to indicate that the sound has been played
        }
    }

    void PlayDestroySound()
    {
        audioSource.PlayOneShot(DestroySound);
    }
}
