using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject deathPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject winPanel;



    public bool isPaused = false;
    private GameObject[] respawns;
    void Start()
    {
        respawns = GameObject.FindGameObjectsWithTag("Respawn");
    }
    void Update()
    {
        if (isPaused == false)settingsPanel.SetActive(false);
    }
    public void ToggleDeathPanel() 
    {
        if (deathPanel.activeSelf == true){
            return;
        }
        else deathPanel.SetActive(true);
    }
     public void ToggleWinPanel() 
    {
        if (winPanel.activeSelf == true){
            return;
        }
        else winPanel.SetActive(true);
    }
    public void TogglePausePanel() 
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        foreach (GameObject obj in respawns){
            obj.SetActive(!obj.activeSelf);

        if (isPaused == true)Time.timeScale = 0f; 
              
        else Time.timeScale = 1f;           

        
    }
}
        public void ToggleSettingsPanel() 
        {
            if (SceneManager.GetActiveScene().buildIndex == 1 && isPaused == true){
                pausePanel.SetActive(!pausePanel.activeSelf);
                settingsPanel.SetActive(!settingsPanel.activeSelf);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 1 && isPaused == false) return;
            else if (SceneManager.GetActiveScene().buildIndex == 2) settingsPanel.SetActive(!settingsPanel.activeSelf);
            
    }
}
