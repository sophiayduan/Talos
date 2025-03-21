using TMPro.EditorUtilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else Destroy(gameObject);
    }

    public void GameOver() {
        UIManager _ui = GetComponent<UIManager>();
        if (_ui != null) {
            _ui.ToggleDeathPanel();
        } 
        GameObject[] respawns = GameObject.FindGameObjectsWithTag("Respawn");
        foreach (GameObject obj in respawns){
            Destroy(obj);
        }
    }
}
