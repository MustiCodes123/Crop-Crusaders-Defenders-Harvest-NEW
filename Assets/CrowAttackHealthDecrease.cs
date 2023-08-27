using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowAttackHealthDecrease : MonoBehaviour
{
    public int maxHealth = 100;
    public float currentHealth;
    public HealthBarScript healthbar;

    private bool crowIsOnField = false;
    public float damagePerSecond = 5.0f; // Adjust this value as needed

    private void Start()
    {
        currentHealth = 1;
        healthbar.SetHealth(currentHealth);
    }

    private void Update()
    {
        if (crowIsOnField)
        {
            DecreaseFieldHealthOverTime();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("CROW ON FIELD");
            crowIsOnField = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            crowIsOnField = false;
        }
    }

    private void DecreaseFieldHealthOverTime()
    {
        float damage = (float)(damagePerSecond * Time.deltaTime);
        currentHealth -= damage;
        if (currentHealth <= 0)
            currentHealth = 0;
        healthbar.SetHealth(currentHealth);
    }
}
