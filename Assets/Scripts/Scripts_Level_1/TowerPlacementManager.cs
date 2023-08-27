using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


public class TowerPlacementManager : MonoBehaviour
{
    public GameObject towerPrefab;
	public Button towerButton;
    public int maxTowerPlacements;
	public GameObject joystickGameObject; 
    private int availableTowerPlacements;
    public static TowerPlacementManager instance;

    private void Awake()
	{
	  instance=this;	
	}
	
    private void Start()
    {
        availableTowerPlacements = maxTowerPlacements;
        towerButton.onClick.AddListener(PlaceTowerButtonClicked);
    }
	
	public void IncreaseTowerCount()
	{
Debug.Log("Going to increase tower count.");
		availableTowerPlacements++;
		Debug.Log("Tower count increase to "+ availableTowerPlacements);
	}

    public void PlaceTowerButtonClicked()
    {
        if (availableTowerPlacements > 0)
        {
            Debug.Log("Towers available");
            FadingButton.instance.OnButtonClick();
			 joystickGameObject.SetActive(false);
            StartCoroutine(PlaceTower());
			 joystickGameObject.SetActive(true);
            //FadingButton.instance.UpdateFadeDuration(4f);

        }
        else
        {
            Debug.Log("No towers available");

        }
    
    }

    private IEnumerator PlaceTower()
    {
        while (true) // Keep waiting for clicks indefinitely
        {
            if (Input.GetMouseButtonDown(0))
            {
				Debug.Log("Screen click detected");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("cornfield") || hit.collider.CompareTag("cornfieldmud") || hit.collider.CompareTag("Crop Plant") || hit.collider.CompareTag("canonbase"))
                    {
						Debug.Log("Got the exact position");
                        Vector3 towerPosition = hit.point;
                        towerPosition.y = 4.75f; // Set the Y position to 4.75

                        Instantiate(towerPrefab, towerPosition, Quaternion.identity).gameObject.SetActive(true);
                        
                        availableTowerPlacements--;
                    }
                    else
                    {
                        Debug.Log("Cannot place tower here. Select a valid Cornfield.");
                    }
                }

                yield break; // Exit the coroutine after placing the tower
            }

            yield return null;
        }
    }




}
