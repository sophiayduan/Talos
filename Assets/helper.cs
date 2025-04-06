using UnityEngine;

public class helper : MonoBehaviour
{
    public GameObject HelpPanel;

    void Awake()
    {
        HelpPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            HelpPanel.SetActive(true);
        }
    }
}
