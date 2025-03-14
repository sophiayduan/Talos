using Charactercontroller.Inputs;
using UnityEngine;

namespace Charactercontroller{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float locomotionBlendSpeed = 0.02f;

        private PlayerInputs _playerinputs;
        private PlayerState _playerState;
        private PlayerController _playerController;

        private static int inputXHash = Animator.StringToHash("inputX");
        private static int inputYHash = Animator.StringToHash("inputY");
        private static int inputMagHash = Animator.StringToHash("inputMag");
        private static int isGroundedHash = Animator.StringToHash("isGrounded");
        private static int isFallingHash = Animator.StringToHash("isFalling");
        private static int isJumpingHash = Animator.StringToHash("isJumping");
        private static int isIdlingHash = Animator.StringToHash("isIdling");
        private static int isPunchingHash = Animator.StringToHash("isPunching");
        private static int isRotatingToTargetHash = Animator.StringToHash("isRotatingToTarget");
        private static int rotationMismatchHash = Animator.StringToHash("rotationMismatch");

        private Vector3 _currentBlendInput = Vector3.zero;

        private void Awake(){
            _playerinputs = GetComponent<PlayerInputs>();
            _playerState = GetComponent<PlayerState>();
            _playerController = GetComponent<PlayerController>();
        }

        private void Update(){
            UpdateAnimationState();
        }

        private void UpdateAnimationState(){
            bool isIdling = _playerState.CurrentPlayerMovementState == PlayerMovementState.Idling;
            bool isRunning = _playerState.CurrentPlayerMovementState == PlayerMovementState.Running;
            bool isSprintng = _playerState.CurrentPlayerMovementState == PlayerMovementState.Sprinting;
            bool isJumping = _playerState.CurrentPlayerMovementState == PlayerMovementState.Jumping;
            bool isFalling = _playerState.CurrentPlayerMovementState == PlayerMovementState.Falling;
            bool isPunching = _playerState.CurrentPlayerMovementState == PlayerMovementState.Punching;
            bool isGrounded = _playerState.InGroundedState();




            Vector2 inputTarget = isSprintng ? _playerinputs.MovementInput * 1.5f : 
                                  isRunning ? _playerinputs.MovementInput * 1f : _playerinputs.MovementInput * 0.5f;
            _currentBlendInput = Vector3.Lerp(_currentBlendInput, inputTarget, locomotionBlendSpeed * Time.deltaTime);
            
            _animator.SetBool(isGroundedHash, isGrounded);
            _animator.SetBool(isPunchingHash, isPunching);
            _animator.SetBool(isFallingHash, isFalling);
            _animator.SetBool(isJumpingHash, isJumping);
            _animator.SetBool(isIdlingHash, isIdling);
            _animator.SetBool(isRotatingToTargetHash, _playerController.IsRotatingToTarget);
            _animator.SetFloat(inputXHash, _currentBlendInput.x);
            _animator.SetFloat(inputYHash, _currentBlendInput.y);
            _animator.SetFloat(inputMagHash, _currentBlendInput.magnitude);
            _animator.SetFloat(rotationMismatchHash, _playerController.RotationMismatch);

        }
    }

}

