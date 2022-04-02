using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayeController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float gravity = -9.81f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = .4f;
    [SerializeField] private LayerMask groundMask;

    public Transform model;

    private InputAction movement;
    private CharacterActions characterAction;
    private CharacterController controller;

    private Vector3 velocity;

    private bool canMove = false;

    private void Awake() {
        controller = GetComponent<CharacterController>();
        characterAction = new CharacterActions();
    } 

    private void FixedUpdate() {
        if(!canMove)
            return;
            
        HandleMovement();
    }

    private void HandleMovement(){
        velocity.x = movement.ReadValue<float>() * speed;

        HandleGravity();
        HandleTurn(velocity.x);

        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleTurn(float direction){
        if(direction != 0){
            model.rotation = new Quaternion(0, direction < 0 ? 180 : 0, 0, 0);
        }
    }

    private void HandleGravity(){
        if(IsGrounded() && velocity.y < 0){
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
    }

    private void DoJump(){
        if(IsGrounded()){
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void ChangeMovementState(bool value){
        canMove = value;
    }

    private bool IsGrounded(){
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    private void OnEnable() {
        characterAction.Enable();  

        movement = characterAction.Player.Movement;
        
        characterAction.Player.Jump.performed += _ => DoJump();
    } 

    private void OnDisable() {
        characterAction.Disable();
    }
}
