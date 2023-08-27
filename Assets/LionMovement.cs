using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionMovement : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        PlayAnimation("walk_forward");
    }

    private void PlayAnimation(string animationName)
    {
        animator.SetBool(animationName, true);
    }
}
