using UnityEngine;

namespace Charactercontroller.scripts{
    public class PickUpDown : MonoBehaviour
    {
        [Header("Ray Settings")]
        [SerializeField][Range(0.0f, 2.0f)] private float rayLength;
        [SerializeField] private Vector3 rayOffset;
        [SerializeField] private LayerMask gunMask;
        [SerializeField] private Transform equipPos;
        [SerializeField] private Transform aimingPos;

        private RaycastHit topRayHitInfo;
        public bool isAiming = false;
        private Gun currentWeapon;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.E)){
                Equip();
            }
            if(currentWeapon){
                currentWeapon.transform.parent = equipPos.transform;
                currentWeapon.transform.position = equipPos.position;
                currentWeapon.transform.rotation = equipPos.rotation;
                isAiming = true;
            }
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
            }

            if(!currentWeapon) return;

        }

        
    }
}

