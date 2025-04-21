using UnityEngine;

public class helper : MonoBehaviour
{
    public GameObject HelpPanel;

    void OnTriggerExit(Collider other)
    {

        if(other.CompareTag("Player")){
            gameObject.SetActive(false);
            HelpPanel.SetActive(false);
        }
    }

    
}
