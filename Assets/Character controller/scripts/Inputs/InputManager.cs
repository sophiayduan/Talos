using Charactercontroller.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Charactercontroller{
    [DefaultExecutionOrder(-3)]
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager Instance;

        public InputActions InputActions {get; private set;}
    

        private void Awake()
        {
            if(Instance != null && Instance != this){
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            InputActions = new InputActions();
            InputActions.Enable();
        }

        private void OnDisable()
        {
            InputActions.Disable();
        }

    }
}
