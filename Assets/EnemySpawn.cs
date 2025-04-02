using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{
    public PlayerHealth player;
    public GameObject enemy;
    // public Transform[] small;
    // public Transform[] medium;
    // public Transform[] large;
    // public Terrain terrain1, terrain2, terrain3, terrain4, terrain5, terrain6;
    [SerializeField] private Vector3 lastSpawnPoint;
    public int enemyAmount;

    public float minSpawnDistance;
   void Start()
    {
        lastSpawnPoint = player.transform.position;
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
            Instantiate(enemy, lastSpawnPoint, Quaternion.identity);
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
