using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Fusion;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Player : SimulationBehaviour , NetworkBehaviourCallbacks
{
    [Networked]
    public float PlayerSpeed { get; set; }
    [Networked]
    public float movementSmoothingSpeed { get; set; }
    public float JumpHigh = 5;


    [Networked]
    private Vector3 rawInputMovement { get; set; }
    [Networked]
    private Vector3 smoothInputMovement { get; set; }
    [SerializeField] private PlayerInput inputmovement { get; set; }


    [SerializeField] private PlayerMovementBehaviour Movement;

    private GameMenu gameMenu;

    private void Start()
    {
        gameMenu = FindObjectOfType<GameMenu>();
        movementSmoothingSpeed = PlayerSpeed;
    }

    public override void FixedUpdateNetwork()
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
        if(gameMenu == null)
        {
            return;
        }

        if (!gameMenu.Isup)
        {
            smoothInputMovement = Vector3.Lerp(smoothInputMovement, rawInputMovement, Time.deltaTime * movementSmoothingSpeed);
        }
        
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 movement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(movement.x, 0, movement.y);
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.started && !gameMenu.Isup)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * JumpHigh, ForceMode.Impulse);
        }
    }

    public void OnRun(InputAction.CallbackContext value)
    {
        if (!gameMenu.Isup)
        {
            switch (value.phase)
            {
                case InputActionPhase.Disabled:
                    movementSmoothingSpeed = PlayerSpeed;
                    break;
                case InputActionPhase.Performed:
                    movementSmoothingSpeed = PlayerSpeed + 5;
                    break;
                case InputActionPhase.Canceled:
                    movementSmoothingSpeed = PlayerSpeed;
                    break;
                default:
                    break;
            }
        }
        
    }

    public void OnDance(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            //Run Animation
        }
    }

    public void OnSkill(InputAction.CallbackContext value)
    {
        if (value.started)
        {

        }
    }

    public void Menu(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            gameMenu.OpenMenu();
        }
    }
}
