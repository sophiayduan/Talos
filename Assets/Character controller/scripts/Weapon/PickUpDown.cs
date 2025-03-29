using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Charactercontroller.scripts{
    public class PickUpDown : MonoBehaviour
    {
        [Header("Ray Settings")]
        [SerializeField][Range(0.0f, 2.0f)] private float rayLength;
        [SerializeField] private Vector3 rayOffset;
        [SerializeField] private LayerMask gunMask;
        [SerializeField] private Transform rightHandPos;
        [SerializeField] private Transform leftHandPos;

        [Header("Right Hand Target")]
        [SerializeField] private TwoBoneIKConstraint rightHandIK;
        [SerializeField] private Transform rightHandTarget;

        [Header("Left Hand Target")]
        [SerializeField] private TwoBoneIKConstraint leftHandIK;
        [SerializeField] private Transform leftHandTarget;

        [SerializeField] private Transform IKRightHandPos;
        [SerializeField] private Transform IKLeftHandPos;
        private RaycastHit topRayHitInfo;
        public bool isAiming = false;
        public bool isShooting = false;
        private Gun currentWeapon;
        
        private PlayerInputs _playerInputs;
        private RigBuilder _rigBuilder;
        private void Awake()
        {
            _rigBuilder = GetComponent<RigBuilder>();
            var rigs = GetComponentInChildren<Rig>();

            _playerInputs = GetComponent<PlayerInputs>();
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.E)){
                Equip();
                if(currentWeapon){
                    if(!isAiming){
                        
                        currentWeapon.pickedUp = true;
                    }
                }
            }
            if(Input.GetKeyDown(KeyCode.Mouse0)){
                isShooting = true;
            }
            if(Input.GetKeyUp(KeyCode.Mouse0)){
                isShooting = false;
            }
            if(currentWeapon){
                
                if(currentWeapon.wasPickedUp){
                    
                    currentWeapon.transform.parent = rightHandPos.transform;
                    currentWeapon.transform.position = rightHandPos.position;
                    currentWeapon.transform.rotation = rightHandPos.rotation;
                    isAiming = true;
                    
                }
                
            }
            
            
        }
        /*void LateUpdate()
        {
            if (currentWeapon) 
            {
                Debug.Log("pick up false");
                currentWeapon.pickedUp = false;
            }
        }
        */
        void FixedUpdate()
        {
            RaycastsHandler();
        }

        private void RaycastsHandler(){
            Ray topRay = new Ray(transform.position + rayOffset, transform.forward);
            Debug.DrawRay(transform.position +rayOffset, transform.forward * rayLength, Color.red);

            Physics.Raycast(topRay, out topRayHitInfo, rayLength, gunMask);
        }

        private void Equip(){
            if(topRayHitInfo.collider != null){
                currentWeapon = topRayHitInfo.transform.gameObject.GetComponent<Gun>();
                
            }

            if(!currentWeapon) return;

        }

        
    }
}

