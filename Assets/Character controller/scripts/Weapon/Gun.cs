
using UnityEngine;

public class Gun : MonoBehaviour
{


    private Rigidbody gunBody;
    public Items item;
    public bool pickedUp;
    public bool inventorySpace;
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
            if(item == Inventory.instance){
                return;
            }
            Debug.Log("Picked up" + item.name);
            inventorySpace = Inventory.instance.Add(item);
           
            pickedUp = false;
        }
    }
}
