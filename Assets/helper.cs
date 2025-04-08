using UnityEngine;

public class helper : MonoBehaviour
{
    public GameObject HelpPanel;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(" set help panel, triggered");
        // HelpPanel.SetActive(true);
        if(other.CompareTag("Player")){
            Debug.Log("yuppity yup");
            HelpPanel.SetActive(true);
        }
    }

    
}
