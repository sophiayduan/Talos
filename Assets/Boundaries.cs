using UnityEngine;

public class Boundaries : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameManager gameManager;

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            Debug.Log("player is leaving bounds");
            playerHealth.currentHealth = playerHealth.maxHealth;
            gameManager.GameOver();

        }
    }
    
}
