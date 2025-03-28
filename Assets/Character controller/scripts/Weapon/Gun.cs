using UnityEngine;

public class Gun : MonoBehaviour
{
    private Rigidbody gunBody;

    void Start()
    {
        gunBody = GetComponent<Rigidbody>();

        if(gunBody){
            gunBody.isKinematic = true;
        }
    }



}
