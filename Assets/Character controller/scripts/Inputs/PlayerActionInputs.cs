using Charactercontroller.Inputs;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Charactercontroller
{
    [DefaultExecutionOrder(-2)]
    public class PlayerActionInputs : MonoBehaviour, InputActions.IPlayerActionMapActions
    {
        public bool AttackPressed {get; private set;}
        public bool GrabPressed {get; private set;}

        private void OnEnable(){
            if(PlayerInputManager.Instance?.InputActions == null){
                Debug.LogError("input actions is not initialized - cannot enable");
                return;
            }

            PlayerInputManager.Instance.InputActions.PlayerActionMap.Enable();
            PlayerInputManager.Instance.InputActions.PlayerActionMap.SetCallbacks(this);
        }
        private void OnDisable(){
            if(PlayerInputManager.Instance?.InputActions == null){
                Debug.LogError("input actions is not initialized - cannot enable");
                return;
            }

            PlayerInputManager.Instance.InputActions.PlayerActionMap.Disable();
            PlayerInputManager.Instance.InputActions.PlayerActionMap.RemoveCallbacks(this);
        }
        private void Update()
        {
            
        }
        public void SetGrabPressedFalse(){
            GrabPressed = false;
        }
        public void SetPunchPressedFalse(){
            AttackPressed = false;
        }
        public void OnAttack(InputAction.CallbackContext context)
        {
            if(!context.performed)
                return;

            AttackPressed = true;
        }

        public void OnGrab(InputAction.CallbackContext context)
        {
            if(!context.performed)
                return;

            GrabPressed = true;
        }
    }
}
