using UnityEngine;

public class SetSpawn : MonoBehaviour
{

    public static Vector3 newSpawn ;
    private bool isInsideTrigger = false;
   
    void Update()
    {   
        if (isInsideTrigger && Input.GetKeyDown(KeyCode.E)){
            newSpawn = transform.position;
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
    

