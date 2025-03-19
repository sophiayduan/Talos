using UnityEngine;
using UnityEngine.UI;

public class DeathPanel  : MonoBehaviour
{
    public Button respawnButton, endButton;
    public GameObject Player;
    void Start()
    {

    }
    void Update()
    {
        respawnButton.onClick.AddListener(respawn);
        

    }
    void respawn()
    {
        Destroy(Player);
        Instantiate(Player,new Vector3(0, 0), Quaternion.identity);
    }

 
}
