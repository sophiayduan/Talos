using UnityEngine;

public class Win : MonoBehaviour
{
    // public GameManager gameManager;

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGERED WIN COLLIDER");
        // GameManager.instance.GameOverWin();

        if(other.CompareTag("Player")){
            Debug.Log("ITS PLAYER WINNING");
            GameManager.instance.GameOverWin();
        }
    }
}
