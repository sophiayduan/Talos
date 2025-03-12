using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject deathPanel;

    public void ToggleDeathPanel() {
        deathPanel.SetActive(!deathPanel.activeSelf) ;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

}
