using UnityEngine;
using System.Collections;

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
   private void Start()
    {
        lastSpawnPoint = player.transform.position;
        objectPooler = FindFirstObjectByType<ObjectPooler>();
        StartCoroutine(DeactivateAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer =  Vector3.Distance(lastSpawnPoint, player.transform.position);
        if (distanceFromPlayer >= minSpawnDistance){
                StartCoroutine(SpawnEnemiesRoutine());

        }
    }

        void SpawnEnemies(){
            if (activeEnemyCount >= maxActiveEnemies)
            {
                Debug.Log("Max enemy limit reached. Not spawning more enemies.");
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
                enemy.OnDeactivate += HandleEnemyDeactivation;
            }

            newSpawnedObject.SetActive(true);
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
    public void HandleEnemyDeactivation()
    {
        activeEnemyCount = Mathf.Max(activeEnemyCount - 1, 0); // Decrease counter safely
        Debug.Log("An enemy was deactivated. Active enemy count: " + activeEnemyCount);
    }
}
