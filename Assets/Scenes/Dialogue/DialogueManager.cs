using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    private Queue<string> sentences;
    public GameObject dialogueCanvas;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Time.timeScale = 0;
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {

            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {

        if (sentences.Count == 0)
        {

            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log(sentence);
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence) { 
    
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()){
            
            dialogueText.text += letter;
            yield return null;

        }
    
    }


    public void EndDialogue() {

        dialogueCanvas.SetActive(false);
        Time.timeScale = 1;
    }





}
