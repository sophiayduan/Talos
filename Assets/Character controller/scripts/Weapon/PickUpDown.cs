using UnityEngine;

namespace Charactercontroller.scripts.Weapons{
    public class PickUpDown : MonoBehaviour
    {
        [SerializeField] private Transform playerCameraTransform;
        [SerializeField] private Transform objectGrabPointTransform;
        [SerializeField] private LayerMask pickUpLayerMask;
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.E)){
                float pickUpDistance = 2f;
                if(Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask)){
                    if (raycastHit.transform.TryGetComponent(out GrabableObject objectGrabbable)){
                        objectGrabbable.Grab(objectGrabPointTransform);
                    }
                }
            }
        }
    }
}

