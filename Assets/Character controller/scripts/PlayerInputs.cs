using Charactercontroller.Inputs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Charactercontroller
{
    [DefaultExecutionOrder(-2)]
    public class PlayerInputs : MonoBehaviour, InputActions.IPlayerLocomotionMapActions
    {
        [SerializeField] private bool holdToSprint = true;

        public bool SprintToggledOn {get; private set;}
        public InputActions InputActions {get; private set;}
        public Vector2 MovementInput {get; private set;}
        public Vector2 LookInput {get; private set;}
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
            
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            LookInput = context.ReadValue<Vector2>();
        }

        public void OnToggleSprint(InputAction.CallbackContext context)
        {
            if (context.performed){
                SprintToggledOn = holdToSprint || !SprintToggledOn;   
            }
            else if (context.canceled){
                SprintToggledOn = !holdToSprint && SprintToggledOn;
            }
        }
    }
}

