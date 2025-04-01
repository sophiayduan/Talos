using System;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.InputSystem;
using Unity.Cinemachine;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace Charactercontroller {
    [DefaultExecutionOrder(-1)]
    public class PlayerController : MonoBehaviour
    {
      [Header("Components")]
      [SerializeField] private CharacterController _characterController;
      [SerializeField] private Camera _playerCamera; 
      public float RotationMismatch {get; private set;} = 0f;
      public bool IsRotatingToTarget {get; private set;} = false;   

      
      
      
      [Header("Base Movement")]
      public float walkAcceleration = 0.15f;
      public float walkSpeed = 3f;
      public float runSpeed = 6f;
      public float runAcceleration = 0.25f;
      public float sprintAcceleration = 0.5f;
      public float sprintSpeed = 9f;
      public float inAirAcceleration = 0.15f;
      public float drag = 0.1f;
      public float movingThreshold = 0.01f;
      public float gravity = 25f;
      public float terminalVelocity = 50f;
      public float jumpSpeed = 1f;
      [Header("Animation")]
      public float playerModelRotationSpeed = 10f;
      public float rotateToTargetTime = 0.25f;

      [Header("Camera Settings")]
      public float lookSenseH = 0.1f;
      public float lookSenseV = 0.1f;
      public float lookLimitV = 89f;

      [Header("Environmental Details")]
      [SerializeField] private LayerMask _groundLayers;
      
      private PlayerInputs _playerLocomotionInput;
      private PlayerState _playerState;

      private Vector2 _cameraRotation = Vector2.zero;
      private Vector2 _playerTargetRotation = Vector2.zero;

      private bool _jumpedLastFrame = false;
      private bool _isRotatingClockwise = false;
      private float _rotatingToTargetTimer = 0f;
      private float _verticalVelocity = 0f;
      private float _antiBump;
      private float _stepOffset;


      private PlayerMovementState _lastMovementState = PlayerMovementState.Falling;

      private void Awake(){
        _playerLocomotionInput = GetComponent<PlayerInputs>();
        _playerState = GetComponent<PlayerState>();
        _antiBump = sprintSpeed;
        _stepOffset = _characterController.stepOffset;
      }

      private void Update(){
        
        UpdateMovementState();
        HandleVerticalMovment();
        HandleLateralMovement();
        
      }
      
      private void UpdateMovementState(){
        _lastMovementState = _playerState.CurrentPlayerMovementState;

        bool canRun = CanRun();
        bool isMovementInput = _playerLocomotionInput.MovementInput != Vector2.zero;
        bool isMovingLaterally = IsMovingLaterally();
        bool isSprintng = _playerLocomotionInput.SprintToggledOn && isMovingLaterally;
        bool isWalking = (isMovingLaterally && !canRun) || _playerLocomotionInput.WalkToggledOn;
        bool isPunching = _playerLocomotionInput.PunchPressed;
        bool isGrounded = IsGrounded();

        PlayerMovementState lateralState = isWalking ? PlayerMovementState.Walking :
                                           isSprintng ? PlayerMovementState.Sprinting :
                                           isPunching ? PlayerMovementState.Punching :
                                           isMovingLaterally || isMovementInput ? PlayerMovementState.Running : PlayerMovementState.Idling;
        _playerState.SetPlayerMovementState(lateralState);

        if ((!isGrounded || _jumpedLastFrame) && _characterController.velocity.y >= 0f){
          _playerState.SetPlayerMovementState(PlayerMovementState.Jumping);
          _jumpedLastFrame = false;
          _characterController.stepOffset = 0f;
        }
        else if ((!isGrounded || _jumpedLastFrame) && _characterController.velocity.y < 0f){
          _playerState.SetPlayerMovementState(PlayerMovementState.Falling);
          
        }
        else{
          _characterController.stepOffset = _stepOffset;
          
        }
      }

      private void HandleVerticalMovment(){
        bool isGrounded = _playerState.InGroundedState();

        _verticalVelocity -= gravity * Time.deltaTime;
        if (isGrounded && _verticalVelocity < 0)
          _verticalVelocity = -_antiBump;


        if(_playerLocomotionInput.JumpPressed && isGrounded){
          _verticalVelocity += Mathf.Sqrt(jumpSpeed * 3 * gravity);
          _jumpedLastFrame = true;
        }

        if(_playerState.IsStateGroundedState(_lastMovementState) && !isGrounded){
          _verticalVelocity += _antiBump; 
        }

        if(Mathf.Abs(_verticalVelocity) > Mathf.Abs(terminalVelocity)){
          _verticalVelocity = -1f * Math.Abs(terminalVelocity);
        }
      }

      private void HandleLateralMovement(){
        bool isSprintng = _playerState.CurrentPlayerMovementState == PlayerMovementState.Sprinting;
        bool isGrounded = _playerState.InGroundedState();
        bool isWalking = _playerState.CurrentPlayerMovementState == PlayerMovementState.Walking;

        float lateralAcceleration = !isGrounded ? inAirAcceleration :
                                    isWalking ? walkAcceleration : 
                                    isSprintng ? sprintAcceleration : runAcceleration;
        float clampLateralMagnitude = !isGrounded ? sprintSpeed :
                                      isWalking ? walkSpeed : 
                                      isSprintng ? sprintSpeed : runSpeed;

        Vector3 cameraForwardXZ = new Vector3(_playerCamera.transform.forward.x, 0f, _playerCamera.transform.forward.z).normalized;
        Vector3 cameraRightXZ = new Vector3(_playerCamera.transform.right.x, 0f, _playerCamera.transform.right.z).normalized;
        Vector3 movementDirection = cameraRightXZ * _playerLocomotionInput.MovementInput.x + cameraForwardXZ * _playerLocomotionInput.MovementInput.y;

        Vector3 movementDelta = movementDirection * lateralAcceleration * Time.deltaTime;
        Vector3 newVelocity = _characterController.velocity + movementDelta;

        Vector3 currentDrag = newVelocity.normalized * drag * Time.deltaTime;
        newVelocity = (newVelocity.magnitude > drag * Time.deltaTime) ? newVelocity - currentDrag : Vector3.zero;
        newVelocity = Vector3.ClampMagnitude(new Vector3(newVelocity.x, 0f, newVelocity.z), clampLateralMagnitude);
        newVelocity.y += _verticalVelocity;
        newVelocity = !isGrounded ? HandleSteepWalls(newVelocity) : newVelocity;

        _characterController.Move(newVelocity * Time.deltaTime);
      }
      
      
      private void LateUpdate(){
       UpdateCameraRotation();
      }

      private void UpdateCameraRotation(){
         _cameraRotation.x += lookSenseH * _playerLocomotionInput.LookInput.x;
        _cameraRotation.y = Mathf.Clamp(_cameraRotation.y - lookSenseV * _playerLocomotionInput.LookInput.y, -lookLimitV, lookLimitV);

        _playerTargetRotation.x += transform.eulerAngles.x + lookSenseH * _playerLocomotionInput.LookInput.x;
        
        float rotationTolerence = 0f;
        bool isIdling = _playerState.CurrentPlayerMovementState == PlayerMovementState.Idling;
        IsRotatingToTarget = _rotatingToTargetTimer > 0;
        //rotate when not idling
        if (!isIdling){

          RotatePlayerToTarget();

        }
        //if rotation mismatch not within tolerance or rotate to target timer is active, ROTATE
        else if(Mathf.Abs(RotationMismatch) > rotationTolerence || IsRotatingToTarget){
          UpdateIdleRotation(rotationTolerence);
        }

        _playerCamera.transform.rotation = Quaternion.Euler(_cameraRotation.y, _cameraRotation.x, 0f);
        // i dont understand, pls understand
        Vector3 camFowardProjectedXZ = new Vector3(_playerCamera.transform.forward.x, 0f, _playerCamera.transform.forward.z).normalized;
        Vector3 crossProduct = Vector3.Cross(transform.forward, camFowardProjectedXZ);
        float sign = Mathf.Sign(Vector3.Dot(crossProduct, transform.up));
        RotationMismatch = sign * Vector3.Angle(transform.forward, camFowardProjectedXZ);
      }
      private void UpdateIdleRotation(float rotationTolerence){
                  
        //initiate new rotation direction
        if (Mathf.Abs(RotationMismatch) > rotationTolerence){

          _rotatingToTargetTimer = rotateToTargetTime;
          _isRotatingClockwise = RotationMismatch > rotationTolerence;
        }
        _rotatingToTargetTimer -= Time.deltaTime;
        
        //rotate player
        if(_isRotatingClockwise && RotationMismatch > 0f || !_isRotatingClockwise && RotationMismatch < 0f){
          RotatePlayerToTarget();
        }
        
      }
      private void RotatePlayerToTarget(){
        Quaternion targetRotationX = Quaternion.Euler(0f, _playerTargetRotation.x, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotationX, playerModelRotationSpeed * Time.deltaTime);
      }

      private Vector3 HandleSteepWalls(Vector3 velocity){
        Vector3 normal = CharacterControllerUtils.GetNormalWithSphereCast(_characterController, _groundLayers);
        float angle = Vector3.Angle(normal, Vector3.up);
        bool validAngle = angle <= _characterController.slopeLimit;

        if(!validAngle && _verticalVelocity < 0f){
          velocity = Vector3.ProjectOnPlane(velocity, normal);
        }

        return velocity;
      }
      private bool IsMovingLaterally(){
        Vector3 LateralVelocity = new Vector3(_characterController.velocity.x, 0f, _characterController.velocity.y);
        return LateralVelocity.magnitude > movingThreshold;
      }

      private bool IsGrounded(){
        bool grounded = _playerState.InGroundedState() ? IsGroundedWhileGrounded() : IsGroundedWhileAirborne();
        return grounded;
      }

      private bool IsGroundedWhileGrounded()
      {
        Vector3 shpherePosition = new Vector3(transform.position.x, transform.position.y - _characterController.radius, transform.position.z);
        bool grounded = Physics.CheckSphere(shpherePosition, _characterController.radius, _groundLayers, QueryTriggerInteraction.Ignore);
        return grounded;
      }

      private bool IsGroundedWhileAirborne(){
        Vector3 normal = CharacterControllerUtils.GetNormalWithSphereCast(_characterController, _groundLayers);
        float angle = Vector3.Angle(normal, Vector3.up);
        bool validAngle = angle <= _characterController.slopeLimit;

        return _characterController.isGrounded && validAngle;
      }

      private bool CanRun(){
        //if player is moving diagonally at 45 or forward it can run
        return _playerLocomotionInput.MovementInput.y >= Mathf.Abs(_playerLocomotionInput.MovementInput.x);
      }
    }
}
