using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSound : MonoBehaviour
{

    public AudioClip soundClip;
    private bool isPlaying = false;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = soundClip;
        audioSource.loop = true; // Set audio source to loop
    }

    // Update is called once per frame
    void Update()
    {
        PlaySound();
    }

    private void PlaySound()
    {
        if (!isPlaying)
        {
            audioSource.Play();
            isPlaying = true;
        }
    }
}
