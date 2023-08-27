using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private Button quitButton;
    [SerializeField] private Button resumeButton;
    void Start()
    {
        quitButton.onClick.AddListener(quit_Level);
        resumeButton.onClick.AddListener(resume_Level);
    }

    public void quit_Level()
    {
		Debug.Log("Inside quit_Level");
        ScenesManager.Instance.LoadMainMenu();
    }

    public void resume_Level()
    {
		Debug.Log("Inside resume_Level");
        PauseButton.instance.PauseGame();
    }
}
