using System.Collections;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed =10f;
    private Vector3 target;
    public ParticleSystem groundParticles;
    public ParticleSystem enemyParticles;
    public float amount = 10;
    private bool hashit = false;
    private ObjectPooler objectPooler;
    private Vector3 newTarget;
    private PoolType poolType = PoolType.playerBullets;
    void Start() 
    {
        objectPooler = FindFirstObjectByType<ObjectPooler>();

        PlayerShoot playerShoot = FindFirstObjectByType<PlayerShoot>();
 

        target = playerShoot.aimPos.position; 

    }
    
    void Update()
    {   
        if (target == Vector3.zero)
        {
            PlayerShoot playerShoot = FindFirstObjectByType<PlayerShoot>();
        target = playerShoot.aimPos.position; 
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, target) < 0.0001f && !hashit ){
           
                StartCoroutine(WaitReturnToPool());

        }    
    }

    IEnumerator WaitReturnToPool(){
        yield return null;
        ReturnToPool();
    }
    void OnEnable()
    {
        hashit = false;
        PlayerShoot playerShoot = FindFirstObjectByType<PlayerShoot>();
        target = playerShoot.aimPos.position; 
    }

    private void ReturnToPool(){
        gameObject.SetActive(false);
        objectPooler.AddToPool(poolType, gameObject);
        hashit = false;
        target = newTarget;

    }
    void OnTriggerEnter(Collider other)
    {

        if(hashit) return;
        hashit = true;
        Debug.Log("collision!");
        Debug.Log($"collided:{other.tag}");
        Debug.Log($"Hit: {other.name}, tag: {other.tag}, layer: {LayerMask.LayerToName(other.gameObject.layer)}");
        if (other.CompareTag("Object"))
        {
            print("it is object");
            ParticleSystem enemy = Instantiate(enemyParticles,target,Quaternion.identity);
            Destroy(enemy.gameObject,0.1f);
        }
        else if (other.CompareTag("Enemy"))
        {            
            print("it is the enemy!");
            ParticleSystem enemy = Instantiate(enemyParticles,target,Quaternion.identity);
            Destroy(enemy.gameObject,0.1f);
            EnemyHealth enemyHealth = other.GetComponentInParent<EnemyHealth>();


            if(enemyHealth != null)
            {
                enemyHealth.takeDamage(amount);
                Debug.Log("enemyHealth took damage");
            }
            else 
            {
                Debug.LogError("enemyhealth bottom = null");
            }
        }
        else if (other.CompareTag("Ground")){
            ParticleSystem ground = Instantiate(groundParticles,target,Quaternion.identity);
            Destroy(ground.gameObject,0.1f);
            Debug.Log("GROUND");
        }

        ReturnToPool();
        // gameObject.SetActive(false);
    }
    
           
}
