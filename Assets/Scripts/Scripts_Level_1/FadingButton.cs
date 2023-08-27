using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadingButton : MonoBehaviour
{
    public Button button;
    public float fadeDuration = 1.0f;
	
	public static FadingButton instance;
    private Image buttonImage;
    private Color originalColor;
   

    private void Start()
    {
		instance=this;
        buttonImage = button.GetComponent<Image>();
        originalColor = buttonImage.color;
       // button.onClick.AddListener(OnButtonClick);
    }
	public void UpdateFadeDuration(float addition)
	{
		fadeDuration += addition;
	}

    public void OnButtonClick()
    {
        Debug.Log("Going to make the buttons interaction false.");
        button.interactable = false;
        StartCoroutine(FadeButton());
	}
	

    private IEnumerator FadeButton()
    {
        float elapsedTime = 0;
        Color fadedColor = originalColor;
        fadedColor.a = 0;

        while (elapsedTime < fadeDuration)
        {
            buttonImage.color = Color.Lerp(originalColor, fadedColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Simulate loading effect
        yield return new WaitForSeconds(2.0f); // Replace 2.0f with your loading time

        buttonImage.color = originalColor;
        Debug.Log("Going to make the buttons interaction false.");
        button.interactable = true;
    }
}
