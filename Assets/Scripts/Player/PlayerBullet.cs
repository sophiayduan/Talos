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
    private PoolType poolType = PoolType.playerBullets;
    void Start() 
    {
        objectPooler = FindFirstObjectByType<ObjectPooler>();

        PlayerShoot playerShoot = FindFirstObjectByType<PlayerShoot>();
        target = playerShoot.aimPos.position; 
        if (target == Vector3.zero)
        {
            ReturnToPool();
        }
    }
    void Update()
    {   if (target == Vector3.zero)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        // if (Vector3.Distance(transform.position, target) < 0.05f && !hashit ){
           
        //         ReturnToPool();
        //         return;
            

        // }    
    }
    void OnEnable()
    {
        hashit = false;
    }
    private void ReturnToPool(){
        // hashit = false;
        Debug.Log("add this to pool pls");
        gameObject.SetActive(false);
        // objectPooler.AddToPool(poolType, gameObject);
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
 
            Debug.Log("shouda been inactive ");
        }

        ReturnToPool();
    }
    
           
}
