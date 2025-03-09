using UnityEngine;
using TMPro;
using UnityEditor.Rendering;

public class Status : MonoBehaviour
{
    public TMP_Text debug;
    public EnemyScript enemy;
    public PlayerHealth player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        debug.text = $" Cooldown: {enemy.Cooldown + enemy.LastAttack - Time.time:F2}s \n seePlayer {enemy.seePlayer} \n In attack range: {enemy.attackRange} \n Current Health: {player.currentHealth}";

    }

}
