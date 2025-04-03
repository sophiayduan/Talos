using Charactercontroller.scripts;

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
    public GameObject RedGun;
    public GameObject BlueGun;
    public GameObject MachineGun;
    public GameObject Revolver;
    public GameObject RocketLauncher;
    
   
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
            RedGun.SetActive(true);
            
            RedGun.gameObject.transform.parent = rightHandPos.transform;
            RedGun.gameObject.transform.position = rightHandPos.position;
            RedGun.gameObject.transform.rotation = rightHandPos.rotation;
        }else if(RedGun.gameObject.transform.position != rightHandPos.position){
            RedGun.SetActive(true);
        }else{
            RedGun.SetActive(false);
        }

        /*
        else if(oldItem.name == "Red Gun"){
            RedGun.SetActive(false);
        }else{
            return;
        }
        */

        if(newItem.name == "Blue Gun"){
            BlueGun.SetActive(true);
            
            BlueGun.gameObject.transform.parent = rightHandPos.transform;
            BlueGun.gameObject.transform.position = rightHandPos.position;
            BlueGun.gameObject.transform.rotation = rightHandPos.rotation;
        }else if(BlueGun.gameObject.transform.position != rightHandPos.position){
            BlueGun.SetActive(true);
        }else{
            BlueGun.SetActive(false);
        }

        if(newItem.name == "Machine Gun"){
            MachineGun.SetActive(true);
            
            MachineGun.gameObject.transform.parent = rightHandPos.transform;
            MachineGun.gameObject.transform.position = rightHandPos.position;
            MachineGun.gameObject.transform.rotation = rightHandPos.rotation;
        }else if(MachineGun.gameObject.transform.position != rightHandPos.position){
            MachineGun.SetActive(true);
        }else{
            MachineGun.SetActive(false);
        }

        if(newItem.name == "Revolver"){
            Revolver.SetActive(true);
            
            Revolver.gameObject.transform.parent = rightHandPos.transform;
            Revolver.gameObject.transform.position = rightHandPos.position;
            Revolver.gameObject.transform.rotation = rightHandPos.rotation;
        }else if(Revolver.gameObject.transform.position != rightHandPos.position){
            Revolver.SetActive(true);
        }else{
            Revolver.SetActive(false);
        }

        if(newItem.name == "Rocket Launcher"){
            RocketLauncher.SetActive(true);
            
            RocketLauncher.gameObject.transform.parent = rightHandPos.transform;
            RocketLauncher.gameObject.transform.position = rightHandPos.position;
            RocketLauncher.gameObject.transform.rotation = rightHandPos.rotation;
        }else if(RocketLauncher.gameObject.transform.position != rightHandPos.position){
            RocketLauncher.SetActive(true);
        }else{
            RocketLauncher.SetActive(false);
        }
        /*
        
        if(currentEquipment[0] == BlueGun){
            BlueGun.SetActive(false);
        }
        if(currentEquipment[0] == RedGun){
            RedGun.SetActive(false);
        }
        
        */
//bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb
        /*
        else if(oldItem.name == "Blue Gun"){
            BlueGun.SetActive(false);
        }else{
            return;
        }
        */
        
        
        //pickUpDown.PickUpTransform();
    }

    public void Unequip (int slotIndex){
        if(currentEquipment[slotIndex] != null){
        

            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            
            if(oldItem.name == "Red Gun"){
                RedGun.SetActive(false);
                BlueGun.SetActive(true);
                MachineGun.SetActive(true);
                Revolver.SetActive(true);
                RocketLauncher.SetActive(true);
            }
            if(oldItem.name == "Blue Gun"){
                RedGun.SetActive(true);
                BlueGun.SetActive(false);
                MachineGun.SetActive(true);
                Revolver.SetActive(true);
                RocketLauncher.SetActive(true);
            }
            if(oldItem.name == "Machine Gun"){
                RedGun.SetActive(true);
                BlueGun.SetActive(true);
                MachineGun.SetActive(false);
                Revolver.SetActive(true);
                RocketLauncher.SetActive(true);
            }
            if(oldItem.name == "Revolver"){
                RedGun.SetActive(true);
                BlueGun.SetActive(true);
                MachineGun.SetActive(true);
                Revolver.SetActive(false);
                RocketLauncher.SetActive(true);
            }
            if(oldItem.name == "Rocket Launcher"){
                RedGun.SetActive(true);
                BlueGun.SetActive(true);
                MachineGun.SetActive(true);
                Revolver.SetActive(true);
                RocketLauncher.SetActive(false);
            }
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
        BlueGun.SetActive(false);
        RedGun.SetActive(false);
        MachineGun.SetActive(false);
        Revolver.SetActive(false);
        RocketLauncher.SetActive(false);
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U)){
            UnequipAll();
        }

        
    }
}
