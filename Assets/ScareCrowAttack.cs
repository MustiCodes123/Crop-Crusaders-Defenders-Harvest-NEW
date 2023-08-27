#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;

public class ScareCrowAttack : MonoBehaviour
{

    public enum UpgradeLevel
    {
        Basic,
        Improved,
        Advanced
    }

    public UpgradeLevel currentUpgrade = UpgradeLevel.Basic;

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
    public Animator animationController;

    void Start()
    {
        animationController = GetComponent<Animator>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
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

    void Update()
    {
        if (target == null)
        {
           // Debug.Log("IDLE ANIMATION PLAYING");
            animationController.SetTrigger("idle");
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(RotatePart.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        RotatePart.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        float fireRate = GetFireRateForUpgradeLevel();

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        animationController.SetTrigger("attack1");
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
#endif
