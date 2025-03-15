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
        public Vector2 MovementInput {get; private set;}
        public Vector2 LookInput {get; private set;}
        public bool JumpPressed {get; private set;}
        public bool PunchPressed {get; private set;}
        private void OnEnable(){
            if(PlayerInputManager.Instance?.InputActions == null){
                Debug.LogError("input actions is not initialized - cannot enable");
                return;
            }

            PlayerInputManager.Instance.InputActions.PlayerLocomotionMap.Enable();
            PlayerInputManager.Instance.InputActions.PlayerLocomotionMap.SetCallbacks(this);
        }
        private void OnDisable(){
            if(PlayerInputManager.Instance?.InputActions == null){
                Debug.LogError("input actions is not initialized - cannot enable");
                return;
            }

            PlayerInputManager.Instance.InputActions.PlayerLocomotionMap.Disable();
            PlayerInputManager.Instance.InputActions.PlayerLocomotionMap.RemoveCallbacks(this);
        }

        private void LateUpdate()
        {
            JumpPressed = false;
            PunchPressed = false;
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

        public void OnPunch(InputAction.CallbackContext context)
        {
            if(!context.performed)
                return;
            
            PunchPressed = true;
        }
    }
}

