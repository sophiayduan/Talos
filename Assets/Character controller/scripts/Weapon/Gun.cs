using JetBrains.Annotations;
using UnityEngine;

public class Gun : MonoBehaviour
{


    private Rigidbody gunBody;
    public Items item;
    public bool pickedUp;
    public bool wasPickedUp;
    void Start()
    {
        gunBody = GetComponent<Rigidbody>();

        if(gunBody){
            gunBody.isKinematic = true;
        }
    }
    void Update()
    {
        if(pickedUp == true){
            Debug.Log("Picked up" + item.name);
            wasPickedUp = Inventory.instance.Add(item);
            pickedUp = false;
        }
    }



}
