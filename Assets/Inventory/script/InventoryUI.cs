using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    Inventory inventory;

    InventorySlot[] slots;
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        

//         // if(Input.GetButtonDown("Inventory")){
//         //     inventoryUI.SetActive(!inventoryUI.activeSelf);
//         // }
    }

    void UpdateUI(){
        for(int i = 0; i < slots.Length; i++){
//             Debug.Log("Inventory : Count" + inventory.items.Count + inventory.items[i]);
            if(i < inventory.items.Count){
//                 Debug.Log("adding item i" + i + slots[i]);
                slots[i].AddItem(inventory.items[i]);
                Debug.Log("adding item in UI");
            } else{
                slots[i].ClearSlot();
            }
        }
    }
}
