using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
	private int numberOfProductsToShow = 1;
	//public int totalProducts;
	public GameObject shopWindow;
	public GameObject[] products;
	private int pageNumber = 1;
	
    void Start()
    {
       for(int i=0;i<products.Length;i++)
	   {
        products[i].SetActive(false);
	   }		   
	   
	   Refresh();
    }
	
	public void OpenShop()
	{
		Time.timeScale = 0f;
		shopWindow.SetActive(true);
		Refresh();
	}
	
	public void CloseShop()
	{
		products[pageNumber-1].SetActive(false);
		pageNumber=1;
		shopWindow.SetActive(false);
		Time.timeScale = 1f;
	}
	
	public void Refresh()
	{
		for(int i=0;i<products.Length;i++)
			products[i].SetActive(false);
		
		if(pageNumber==1)
		{
			for(int i=0;i<numberOfProductsToShow;i++){
			//products[i].GetComponent<Products>().id=id[i];
			products[i].SetActive(true);
			}
		}
	}

    public void NextPage()
	{
		if(pageNumber>0 && pageNumber < products.Length)
		{
			products[pageNumber-1].SetActive(false);
			pageNumber++;
			Debug.Log("Page number changed to :- " + pageNumber);
			products[pageNumber-1].SetActive(true);
		}
	}
	
	public void PreviousPage()
	{
		if(pageNumber>1)
		{
			products[pageNumber-1].SetActive(false);
			pageNumber--;
			Debug.Log("Page number changed to :- " + pageNumber);
			products[pageNumber-1].SetActive(true);
		}
	}
}
