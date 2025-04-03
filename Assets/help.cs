using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    // private float updateInterval = 1.0f; 
    private float _currentFPS;

    private void Start()
    {
        text.text = "FPS: 0";
    }

    void Update()
    {
        _currentFPS = 1f / Time.deltaTime;
        text.text = "FPS: " + Mathf.RoundToInt(_currentFPS);
    }


}