using UnityEngine;

public class Win : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGERED WIN COLLIDER");

        if(other.CompareTag("Player")){
            Debug.Log("ITS PLAYER WINNING");
            GameManager.instance.GameOverWin();
        }
    }
}
