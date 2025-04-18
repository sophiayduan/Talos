using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour
{
    public PlayerHealth player;
    public EnemyHealth enemy;
    // public GameObject enemy;
 
    [SerializeField] private Vector3 lastSpawnPoint;
    public int enemyAmount;
    public int maxActiveEnemies = 5; 
    public float cooldown;
    private int activeEnemyCount = 0;
    public float minSpawnDistance;
    private ObjectPooler objectPooler;
    public PoolType poolType = PoolType.smallEnemy;
    public float initialSpeed;
    private List<GameObject> activeEnemies = new List<GameObject>();
    private bool spawning = false;
    private void Start()
    {
        lastSpawnPoint = player.transform.position;
        objectPooler = FindFirstObjectByType<ObjectPooler>();
    }

    void Update()
    {
        // float distanceFromPlayer =  Vector3.Distance(lastSpawnPoint, player.transform.position);
        StartCoroutine(SpawnEnemiesRoutine());

        // if (distanceFromPlayer >= minSpawnDistance && spawning == false){
        //         StartCoroutine(SpawnEnemiesRoutine());
        // }

    }

        void SpawnEnemies(){
            spawning = true;
            if (activeEnemyCount >= maxActiveEnemies)
            {
                Debug.Log("max active enemies");
                return;
            }

            Vector3 spawnPosition = player.transform.position + new Vector3(
                Random.Range(-12f, 12f), 
                0, 
                Random.Range(-12f, 12f) 
            );
            // Instantiate(enemy, lastSpawnPoint, Quaternion.identity);
            // lastSpawnPoint = player.transform.position;
            GameObject newSpawnedObject = objectPooler.GetFromPool(poolType);
            newSpawnedObject.transform.position = spawnPosition;
            newSpawnedObject.transform.rotation = Quaternion.identity;
            newSpawnedObject.GetComponent<Rigidbody>().linearVelocity = transform.forward * initialSpeed;
            newSpawnedObject.transform.parent = transform;
            enemy = newSpawnedObject.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.OnDeactivate += EnemyDeactivation;
            }

            newSpawnedObject.SetActive(true);
            activeEnemies.Add(newSpawnedObject);
            activeEnemyCount++;
            lastSpawnPoint = spawnPosition;
            spawning = false;

           
        }
    IEnumerator SpawnEnemiesRoutine()
    {
        float count = 0f;
        while(count < enemyAmount)
        {
            if (activeEnemyCount >= maxActiveEnemies)
            {
                yield break;
            }
            
            Debug.Log("I SPAWNED");
            count += 1;
            SpawnEnemies();
            yield return new WaitForSeconds(3);

        }
        
    }

    
    public void EnemyDeactivation()
    {
        activeEnemyCount = Mathf.Max(activeEnemyCount - 1, 0); 
    }

            
    //     foreach (GameObject enemies in activeEnemies)
    //     {
            
    //         if (enemies.activeSelf) // Ensure enemy is active
    //         {
    //             EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
    //             if (enemyScript != null)
    //             {
    //                 enemy.Deactivate(); // Call the Deactivate method on the enemy
    //             }
    //             else
    //             {
    //                 enemy.gameObject.SetActive(false); 
    //             }
    //         }
    //     }

    //     activeEnemies.Clear(); 
    //     activeEnemyCount = 0; 
    //     Debug.Log("All enemies have been deactivated.");
    
}
