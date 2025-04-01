using UnityEngine;
using TMPro;
using UnityEditor.Rendering;
using UnityEditor;
using Unity.VisualScripting;

public class Status : MonoBehaviour
{
    public TMP_Text debug;
    public EnemyScript enemy;
    public PlayerShoot player;
    public PlayerBullet bullet;
    public PlayerHealth health;



    void Start()
    {

        
    }

    void Update()
    {               
        // debug.text = $"\n Status: {enemy.status} \n Real Time: {Time.time} \n Pause Start: {enemy.startPause} \n Is Paused: {enemy.isPaused} \n Pause Duration: {enemy.pauseTime}" ;
        // debug.text $" respawn pos = {health.transform.position} "


    }



    

}
