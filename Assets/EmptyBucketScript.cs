using UnityEngine;

public class EmptyBucketScript : MonoBehaviour
{
    public WaterBarScript feedBar; // Reference to the WaterBarScript
    public GameObject emptyBucketText;
    public ReverseTimerController rtc;
    private bool isEmpty = false;

    private void Start()
    {
        HideCanvas();
    }

    void Update()
    {
        if (rtc.GetTimer() > 58)
        {
            HideCanvas();
            isEmpty = false; // Reset isEmpty when the timer is greater than 58
        }
        else
        {
            if (feedBar.slider.value == 0)
            {
                if (!isEmpty)
                {
                    ShowCanvas();
                    isEmpty = true;
                }
            }
            else
            {
                if (isEmpty)
                {
                    HideCanvas();
                    isEmpty = false;
                }
            }
        }
    }

    void HideCanvas()
    {
        if (emptyBucketText.activeSelf)
        {
            emptyBucketText.SetActive(false);
        }
    }

    void ShowCanvas()
    {
        if (!emptyBucketText.activeSelf)
        {
            emptyBucketText.SetActive(true);
        }
    }
}
