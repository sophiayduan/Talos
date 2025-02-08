using UnityEngine;
using UnityEngine.InputSystem;

namespace Charactercontroller {
    public class PlayerController : MonoBehaviour
    {
      [SerializeField] private CharacterController _characterController;
      [SerializeField] private Camera _playerCamera;
      public float runAcceleration = 0.25f;
      public float runSpeed = 4f;
      public float drag = 0.1f;

      private PlayerInputs _playerLocomotionInput;
      private void Awake(){
        _playerLocomotionInput = GetComponent<PlayerInputs>();
      }
      private void Update(){
        Vector3 cameraForwardXZ = new Vector3(_playerCamera.transform.forward.x, 0f, _playerCamera.transform.forward.z).normalized;
        Vector3 cameraRightXZ = new Vector3(_playerCamera.transform.right.x, 0f, _playerCamera.transform.right.z).normalized;
        Vector3 movementDirection = cameraRightXZ * _playerLocomotionInput.MovementInput.x + cameraForwardXZ * _playerLocomotionInput.MovementInput.y;

        Vector3 movementDelta = movementDirection * runAcceleration * Time.deltaTime;
        Vector3 newVelocity = _characterController.velocity + movementDelta;

        Vector3 currentDrag = newVelocity.normalized * drag * Time.deltaTime;
        newVelocity = (newVelocity.magnitude > drag * Time.deltaTime) ? newVelocity - currentDrag : Vector3.zero;

        _characterController.Move(newVelocity * Time.deltaTime);
      }
    }
}


