using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    Items item;
//     public EquipmentManager equipmentManager;

//     void Awake()
//     {
//         equipmentManager = GetComponent<EquipmentManager>();
//     }

    public void AddItem(Items newItem){
        
        item = newItem;
//         Debug.Log("icon"+_item.icon);
//         micon = GetComponent<Image>();
//         if (micon != null) {
            icon.sprite = item.icon;
            icon.enabled = true;
//         }
        removeButton.interactable = true;
    }

    public void ClearSlot(){
        item = null;
        
//         if (micon != null) {
            icon.sprite = null;
            icon.enabled = false;
//         }
        removeButton.interactable = false; 
    }

    public void onRemoveButton(){
        Inventory.instance.Remove(item);
    }

    public void UseItem(){
        if(item != null){
            item.Use(); 
        }
   }
}
