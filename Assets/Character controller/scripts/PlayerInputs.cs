using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Charactercontroller
{
    public class PlayerInputs : MonoBehaviour, InputActions.IPlayerLocomotionMapActions
    {
        public InputActions InputActions {get; private set;}
        public Vector2 MovementInput {get; private set;}
        private void OnEnable(){
            InputActions = new InputActions();
            InputActions.Enable();

            InputActions.PlayerLocomotionMap.Enable();
            InputActions.PlayerLocomotionMap.SetCallbacks(this);
        }
        private void OnDisable(){
            InputActions.PlayerLocomotionMap.Disable();
            InputActions.PlayerLocomotionMap.RemoveCallbacks(this);
        }
        public void OnMovement(InputAction.CallbackContext context)
        {
            MovementInput = context.ReadValue<Vector2>();
            print(MovementInput);
        }
    }
}

