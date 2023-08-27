using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public ReverseTimerController rtc;
    public Dialogue dialogue;
    private bool dialogueTriggered = false;
    public GameObject dialogueCanvas; // Reference to the dialogue canvas

    private void Start()
    {
        dialogueCanvas.SetActive(false);

    }

    void Update()
    {
        if (!dialogueTriggered && rtc.GetTimer() <= 60.0 && rtc.GetTimer() > 59.0f)
        {
            dialogueCanvas.SetActive(true);
            Debug.Log("DISPLAYING DIALOGUE");
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            dialogueTriggered = true;
        }
    }
}
