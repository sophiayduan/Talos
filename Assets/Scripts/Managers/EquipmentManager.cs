using Charactercontroller.scripts;
using InventorySystem;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;
    }
    [SerializeField] private Transform rightHandPos;
    private PickUpDown pickUpDown;
    private Gun weapon;
    Equipment[] currentEquipment;
    MeshRenderer[] currentMeshes;

    //public bool isAiming;
    public GameObject Slot1;
    public GameObject Slot2;
    
   
    //public List<Gun> guns = new List<Gun>();
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;
    Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        //currentMeshes = new MeshRenderer[numSlots];
    }

    public void Equip(Equipment newItem){
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;
        if(currentEquipment[slotIndex] != null){
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
            //isAiming = true;
        }

        if(onEquipmentChanged != null){
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;
        
        //isAiming = true;
        
        if(newItem.name == "Red Gun"){
            Slot1.SetActive(true);
            Slot2.SetActive(false);
            

            Slot1.gameObject.transform.parent = rightHandPos.transform;
            Slot1.gameObject.transform.position = rightHandPos.position;
            Slot1.gameObject.transform.rotation = rightHandPos.rotation;
            
        }
        
        
        
        
        //pickUpDown.PickUpTransform();
    }

    public void Unequip (int slotIndex){
        if(currentEquipment[slotIndex] != null){
        

            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if(onEquipmentChanged != null){
            onEquipmentChanged.Invoke(null, oldItem);
        }
        }
        
    }

    public void UnequipAll(){
        for(int i = 0; i < currentEquipment.Length; i++){
            Unequip(i);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U)){
            UnequipAll();
        }

        if(currentEquipment[0] != null){
            //isAiming = true;
        }
    }
}
