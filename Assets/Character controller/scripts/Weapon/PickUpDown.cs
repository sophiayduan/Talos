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


        private RaycastHit topRayHitInfo;
        public bool isAiming = false;
        public bool isShooting = false;
        public bool hasWeapon = false;
        public Gun currentWeapon;
        
        private PlayerInputs _playerInputs;
        private InventorySlot _inventorySlot;
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
                    //if(!isAiming){
                        currentWeapon.pickedUp = true;
                    //}
                    

                    /*
                    if(currentWeapon.wasPickedUp && isAiming){
                        Destroy(currentWeapon.gameObject);
                        hasWeapon = false;
                        isAiming = false;
                        Debug.Log("Aiming is false");
                    }
                    */
                }
                
            }
            if(Input.GetKeyDown(KeyCode.Mouse0)){
                isShooting = true;
            }
            if(Input.GetKeyUp(KeyCode.Mouse0)){
                isShooting = false;
            }
            if(currentWeapon){
                Debug.Log("current weapon");
                
                //if(currentWeapon.inventorySpace){
                    if(currentWeapon.transform.position == rightHandPos.position){
                        isAiming = true;
                        Debug.Log("Aiming is true");
                    }
                //}    
                  
            }
            if(_inventorySlot.isAiming){
                hasWeapon = true;
                Debug.Log("hasWeapon is true");
            }
        }
        void LateUpdate()
        {
            //if(isAiming){
            //    hasWeapon = true;
            //}
        }
        
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
                Debug.Log("equip is running when there is a weapon");
            }

            if(!currentWeapon) return;

        }

        public void PickUpTransform(){
            currentWeapon.transform.parent = rightHandPos.transform;
            currentWeapon.transform.position = rightHandPos.position;
            currentWeapon.transform.rotation = rightHandPos.rotation;
            //isAiming = true;
            Debug.Log("Aiming is true");
        }

        
    }
}

