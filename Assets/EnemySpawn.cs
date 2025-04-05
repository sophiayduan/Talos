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
    public int maxActiveEnemies = 5; // Maximum number of active enemies
    private int activeEnemyCount = 0;
    private float lifeTime = 10f;

    public float minSpawnDistance;
    private ObjectPooler objectPooler;
    public PoolType poolType = PoolType.smallEnemy;
    public float initialSpeed;
    private List<GameObject> activeEnemies = new List<GameObject>();
   private void Start()
    {
        lastSpawnPoint = player.transform.position;
        objectPooler = FindFirstObjectByType<ObjectPooler>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer =  Vector3.Distance(lastSpawnPoint, player.transform.position);
        if (distanceFromPlayer >= minSpawnDistance){
                StartCoroutine(SpawnEnemiesRoutine());
        }
        if(activeEnemyCount == maxActiveEnemies){
                StartCoroutine(DeactivateAfterTime());
        }

        if (Input.GetKeyDown(KeyCode.B)) // Example key press
        {
            DeactivateAllEnemies();
        }
    }

        void SpawnEnemies(){
            if (activeEnemyCount >= maxActiveEnemies)
            {
                Debug.Log("max active enemies");
                return;
            }
            // Instantiate(enemy, lastSpawnPoint, Quaternion.identity);
            // lastSpawnPoint = player.transform.position;
            GameObject newSpawnedObject = objectPooler.GetFromPool(poolType);
            newSpawnedObject.transform.position = lastSpawnPoint;
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
            lastSpawnPoint = player.transform.position;

           
        }
    IEnumerator SpawnEnemiesRoutine()
    {
        float count = 0f;
        while(count < enemyAmount)
        {
            Debug.Log("I SPAWNED");
            count += 1;
            SpawnEnemies();
            yield return new WaitForSeconds(1);

        }
        
    }
    IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        enemy.Deactivate();
    }
    

    public void EnemyDeactivation()
    {
        activeEnemyCount = Mathf.Max(activeEnemyCount - 1, 0); 
        Debug.Log("deactivated, active enemy # " + activeEnemyCount);
    }

    public void DeactivateAllEnemies()
    {
        foreach (GameObject enemies in activeEnemies)
        {
            Destroy(enemy.gameObject);
            // if (enemies.activeSelf) // Ensure enemy is active
            // {
            //     EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
            //     if (enemyScript != null)
            //     {
            //         enemy.Deactivate(); // Call the Deactivate method on the enemy
            //     }
            //     else
            //     {
            //         enemy.gameObject.SetActive(false); // Fallback if no Enemy script exists
            //     }
            // }
        }

        activeEnemies.Clear(); // Clear the list after deactivating all
        activeEnemyCount = 0; // Reset active enemy count
        Debug.Log("All enemies have been deactivated.");
    }
}
