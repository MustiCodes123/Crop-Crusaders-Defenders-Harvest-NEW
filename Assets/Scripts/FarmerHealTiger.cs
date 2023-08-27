using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerHealTiger : MonoBehaviour
{
    public int healAmountPerTick = 20; // Amount to increase the health per healing tick
    public float healingInterval = 1f; // Time interval between healing ticks
    public HealthBarScript fieldHealth; // Reference to the specific field's health script
    public TigerAttackHealthDecrease tigerAttack;
    public FeedHealthBarScript feedHealth; // Reference to the feed health script
    private Coroutine healingCoroutine; // To keep track of the healing coroutine

    public AudioClip soundClip;
    private bool isPlaying = false;
    private AudioSource audioSource;

    public GameObject FeedParticles;
    public GameObject FeedParticlesSpawnPoint;
    public ReverseTimerController timerController;



    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = soundClip;
        audioSource.loop = true; // Set audio source to loop

    }


    private void Update()
    {
        if ((fieldHealth.slider.value < 100 && fieldHealth.slider.value > 0) && isPlaying && timerController.GetTimer() > 0)
        {
            PlaySound();
        }
        else
        {
            StopHealing();
            StopSound();
        }
    }

    private void PlaySound()
    {
        if (!isPlaying)
        {
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Farmer"))
        {
            StartHealing();
            if (feedHealth.slider.value > 0)
                PlaySound();

        }
    }


    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Farmer")) {

    //        GameObject effinst = (GameObject)Instantiate(FeedParticles, FeedParticlesSpawnPoint.transform.position, Quaternion.identity);
    //        Destroy(effinst, 3f);
    //    }
    //}


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Farmer"))
        {
            Debug.Log("FARMER LEFT HEALING AREA");
            StopHealing();
            StopSound();

        }
    }

    private void StartHealing()
    {
        if (healingCoroutine == null && feedHealth.GetHealth() > 0 && fieldHealth.slider.value != 0 && timerController.GetTimer() > 0)
        {
            healingCoroutine = StartCoroutine(HealingCoroutine());
            tigerAttack.currentHealth = tigerAttack.healthbar.GetHealth();
            PlaySound();
        }
    }

    private void StopHealing()
    {
        if (healingCoroutine != null)
        {
            StopCoroutine(healingCoroutine);
            healingCoroutine = null;
            StopSound();
        }
    }

    private IEnumerator HealingCoroutine()
    {
        while (true)
        {
            // Check if feed health is greater than 0 before increasing field health
            if (feedHealth.GetHealth() > 0)
            {
                // Increase field health
                int newHealth = (int)fieldHealth.slider.value + healAmountPerTick;
                fieldHealth.SetHealth(newHealth);

                // Decrease feed health
                if (fieldHealth.slider.value < 100)
                {
                    float newFeedHealth = feedHealth.GetHealth() - healAmountPerTick;
                    feedHealth.SetHealth(newFeedHealth);


                    GameObject effinst = (GameObject)Instantiate(FeedParticles, FeedParticlesSpawnPoint.transform.position, Quaternion.identity);
                    Destroy(effinst, 2f);

                }
            }

            // Increase tiger health if not exceeding the maximum
            if (tigerAttack.currentHealth <= 100)
            {
                float newHealthCrow = tigerAttack.currentHealth + healAmountPerTick;
                tigerAttack.currentHealth = newHealthCrow;
                tigerAttack.healthbar.SetHealth(newHealthCrow);
            }

            if (fieldHealth.slider.value >= 100 || fieldHealth.slider.value == 0)
            {
                StopSound();
            }
            else
            {
                PlaySound(); // Play sound as long as the healing continues
            }


            yield return new WaitForSeconds(healingInterval);
        }
    }
}