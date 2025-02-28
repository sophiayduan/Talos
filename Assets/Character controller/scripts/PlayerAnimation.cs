using Charactercontroller.Inputs;
using UnityEngine;

namespace Charactercontroller{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float locomotionBlendSpeed = 0.02f;

        private PlayerInputs _playerinputs;
        private PlayerState _playerState;

        private static int inputXHash = Animator.StringToHash("inputX");
        private static int inputYHash = Animator.StringToHash("inputY");
        private static int inputMagHash = Animator.StringToHash("inputMag");

        private Vector3 _currentBlendInput = Vector3.zero;

        private void Awake(){
            _playerinputs = GetComponent<PlayerInputs>();
            _playerState = GetComponent<PlayerState>();
        }

        private void Update(){
            UpdateAnimationState();
        }

        private void UpdateAnimationState(){
            bool isSprintng = _playerState.CurrentPlayerMovementState == PlayerMovementState.Sprinting;

            Vector2 inputTarget = isSprintng ? _playerinputs.MovementInput * 1.5f : _playerinputs.MovementInput;
            _currentBlendInput = Vector3.Lerp(_currentBlendInput, inputTarget, locomotionBlendSpeed * Time.deltaTime);
            
            _animator.SetFloat(inputXHash, _currentBlendInput.x);
            _animator.SetFloat(inputYHash, _currentBlendInput.y);
            _animator.SetFloat(inputMagHash, _currentBlendInput.magnitude);

        }
    }

}

