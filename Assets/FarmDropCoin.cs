using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmDropCoin : MonoBehaviour
{
    public GameObject coinPrefab;
    private float coinDropHeight = 2.0f;
    public HealthBarScript fieldhealth;
    public GameObject farmPosition;

    private float healthThreshold = 100f; // Updated health threshold to 50
    private float timeThreshold = 10f;
    private float elapsedTime = 0f;

    void Update()
    {
        if (fieldhealth.slider.value >= healthThreshold)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= timeThreshold)
            {
                elapsedTime = 0f; // Reset the timer
                DropCoin();
            }
        }
        else
        {
            elapsedTime = 0f;
        }
    }

    void DropCoin()
    {
        GameObject coinIns = Instantiate(coinPrefab, farmPosition.transform.position + Vector3.up * coinDropHeight, Quaternion.identity);
        Destroy(coinIns, 10f);
    }
}
