using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerHeal : MonoBehaviour
{
    public int healAmountPerTick = 20;
    public float healingInterval = 1f;
    public HealthBarScript fieldHealth;
    public CrowAttackHealthDecrease crowAttack;
    public WaterBarScript waterLevelScript;
    public AudioClip soundClip;
    private bool isPlaying = false;
    private AudioSource audioSource;
    public GameObject WaterParticles;
    public GameObject waterParticlesSpawnPoint;
    public ReverseTimerController timerController;
    private Coroutine healingCoroutine;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
    }

    private void Update()
    {
        if (fieldHealth.slider.value < 100 && isPlaying && timerController.GetTimer() > 0)
        {
            PlaySound();
        }
        else if (fieldHealth.slider.value >= 100 || !isPlaying)
        {
            StopHealing();
            StopSound();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Farmer") && timerController.GetTimer() > 0)
        {
            StartHealing();
            if (fieldHealth.slider.value < 100 && waterLevelScript.slider.value >0 )
            {
                PlaySound();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Farmer") && waterLevelScript.slider.value > 0 && fieldHealth.slider.value > 0 && fieldHealth.slider.value < 100 && timerController.GetTimer() > 0)
        {
            GameObject effinst = (GameObject)Instantiate(WaterParticles, waterParticlesSpawnPoint.transform.position, Quaternion.identity);
            Destroy(effinst, 2f);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Farmer"))
        {
            StopHealing();
            StopSound();
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

    public void StopSound()
    {
        if (isPlaying)
        {
            audioSource.Stop();
            isPlaying = false;
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

    private void StartHealing()
    {
        if (healingCoroutine == null && waterLevelScript.GetHealth() > 0 && timerController.GetTimer() > 0)
        {
            healingCoroutine = StartCoroutine(HealingCoroutine());
            crowAttack.currentHealth = crowAttack.healthbar.GetHealth();
            PlaySound();
        }
    }

    private IEnumerator HealingCoroutine()
    {
        while (true)
        {
            if (waterLevelScript.GetHealth() > 0 && fieldHealth.slider.value < 100)
            {
                // Increase field health
                int newHealth = (int)fieldHealth.slider.value + healAmountPerTick;
                fieldHealth.SetHealth(newHealth);

                // Decrease water level health
                float newWaterLevel = waterLevelScript.GetHealth() - healAmountPerTick;
                waterLevelScript.SetHealth(newWaterLevel);

            }

            // Increase crow health if not exceeding the maximum
            if (crowAttack.currentHealth <= 100)
            {
                float newHealthCrow = crowAttack.currentHealth + healAmountPerTick;
                crowAttack.currentHealth = newHealthCrow;
                crowAttack.healthbar.SetHealth(newHealthCrow);
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
