using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    Items _item;
    public EquipmentManager equipmentManager;

    void Awake()
    {
        equipmentManager = GetComponent<EquipmentManager>();
    }

    public void AddItem(Items newItem){
        
        _item = newItem;
        icon.sprite = _item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot(){
        _item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false; 
    }

    public void onRemoveButton(){
        Inventory.instance.Remove(_item);
    }

    public void UseItem(){
        if(_item != null){
            _item.Use();
            
            
            
        }
    }
}
