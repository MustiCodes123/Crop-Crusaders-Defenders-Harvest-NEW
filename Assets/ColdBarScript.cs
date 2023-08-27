using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ColdBarScript : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public int startHealth;
    public float DisplayHealth;
    public float healthDecreaseRate = 1f;

    public AudioClip DestroySound;
    private bool hasPlayedDestroySound = false;
    private AudioSource audioSource;
    private void Start()
    {
        startHealth = 100;
        SetHealth(startHealth);
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        float newHealth = GetHealth() - healthDecreaseRate * Time.deltaTime;
        SetHealth(newHealth);

        if (slider.value == 0 && !hasPlayedDestroySound) // Check if the destroy sound has not been played yet
        {
            PlayDestroySound();
            hasPlayedDestroySound = true; // Set the flag to true to indicate that the sound has been played
        }
    }
    void PlayDestroySound()
    {
        audioSource.PlayOneShot(DestroySound);
    }

    public void SetMaxHealth(int health)
    {
        if (health <= 0)
            health = 0;
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }
    public void SetHealth(float health)
    {
        if (health <= 0)
            health = 0;
        else if(health >=100)
            health = 100;
        DisplayHealth = health;
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    public float GetHealth()
    {
        return slider.value;
    }

}
