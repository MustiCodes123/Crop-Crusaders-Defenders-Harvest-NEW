using UnityEngine;
using UnityEngine.AI;

public class TigerBulletHit : MonoBehaviour
{
    private int bulletCount = 0;
    public GameObject coinPrefab;
    public float coinDropHeight = 5.0f;
    public float upwardForce = 2147483647;
    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;

    // Reference to the "ReturnTag" GameObject
    public string returnTag = "ReturnTag";

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            HitByBullet();
        }
    }

    private void Update()
    {
        CheckReturnZone();
    }

    public void HitByBullet()
    {
        bulletCount++;

        if (bulletCount >= 5)
        {
            Die();
        }
    }

    private void Die()
    {
        navMeshAgent.enabled = false;
        rb.useGravity = true;
        rb.AddForce(-Vector3.up * upwardForce, ForceMode.Impulse);

        Destroy(gameObject, 2.0f);
        DropCoin();
    }

    void DropCoin()
    {
        Vector3 coinDropPosition = new Vector3(transform.position.x, coinDropHeight, transform.position.z);
        GameObject coinDestroy = Instantiate(coinPrefab, coinDropPosition, Quaternion.identity);
        Destroy(coinDestroy, 15f);
    }

    void CheckReturnZone()
    {
        GameObject returnObject = GameObject.FindGameObjectWithTag(returnTag);

        if (returnObject != null)
        {
            float distanceToReturn = Vector3.Distance(transform.position, returnObject.transform.position);

            if (distanceToReturn <= 1f)
            {
                Destroy(gameObject);
            }
        }
    }
}
