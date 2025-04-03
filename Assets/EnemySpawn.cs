using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{
    public PlayerHealth player; 
    [SerializeField] private Vector3 lastSpawnPoint;
    public int enemyAmount;
    public float minSpawnDistance;
    private ObjectPooler objectPooler;
    public PoolType poolType = PoolType.smallEnemy;
    public float initialSpeed;
    private void Start()
    {
        lastSpawnPoint = player.transform.position;
        objectPooler = FindFirstObjectByType<ObjectPooler>();
    }

    void Update()
    {
        float distanceFromPlayer =  Vector3.Distance(lastSpawnPoint, player.transform.position);
        if (distanceFromPlayer >= minSpawnDistance){
                StartCoroutine(SpawnEnemiesRoutine());
        }
    }
        void SpawnEnemies(){
            // Instantiate(enemy, lastSpawnPoint, Quaternion.identity);
            // lastSpawnPoint = player.transform.position;
            GameObject newSpawnedObject = objectPooler.GetFromPool(poolType);
            newSpawnedObject.transform.position = lastSpawnPoint;
            newSpawnedObject.transform.rotation = Quaternion.identity;
            newSpawnedObject.GetComponent<Rigidbody>().linearVelocity = transform.forward * initialSpeed;
            newSpawnedObject.transform.parent = transform;
            newSpawnedObject.SetActive(true);
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
}
