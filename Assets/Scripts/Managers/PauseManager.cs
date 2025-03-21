using TMPro.EditorUtilities;
using UnityEngine;

public  class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            UIManager _ui = GetComponent<UIManager>();
             if (_ui != null) {
            _ui.TogglePausePanel();
            }
            else
            {
                Debug.Log("ui null");
            }
        }
        }
    }


