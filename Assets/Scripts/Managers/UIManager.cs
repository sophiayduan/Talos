using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject deathPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject settingsPanel;
    
    private bool isPaused = false;

    public void ToggleDeathPanel() {
        if (deathPanel.activeSelf == true){
            return;
        }
        else deathPanel.SetActive(true);
    }
    public void TogglePausePanel() {
        isPaused = !isPaused;
        pausePanel.SetActive(!pausePanel.activeSelf);
        GameObject[] respawns = GameObject.FindGameObjectsWithTag("Respawn");

        if (isPaused == true){
            Time.timeScale = 0f;
            foreach (GameObject obj in respawns){
            obj.SetActive(false);
        }        
        }
        else {
            Time.timeScale = 1f;            
            foreach (GameObject obj in respawns){
                obj.SetActive(true);
            }
        }
    }
        public void ToggleSettingsPanel() {
            if (SceneManager.GetActiveScene().buildIndex == 1){
                pausePanel.SetActive(!pausePanel.activeSelf);
                settingsPanel.SetActive(!settingsPanel.activeSelf);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2) {
                settingsPanel.SetActive(!settingsPanel.activeSelf);
            }
    }
}
