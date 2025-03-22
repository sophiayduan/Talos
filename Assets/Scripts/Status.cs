using UnityEngine;
using TMPro;
using UnityEditor.Rendering;
using UnityEditor;
using Unity.VisualScripting;

public class Status : MonoBehaviour
{
    public TMP_Text debug;
    public EnemyHealth enemy;
    public PlayerShoot player;
    public PlayerBullet bullet;

    void Start()
    {

        
    }

    void Update()
    {               
        debug.text = $"\n Enemy Health: {enemy.enemyHealth} \n {player.destinationpoint} " ;



    }



    

}
