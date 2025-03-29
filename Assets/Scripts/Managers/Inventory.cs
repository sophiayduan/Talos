using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    void Awake()
    {
        if(instance != null){
            Debug.LogWarning("More than one instance of inventory");
            return;
        }

        instance = this;
    }

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;
    public int space = 6;
    public List<Items> items = new List<Items>();

    public bool Add(Items item){
        if(items.Count >= space){
            Debug.Log("Not enough room");
            return false;
        }
        items.Add(item);
        if(onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
        
        return true;

    }

    public void Remove(Items item){
        items.Remove(item);

        if(onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }
}
