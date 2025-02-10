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
        private Vector3 _currentBlendInput = Vector3.zero;

        private void Awake(){
            _playerinputs = GetComponent<PlayerInputs>();
            _playerState = GetComponent<PlayerState>();
        }

        private void Update(){
            UpdateAnimationState();
        }

        private void UpdateAnimationState(){
            Vector2 inputTarget = _playerinputs.MovementInput;
            _currentBlendInput = Vector3.Lerp(_currentBlendInput, inputTarget, locomotionBlendSpeed * Time.deltaTime);
            
            _animator.SetFloat(inputXHash, inputTarget.x);
            _animator.SetFloat(inputYHash, inputTarget.y);

        }
    }

}

