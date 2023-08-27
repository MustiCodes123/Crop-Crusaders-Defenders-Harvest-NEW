using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingCoins : MonoBehaviour
{
    public int coins;
    public AudioClip soundClip;
    private bool isPlaying = false;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Coin Collected");
            coins += 10;
            PlaySound();
            Destroy(col.gameObject);
            isPlaying = false;
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

    public int GetCollectedCoins()
    {
        return coins;
    }
   public void SetCoins(int newCoin)
    {
        coins = newCoin;
        Debug.Log("Remaining Coins are " + coins);
    }
}
