using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WoodHealthBarScript : MonoBehaviour
{
    public Slider slider;
    public int startHealth;


    private void Start()
    {
        startHealth = 0;
        SetHealth(startHealth);

    }


    public void SetMaxHealth(int health)
    {
        if (health <= 0)
            health = 0;
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        if (health <= 0)
            health = 0;
        slider.value = health;
    }
    public float GetHealth()
    {
        return slider.value;
    }




}
