using Unity.VisualScripting;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed =10f;
    private Vector3 target;
    public ParticleSystem groundParticles;
    public ParticleSystem enemyParticles;
    public float amount = 10;
    private bool hasInstantiated = false;
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
            Debug.LogError("Bullet target is Vector3.zero, destroying bullet.");
            ReturnToPool();
        }
    }
    void Update()
    {   if (target == Vector3.zero)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        // if (Vector3.Distance(transform.position, target) < 0.1f && !hashit && !hasInstantiated){
           
        //         ReturnToPool();
        //         return;
            

        // }    
    }
    void OnEnable()
    {
        hashit = false;
        hasInstantiated = false;
    }
    void OnTriggerEnter(Collider other)
    {

        if(hashit) return;
        
        if (other.CompareTag("Object"))
        {
            print("it is object");
            ParticleSystem enemy = Instantiate(enemyParticles,target,Quaternion.identity);
            Destroy(enemy.gameObject,0.1f);
            hashit = true;
            // Destroy(gameObject);
            ReturnToPool();
            hasInstantiated = true;



        }
        else if (other.CompareTag("Enemy"))
        {            
            print("it is the enemy!");
            ParticleSystem enemy = Instantiate(enemyParticles,target,Quaternion.identity);
            Destroy(enemy.gameObject,0.1f);
            EnemyHealth enemyHealth = other.GetComponentInParent<EnemyHealth>();
            hasInstantiated = true;

            hashit = true;

            if(enemyHealth != null)
            {
                enemyHealth.takeDamage(amount);
                gameObject.SetActive(false);
                ReturnToPool();
                //  Destroy(gameObject);


                Debug.Log("enemyHealth took damage");
            }
            else 
            {
                Debug.LogError("enemyhealth bottom = null");
            }
        }
        else if (other.CompareTag("Ground")){
            // Destroy(gameObject);
            hashit = true;
            ParticleSystem ground = Instantiate(groundParticles,target,Quaternion.identity);
            Destroy(ground.gameObject,0.1f);

            hasInstantiated = true;
            Debug.Log("GROUND");
            gameObject.SetActive(false);
            ReturnToPool();


        }
    }
    private void ReturnToPool(){
        // hashit = false;
        // hasInstantiated = false;
        gameObject.SetActive(false);
        objectPooler.AddToPool(poolType, gameObject);
    }
           
}
