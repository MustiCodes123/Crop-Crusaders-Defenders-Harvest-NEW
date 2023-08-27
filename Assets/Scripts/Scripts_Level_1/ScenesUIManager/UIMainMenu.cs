using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{

    [SerializeField] Button playButton, shopButton, quitButton;
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void PlayGame()
    {
        ScenesManager.Instance.PlayGame();
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
