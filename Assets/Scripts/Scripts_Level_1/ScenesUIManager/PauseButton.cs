using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject pauseMenuCanvas;
    public bool isPaused = false;
    public static PauseButton instance;

    private void Start()
    {
        instance = this;
        pauseButton.onClick.AddListener(PauseGame);
    }

    public void PauseGame()
    {
        if(!isPaused)
        {
            Time.timeScale = 0f;
            
            pauseMenuCanvas.SetActive(true);
            isPaused = true;
        }
        else
        {
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
    }
}
