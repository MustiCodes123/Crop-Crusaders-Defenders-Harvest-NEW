using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Products : MonoBehaviour
{
    public GameObject shop;
	public int id;
	public string productName;
	public Image productImageComponent;
	public int price;
	public TowerPlacementManager tower;
	public GameObject product_Prefab;
	public Sprite sprite;
	public TextMeshProUGUI nameText, priceText, upgradePriceText;
	private CannonAttack attack_Script_Component;
	public CollectingCoins CoinsRemaining;
	
	void Start()
    {
		product_Prefab.SetActive(false);
        shop =GameObject.Find("shop");
		attack_Script_Component = product_Prefab.GetComponent<CannonAttack>();
    }

    void Update()
    {
        nameText.text=productName;
		productImageComponent.sprite=sprite;
		priceText.text = price.ToString();
    }
	
	public void  Equip()
	{
		Debug.Log("Equip() function called.");
	   if(!attack_Script_Component.getWeaponUseStatus())	
	   {
           if(attack_Script_Component.GetBuyStatus())
		   {
			   attack_Script_Component.SetWeaponUseStatus();
			   product_Prefab.SetActive(true);
		   }
		   else
		   {
			   Debug.Log("First Buy the weapon.");
		   }
	   }
       else
	   {
          Debug.Log("Weapon already in use.");
	   }		   
	}
	
	public void Upgrade()
	{
	   // Debug.Log("Upgrade() function called.");
		if(attack_Script_Component.GetBuyStatus())
		{
			if(attack_Script_Component.isFullyUpgraded()==true)
			{
			int count = CoinsRemaining.GetCollectedCoins();
			if(count >= 10)
			{
				attack_Script_Component.UpgradeWeapon();
				CoinsRemaining.SetCoins(count-10);
				Debug.Log("Weapon Upgraded.");
			}
			else
		     	Debug.Log("Not enough coins to buy the weapon.");
		    }
		    else
				Debug.Log("Weapon is fully upgraded.");
		}		
		else
			Debug.Log("First buy the weapon.");
		
	}
	
	public void BuyGun()
	{
		Debug.Log("Buy Called");
		if(!attack_Script_Component.GetBuyStatus())
		{
			Debug.Log("Attempting to Buy Weapon");
			if(TryBuyGun())
			{
				Debug.Log("Successfully Bought the Weapon " + productName);
			}
			else
				Debug.Log("Not enough Coins");
		}
		else
			Debug.Log("Weapon already Bought");
	}
	
	
	public void BuyTower()
	{
		Debug.Log("Buy Called");
		Debug.Log("Attempting to Buy Weapon");
			if(TryBuyTower())
			{
				Debug.Log("Successfully Bought the Weapon " + productName);
			}
			else
				Debug.Log("Not enough Coins");
	}
	
	private bool TryBuyTower()
	{
		int count = CoinsRemaining.GetCollectedCoins();
		if(count >= price)
		{
			CoinsRemaining.SetCoins(count-price);
			Debug.Log("I am here");
			attack_Script_Component.SetWeaponStatustoBought();
			tower.IncreaseTowerCount();
Debug.Log("Weapon Bought...Going to return true");
			return true;
		}
		return false;
	}
	
	private bool TryBuyGun()
	{
		int count = CoinsRemaining.GetCollectedCoins();
		if(count >= price)
		{
			CoinsRemaining.SetCoins(count-price);
			product_Prefab.SetActive(true);
			attack_Script_Component.SetWeaponStatustoBought();
			return true;
		}
		return false;
	}
}
