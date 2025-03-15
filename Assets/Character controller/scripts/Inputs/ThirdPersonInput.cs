using Charactercontroller.Inputs;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Charactercontroller
{
    [DefaultExecutionOrder(-2)]
    public class ThirdPersonInputs : MonoBehaviour, InputActions.IThirdPersonMapActions
    {
        public Vector2 ScrollInput {get; private set;}

        [SerializeField] private CinemachineCamera _virtualCamera;
        [SerializeField] private float _cameraZoomSpeed = 0.2f;
        [SerializeField] private float _cameraMinZoom = 1f;
        [SerializeField] private float _cameraMaxZoom = 5f;

        private CinemachineThirdPersonFollow _thirdPersonFollow;

        private void Awake()
        {
            _thirdPersonFollow = _virtualCamera.GetComponent<CinemachineThirdPersonFollow>();
        }
        private void OnEnable(){
            if(PlayerInputManager.Instance?.InputActions == null){
                Debug.LogError("input actions is not initialized - cannot enable");
                return;
            }

            PlayerInputManager.Instance.InputActions.ThirdPersonMap.Enable();
            PlayerInputManager.Instance.InputActions.ThirdPersonMap.SetCallbacks(this);
        }
        private void OnDisable(){
            if(PlayerInputManager.Instance?.InputActions == null){
                Debug.LogError("input actions is not initialized - cannot enable");
                return;
            }

            PlayerInputManager.Instance.InputActions.ThirdPersonMap.Disable();
            PlayerInputManager.Instance.InputActions.ThirdPersonMap.RemoveCallbacks(this);
        }
        private void Update()
        {
            _thirdPersonFollow.CameraDistance = Mathf.Clamp(_thirdPersonFollow.CameraDistance + ScrollInput.y, _cameraMinZoom, _cameraMaxZoom);
        }
        private void LateUpdate()
        {
            ScrollInput = Vector2.zero;
        }

        public void OnScrollCamera(InputAction.CallbackContext context)
        {
            if(!context.performed)
                return;

            Vector2 scrollInput = context.ReadValue<Vector2>();
            ScrollInput = -1f * scrollInput.normalized * _cameraZoomSpeed;
            
        
        }
        
    }
}
