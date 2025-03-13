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
        public bool WalkToggledOn {get; private set;}
        public InputActions InputActions {get; private set;}
        public Vector2 MovementInput {get; private set;}
        public Vector2 LookInput {get; private set;}
        public bool JumpPressed {get; private set;}
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

        private void LateUpdate()
        {
            JumpPressed = false;
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

        public void OnJump(InputAction.CallbackContext context)
        {
            if(!context.performed)
                return;
            
            JumpPressed = true;
        }

        public void OnToggleWalk(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            WalkToggledOn = !WalkToggledOn;
        }
    }
}

