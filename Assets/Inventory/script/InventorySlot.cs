using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    public Image icon;
    Items _item;

    public void AddItem(Items newItem){
        _item = newItem;
        icon.sprite = _item.icon;
        icon.enabled = true;
    }

    public void ClearSlot(){
        _item = null;

        icon.sprite = null;
        icon.enabled = false;
    }
}
