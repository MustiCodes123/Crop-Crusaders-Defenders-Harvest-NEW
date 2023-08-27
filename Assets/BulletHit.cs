using UnityEngine;

public class BulletHit : MonoBehaviour
{
    private int bulletCount = 0;
    public GameObject coinPrefab;
    public float coinDropHeight = 5.0f;
    public float upwardForce = 25.0f; // Adjust this to control the upward force applied to the crow

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void HitByBullet()
    {
        bulletCount++;
        Debug.Log("Enemy hit by " + bulletCount + " bullets.");

        if (bulletCount >= 2)
        {
            Die(); // the enemy dies here
        }
    }

    private void Die()
    {
        rb.useGravity = true; // Enable gravity to make the crow fall smoothly
        rb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse); // Apply upward force

        Destroy(gameObject, 2.0f); // Destroy the crow after 2 seconds
        DropCoin();
    }

    void DropCoin()
    {
        Vector3 coinDropPosition = new Vector3(transform.position.x, coinDropHeight, transform.position.z);
        GameObject coinIns = Instantiate(coinPrefab, coinDropPosition, Quaternion.identity);
        Destroy(coinIns, 15f);
    }
}
