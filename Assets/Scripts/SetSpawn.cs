using UnityEngine;

public class SetSpawn : MonoBehaviour
{

    public static Vector3 newSpawn ;
    public bool isInsideTrigger = false;
    public  bool spawnSets = false;

    void Start()
    {
        newSpawn = Vector3.zero;
    }
    void Update()
    {   
        if (isInsideTrigger && Input.GetKeyDown(KeyCode.E)){
            newSpawn = transform.position;
            spawnSets = true;

            Debug.Log("set new spawn i hope");
        }
        else {
            return;
        }
        Debug.Log($"new spawn loc : {newSpawn}");
    }
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("aimPos") ){
            Debug.Log("inside trigger is true!!");
            isInsideTrigger = true;

        }
        else {
            isInsideTrigger = false;
            Debug.Log("is inside trigger false");
        }
    }

}
    

