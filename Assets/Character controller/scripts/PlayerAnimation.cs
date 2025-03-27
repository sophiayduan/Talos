using System.Linq;
using Charactercontroller.Inputs;
using Charactercontroller.scripts;
using UnityEngine;

namespace Charactercontroller{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float locomotionBlendSpeed = 0.02f;

        private PlayerInputs _playerinputs;
        private PlayerState _playerState;
        private PlayerController _playerController;
        private PlayerActionInputs _playerActionInputs;
        private PickUpDown _pickUpDown;

        private static int inputXHash = Animator.StringToHash("inputX");
        private static int inputYHash = Animator.StringToHash("inputY");
        private static int inputMagHash = Animator.StringToHash("inputMag");
        private static int isGroundedHash = Animator.StringToHash("isGrounded");
        private static int isFallingHash = Animator.StringToHash("isFalling");
        private static int isJumpingHash = Animator.StringToHash("isJumping");
        private static int isIdlingHash = Animator.StringToHash("isIdling");
        private static int isPunchingHash = Animator.StringToHash("isPunching");
        private static int isGrabingHash = Animator.StringToHash("isGrabing");
        private static int isPlayingActionsHash = Animator.StringToHash("isPlayingAction");
        private static int isAimingHash = Animator.StringToHash("isAiming");
        private static int isShootingHash = Animator.StringToHash("isShooting");
        private int[] actionHashes;
        private static int isRotatingToTargetHash = Animator.StringToHash("isRotatingToTarget");
        private static int rotationMismatchHash = Animator.StringToHash("rotationMismatch");


        private Vector3 _currentBlendInput = Vector3.zero;

        private float _sprintMaxBlendValue = 1.5f;
        private float _runMaxBlaendValue = 1f;
        private float _walkMaxBlendValue = 0.5f;

        private void Awake(){
            _playerinputs = GetComponent<PlayerInputs>();
            _playerState = GetComponent<PlayerState>();
            _playerController = GetComponent<PlayerController>();
            _playerActionInputs = GetComponent<PlayerActionInputs>();
            _pickUpDown = GetComponent<PickUpDown>();

            actionHashes = new int[] {isGrabingHash, isFallingHash};
        }

        private void Update(){
            UpdateAnimationState();
        }

        private void UpdateAnimationState(){
            bool isIdling = _playerState.CurrentPlayerMovementState == PlayerMovementState.Idling;
            bool isRunning = _playerState.CurrentPlayerMovementState == PlayerMovementState.Running;
            bool isSprintng = _playerState.CurrentPlayerMovementState == PlayerMovementState.Sprinting;
            bool isJumping = _playerState.CurrentPlayerMovementState == PlayerMovementState.Jumping;
            bool isGrounded = _playerState.InGroundedState();
            bool isFalling = _playerState.CurrentPlayerMovementState == PlayerMovementState.Falling && !isGrounded;
            bool isPlayingAction = actionHashes.Any(hash => _animator.GetBool(hash));
            bool isShooting = _playerActionInputs.AttackPressed;

            bool isRunBlendValue = isRunning || isJumping || isFalling;


            Vector2 inputTarget = isSprintng ? _playerinputs.MovementInput * _sprintMaxBlendValue : 
                                  isRunBlendValue ? _playerinputs.MovementInput * _runMaxBlaendValue : _playerinputs.MovementInput * _walkMaxBlendValue;
            _currentBlendInput = Vector3.Lerp(_currentBlendInput, inputTarget, locomotionBlendSpeed * Time.deltaTime);
            
            _animator.SetBool(isGroundedHash, isGrounded);
            _animator.SetBool(isFallingHash, isFalling);
            _animator.SetBool(isJumpingHash, isJumping);
            _animator.SetBool(isIdlingHash, isIdling);
            _animator.SetBool(isPlayingActionsHash, isPlayingAction);
            _animator.SetBool(isRotatingToTargetHash, _playerController.IsRotatingToTarget);
            _animator.SetFloat(inputXHash, _currentBlendInput.x);
            _animator.SetFloat(inputYHash, _currentBlendInput.y);
            _animator.SetFloat(inputMagHash, _currentBlendInput.magnitude);
            _animator.SetFloat(rotationMismatchHash, _playerController.RotationMismatch);
            _animator.SetBool(isPunchingHash, _playerActionInputs.AttackPressed);
            _animator.SetBool(isGrabingHash, _playerActionInputs.GrabPressed);
            _animator.SetBool(isAimingHash, _pickUpDown.isAiming);
            _animator.SetBool(isShootingHash, _pickUpDown.isShooting);
        }
    }

}

