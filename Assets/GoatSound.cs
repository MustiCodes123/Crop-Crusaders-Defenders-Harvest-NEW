using UnityEngine;

public class SoundLoop : MonoBehaviour
{
    public AudioClip soundClip;
    public float loopGap = 10f;

    private bool isPlaying = false;
    private AudioSource audioSource;

    private void Start()
    {
        // Create an AudioSource component dynamically
        audioSource = gameObject.AddComponent<AudioSource>();

        // Set the initial state to not playing
        isPlaying = false;
    }

    private void Update()
    {
        if (!isPlaying)
        {
            // Play the sound
            PlaySoundWithDelay();
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
}
