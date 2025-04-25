using UnityEngine;
using TMPro;


public class Status : MonoBehaviour
{
    public TMP_Text debug;
    public EnemyScript enemy;
    public PlayerShoot player;
    public PlayerBullet bullet;
    public PlayerHealth health;
    public SetSpawn spawn;

    void Start()
    {

        
    }

    void Update()
    {               
        // debug.text = $"\n Status: {enemy.status} \n Real Time: {Time.time} \n Pause Start: {enemy.startPause} \n Is Paused: {enemy.isPaused} \n Pause Duration: {enemy.pauseTime}" ;
        debug.text = $"is inside trigger: {spawn.isInsideTrigger} \n set a spawn?: {spawn.spawnSets} \n respawn point loc: {health.respawnPoint}";



    }



    

}
