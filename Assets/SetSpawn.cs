
using UnityEditor;
using UnityEngine;

public class SetSpawn : MonoBehaviour
{

    public static Vector3 newSpawn ;
    private bool isInsideTrigger = false;
   
    void Update()
    {   
        if (isInsideTrigger && Input.GetKeyDown(KeyCode.Q)){
            newSpawn = transform.position;
            Debug.Log("set new spawn i hope");
        }
        Debug.Log($"new spawn loc : {newSpawn}");
    }
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("aimPos") ){
            Debug.Log("got the layer trigger");
            isInsideTrigger = true;

        }
        else {
            isInsideTrigger = false;
            Debug.Log("is inside trigger false");
        }
    }

}
    

