using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonAttack : MonoBehaviour
{
    public enum UpgradeLevel
    {
        Basic,
        Improved,
        Advanced,
    }

    public UpgradeLevel currentUpgrade;
    [SerializeField] private Text error_Message_Text;
    [SerializeField] private Text weapon_Upgrade_Message;

    private Transform target;

    [Header("Attributes")]
    public float fireRate = 1f;
    public float fireCountdown = 0f;
    public float range = 15f;

    [Header("Unity Setup Fields")]
    public string enemytag = "Enemy";
    public Transform RotatePart;
    public float turnSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;
	
	private bool isWeaponBought;
	private bool allowWeaponUse;
	
//	void Awake()
//{
//    currentUpgrade = UpgradeLevel.Basic;
//}

    void Start()
    {
		isWeaponBought=false;
		allowWeaponUse=false;
		//weapon_Upgrade_Message.gameObject.SetActive(false);
		//error_Message_Text.gameObject.SetActive(false);
         InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

  


     IEnumerator ShowUpgradeMessage(string message, float delay)
     {
     weapon_Upgrade_Message.text = message;
     weapon_Upgrade_Message.gameObject.SetActive(true);
     yield return new WaitForSeconds(delay);
     weapon_Upgrade_Message.gameObject.SetActive(false);
     }

     IEnumerator ShowErrorMessage(string message, float delay)
     {
     error_Message_Text.text = message;
     error_Message_Text.gameObject.SetActive(true);
     yield return new WaitForSeconds(delay);
     error_Message_Text.gameObject.SetActive(false);
    }

    public bool isFullyUpgraded()
	{
	
	   if(currentUpgrade == UpgradeLevel.Advanced)	
	   {
		   Debug.Log("Upgrade level " + currentUpgrade);
		   return true;
	   }
	   return false;
	}
    
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemytag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {

            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
	
	public bool getWeaponUseStatus()
	{
		return allowWeaponUse;
	}
	
	public void SetWeaponUseStatus()
	{
		allowWeaponUse=true;	
	}
	
	public bool GetBuyStatus()
	{
		return isWeaponBought;
		Debug.Log("allowWeaponUse set to "+allowWeaponUse);
	}
	
	public void SetWeaponStatustoBought()
	{
		isWeaponBought=true;
		Debug.Log("isWeaponBought set to "+isWeaponBought);
	}
	
	 public void UpgradeWeapon()
   {
    Debug.Log("Checking upgrade level: " + currentUpgrade);
    switch (currentUpgrade)
    {
        case UpgradeLevel.Basic:
            currentUpgrade = UpgradeLevel.Improved;
            break; 
        case UpgradeLevel.Improved:
            currentUpgrade = UpgradeLevel.Advanced;
            break; 
        case UpgradeLevel.Advanced:
            Debug.Log("Upgrade is already advanced.");
            break; 
        default:
            Debug.Log("Upgrade is already advanced.");
            break; 
    }
    }

    void Update()
    {
		Debug.Log("Upgrade level " + currentUpgrade);
        if (target == null)
            return;
        
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(RotatePart.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        RotatePart.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        float fireRate = GetFireRateForUpgradeLevel();
         Debug.Log("fire rate: "+fireRate);
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GetComponent<AudioSource>().Play();
        GameObject BulletGameObject =  (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = BulletGameObject.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private float GetFireRateForUpgradeLevel()
    {
        switch (currentUpgrade)
        {
            case UpgradeLevel.Basic:
                return 1f;
            case UpgradeLevel.Improved:
                return 3f;
            case UpgradeLevel.Advanced:
                return 5f;
            default:
                return 1f; // Default to Basic fire rate
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position , range);

    }

}
