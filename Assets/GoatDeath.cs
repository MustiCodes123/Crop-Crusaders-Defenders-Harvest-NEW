using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatDeath : MonoBehaviour
{

    public HealthBarScript healthbar;

    private void Update()
    {
        if (healthbar.slider.value == 0)
        {
            Destroy(gameObject);
        }
    }

}
