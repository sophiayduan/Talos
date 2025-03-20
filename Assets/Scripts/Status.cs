using UnityEngine;
using TMPro;
using UnityEditor.Rendering;

public class Status : MonoBehaviour
{
    public TMP_Text debug;
    public EnemyScript enemy;
    public PlayerHealth player;

    void Start()
    {

        
    }

    void Update()
    {               
        debug.text = $"\n Cooldown: {enemy.Cooldown + enemy.LastAttack - Time.time:F2}s \n seePlayer {enemy.seePlayer} \n In attack range: {enemy.attackRange} \n Current Health: {player.currentHealth} \n Ease Slider: {player.easeHealthSlider.value} \n Time: {Time.time} \n TimeScale: {Time.timeScale}" ;



    }



    

}
