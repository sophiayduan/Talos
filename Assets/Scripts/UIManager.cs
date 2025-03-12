using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject deathPanel;
    [SerializeField] GameObject pausePanel;
    private bool isPaused = false;

    


    public void ToggleDeathPanel() {
         deathPanel.SetActive(!deathPanel.activeSelf);

    }
    public void TogglePausePanel() {
        isPaused = !isPaused;
        pausePanel.SetActive(!pausePanel.activeSelf);
        if (isPaused == true)Time.timeScale = 0f;
        else Time.timeScale = 1f;
        

    }

}
