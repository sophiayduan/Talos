using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public void ReloadCurrentScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f; 

    }

    public void ChangeSceneByName(string name) {
        if(name !=null) {
            SceneManager.LoadScene(name);
        }
    }
}
