using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerAttackHealthDecrease : MonoBehaviour
{
    public int maxHealth = 100;
    public float currentHealth;
    public HealthBarScript healthbar;
    public AudioClip soundClip;

    public float loopGap = 10f;
    private bool isPlaying = false;
    private AudioSource audioSource;

    private bool tigerIsOnField = false;
    public float damagePerSecond = 8.0f; // Adjust this value as needed

    private void Start()
    {
        currentHealth = 1;
        healthbar.SetHealth(currentHealth);
        audioSource = gameObject.AddComponent<AudioSource>();
        isPlaying = false;
    }

    private void Update()
    {
        if (tigerIsOnField)
        {
            DecreaseFieldHealthOverTime();
            if (!isPlaying)
            {
                
                PlaySoundWithDelay();
            }
        }
    }
    private void PlaySoundWithDelay()
    {
        isPlaying = true;
        audioSource.PlayOneShot(soundClip);

        // Start a coroutine to reset isPlaying after the loopGap
        StartCoroutine(ResetIsPlaying());
    }

    private System.Collections.IEnumerator ResetIsPlaying()
    {
        yield return new WaitForSeconds(loopGap);

        isPlaying = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            tigerIsOnField = true;
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            tigerIsOnField = false;
        }
    }

    private void DecreaseFieldHealthOverTime()
    {
        float damage = (float)(damagePerSecond * Time.deltaTime);
        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;
      
        healthbar.SetHealth(currentHealth);
    }

 
}
