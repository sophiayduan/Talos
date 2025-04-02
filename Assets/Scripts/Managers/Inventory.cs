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
    public OnItemChanged onItemChanged;
    public int space = 6;
    public List<Items> items = new List<Items>();
    

    public bool Add(Items item){
        if(items.Count >= space){
            Debug.Log("Not enough room");
            return false;
        }
        for(int i = items.Count - 1; i >= 0; i = i - 1){
            if(item == items[i]){
                return false;
            }
        }
        
        items.Add(item);
        if(onItemChanged != null)
            onItemChanged.Invoke();
        Debug.Log("returning true for inventory space");
        return true;

    }

    public void Remove(Items item){
        items.Remove(item);

        if(onItemChanged != null)
            onItemChanged.Invoke();
    }
}
