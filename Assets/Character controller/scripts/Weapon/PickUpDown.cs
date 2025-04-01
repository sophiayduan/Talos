using UnityEngine;


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
        
        private Gun currentWeapon;
        public GameObject RedGun;
        public GameObject BlueGun;
        
        private PlayerInputs _playerInputs;
        private InventorySlot _inventorySlot;
        
        private void Awake()
        {
            
            

            _playerInputs = GetComponent<PlayerInputs>();
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.E)){
                Equip();
                if(currentWeapon){
                    
                        currentWeapon.pickedUp = true;
                    
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
            }
                
                //if(currentWeapon.inventorySpace){
            if(RedGun.gameObject.transform.position == rightHandPos.position || BlueGun.gameObject.transform.position == rightHandPos.position){
                isAiming = true;
                Debug.Log("Aiming is true");
            }
            if(!RedGun.gameObject.activeSelf && !BlueGun.gameObject.activeSelf){
                isAiming = false;
            }
                //}
                  
            
           
        }
        void LateUpdate()
        {
            
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

