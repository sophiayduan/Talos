using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    public Image micon;
    public Button removeButton;
    Items _item;
    public EquipmentManager equipmentManager;

    void Awake()
    {
        equipmentManager = GetComponent<EquipmentManager>();
    }

    public void AddItem(Items newItem){
        
        _item = newItem;
        Debug.Log("icon"+_item.icon);
        micon = GetComponent<Image>();
        if (micon != null) {
            micon.sprite = _item.icon;
            micon.enabled = true;
        }
        removeButton.interactable = true;
    }

    public void ClearSlot(){
        _item = null;
        
        if (micon != null) {
            micon.sprite = null;
            micon.enabled = false;
        }
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
