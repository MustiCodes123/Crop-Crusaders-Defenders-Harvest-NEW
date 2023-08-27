using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public Text CurrencyText;

    void Update()
    {
        UpdateCurrencyText();
    }

    void UpdateCurrencyText()
    {
        CollectingCoins collectingCoinsScript = GetComponent<CollectingCoins>();

        if (collectingCoinsScript != null)
        {
            int coins = collectingCoinsScript.GetCollectedCoins();
            CurrencyText.text = "$" + coins.ToString();
        }
        else
        {
            Debug.LogWarning("CollectingCoins script not found on the same GameObject.");
        }
    }
}
