using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float movementSmoothingSpeed = 1f;
    public float JumpHigh = 5;

    [SerializeField] private Vector3 rawInputMovement;
    [SerializeField] private Vector3 smoothInputMovement;
    [SerializeField] private PlayerInput inputmovement;

    [SerializeField] private PlayerMovementBehaviour Movement;

    void Update()
    {
        CalculateMovementInputSmoothing();
        UpdatePlayerMovement();
    }

    void UpdatePlayerMovement()
    {
        Movement.UpdateMovementData(smoothInputMovement);
    }

    void CalculateMovementInputSmoothing()
    {
        smoothInputMovement = Vector3.Lerp(smoothInputMovement, rawInputMovement, Time.deltaTime * movementSmoothingSpeed);
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 movement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(movement.x, 0, movement.y);
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * JumpHigh, ForceMode.Impulse);
        }
    }

    public void OnRun(InputAction.CallbackContext value)
    {
        if (value.started)
        {

        }
    }

    public void OnDance(InputAction.CallbackContext value)
    {
        if (value.started)
        {

        }
    }

    public void OnSkill(InputAction.CallbackContext value)
    {
        if (value.started)
        {

        }
    }
}
