using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovementManager : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private float movement_Speed = 5f;
    [SerializeField] private float rotation_Speed = 3f;
    [SerializeField] private Canvas canvas;
    private Vector3 movement_Direction;
    private bool isJoystick;

    public void Start()
    {
        EnableJoystick();
    }
    
    public void EnableJoystick()
    {
        isJoystick = true;
        canvas.gameObject.SetActive(true);
    }

    public void Update()
    {
        if (isJoystick)
        {
            movement_Direction = new Vector3(joystick.Direction.x, 0.0f, joystick.Direction.y);
            controller.SimpleMove(movement_Direction * movement_Speed);
        }

        if (movement_Direction.sqrMagnitude <= 0.0f)
        {
            return;
        }

        var targetDirection = Vector3.RotateTowards(controller.transform.forward, movement_Direction, rotation_Speed * Time.deltaTime, 0.0f);
        controller.transform.rotation = Quaternion.LookRotation(targetDirection);
    }


}
