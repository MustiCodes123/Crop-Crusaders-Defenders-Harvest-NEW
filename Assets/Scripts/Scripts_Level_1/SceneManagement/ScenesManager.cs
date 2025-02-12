using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    private void Awake()
    {
        Instance = this;
        Application.runInBackground = true;
    }
	
	void Update()
	{
		//Debug.Log("TimeScale set to " + Time.timeScale);
	}

    public enum Scene { 
        MainMenu,
Level_Selector,
        Level_1_Scene,
		Level_2_Scene,
Level_3_Scene,


    }

    public void LoadScene(Scene scene)
    {
		//Debug.Log("Inside LoadScene");
        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Single);
    }

    public void LoadMainMenu()
    {
		//Debug.Log("Inside LoadMainMenu");
        LoadScene(Scene.MainMenu);
    }

public void LoadLevel1()
{
SceneManager.LoadScene(Scene.Level_1_Scene.ToString(), LoadSceneMode.Single);
		Time.timeScale = 1f;
}

public void LoadLevel2()
{
SceneManager.LoadScene(Scene.Level_2_Scene.ToString(), LoadSceneMode.Single);
		Time.timeScale = 1f;
}

public void LoadLevel3()
{
SceneManager.LoadScene(Scene.Level_3_Scene.ToString(), LoadSceneMode.Single);
		Time.timeScale = 1f;
}
    
    public void PlayGame()
    {
		//Debug.Log("Inside PlayGame");
        SceneManager.LoadScene(Scene.Level_Selector.ToString(), LoadSceneMode.Single);
		Time.timeScale = 1f;
    }
	
	
        public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        Time.timeScale = 1f; // Setting the time scale back to normal
		//Debug.Log("TimeScale set to " + Time.timeScale);
    }

    
}
