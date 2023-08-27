using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public HealthBarScript[] fields; 
    public GameObject gameOverScreen;

    void Update()
    {
        CheckFieldsHealth();
    }

    void CheckFieldsHealth()
    {
        int fieldsDestroyed = 0; 

        foreach (var field in fields)
        {
            if (field.slider.value <= 0)
            {
                fieldsDestroyed++;
            }
        }

        if (fieldsDestroyed == fields.Length)
        {
            
            ActivateGameOverScreen();
        }
    }

    void ActivateGameOverScreen()
    {
        Debug.Log("GAME OVER");

        Time.timeScale=0f;
        gameOverScreen.SetActive(true);
      
    }
}
