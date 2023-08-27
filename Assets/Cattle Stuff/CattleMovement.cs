using UnityEngine;
using System.Collections;

public class CattleMovement : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 2f;
    public float maxRotationDuration = 5f; // Maximum time allowed for rotation in one direction
    private float currentRotationTimer = 0f;

    private bool isMovingForward = true;

    private void Update()
    {
        PlayAnimation("walk_forward");
        if (isMovingForward)
        {
            MoveForward();
            currentRotationTimer += Time.deltaTime;

            // If rotation duration exceeds the threshold, change direction
            if (currentRotationTimer >= maxRotationDuration)
            {
                ChangeDirection();
            }
        }
    }

    private void PlayAnimation(string animationName)
    {
        animator.SetBool("walk_forward", false);
        animator.SetBool(animationName, true);
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void ChangeDirection()
    {
     //   Debug.Log("Changing direction due to rotation timeout.");

        // Set rotation to face the opposite direction
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 180f, 0f);

        // Reset rotation timer
        currentRotationTimer = 0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
   //     Debug.Log("Collision detected with: " + collision.gameObject.tag);

        if (collision.gameObject.CompareTag("RightBoundary"))
        {
          //  Debug.Log("COLLISION WITH BOUNDARY");

            isMovingForward = false;

            // Set rotation to face left (180 degrees opposite)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);

            StartCoroutine(ResetMovingForward());
        }
        else if (collision.gameObject.CompareTag("LeftBoundary"))
        {
         //   Debug.Log("COLLISION WITH BOUNDARY");

            isMovingForward = false;

            // Set rotation to face right (180 degrees opposite)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            StartCoroutine(ResetMovingForward());
        }
        else if (collision.gameObject.CompareTag("FrontBoundary"))
        {
          //  Debug.Log("COLLISION WITH BOUNDARY");

            isMovingForward = false;

            // Set rotation to face backward (180 degrees opposite)
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);

            StartCoroutine(ResetMovingForward());
        }
        else if (collision.gameObject.CompareTag("BackBoundary"))
        {
          //  Debug.Log("COLLISION WITH BOUNDARY");

            isMovingForward = false;

            // Set rotation to face forward (180 degrees opposite)
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            StartCoroutine(ResetMovingForward());
        }
    }

    private IEnumerator ResetMovingForward()
    {
        yield return new WaitForSeconds(2f); // Adjust the delay as needed

        // Reset rotation to face forward again
        transform.rotation = Quaternion.identity;

        // Reset rotation timer
        currentRotationTimer = 0f;

        isMovingForward = true;
    }
}
