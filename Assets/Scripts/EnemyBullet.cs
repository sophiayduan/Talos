using System;
using Charactercontroller;
using UnityEditor;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Vector3 target;
    public float damage = 2;
    public float KOtime;
    // public int damage = 2;
    private PlayerHealth playerHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector3(player.position.x, player.position.y, player.position.z);

        if(player != null)
        {
            
            playerHealth = player.GetComponent<PlayerHealth>();
            if(playerHealth != null){
                playerHealth.takeDamage(damage);
                Debug.Log($"Damage Amount: {damage}");
                DestroyEnemyBullet();
                
                target = player.transform.position;
                transform.LookAt(target);
                Debug.Log("wow this is ok"); 
            }
            else {
                Debug.LogError("playerhealth issue");
            }

        }
        else 
        {
            Debug.LogError("issue w player");
        }

 
        
            
    }




    // Update is called once per frame
    void Update()
    {        
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(transform.position.x == target.x && transform.position.y == target.y){
            DestroyEnemyBullet();
        }



    }


    void DestroyEnemyBullet(){
    Destroy(gameObject);
}
    void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();
        if(playerHealth!=null)
        {
            playerHealth.takeDamage(damage);
        }
    }
}


