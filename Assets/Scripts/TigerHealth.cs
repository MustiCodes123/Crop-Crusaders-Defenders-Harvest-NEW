using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private int StartHealth;
    public HealthBarScript healthbar;
    public AudioClip soundClip;
    private bool isPlaying = false;
    private AudioSource audioSource;


    private void Awake()
    {
        healthbar.slider.value = maxHealth;
        currentHealth = maxHealth;
        healthbar.SetHealth(maxHealth);
    }
    private void Start()
    {

        audioSource = gameObject.AddComponent<AudioSource>();

        healthbar.slider.value = maxHealth;
        currentHealth = maxHealth;
        healthbar.SetHealth(maxHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected with: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth == 0)
        {
            PlaySound();
        }
        healthbar.SetHealth(currentHealth);
    }
    private void PlaySound() {

        if (!isPlaying)
        {
            audioSource.clip = soundClip;
            audioSource.Play();
            isPlaying = true;
        }

    }
}
