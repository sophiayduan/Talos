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
    public TrailRenderer trailRenderer;
    public Transform player;
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
        // float distance = (player.transform.position - gameObject.transform.position).magnitude;
        // if(distance > 1f){
        //     trailRenderer.material.SetColor("_TintColor", new Color(128,128,128,128));

        // }
        

    }

    private void ReturnToPool(){
        gameObject.SetActive(false);
        objectPooler.AddToPool(poolType, gameObject);
        hashit = false;
        target = newTarget;

    }
    void OnTriggerEnter(Collider other)
    {

        if(hashit){ ReturnToPool(); return;}
        Debug.Log($"collided:{other.tag}");
        Debug.Log($"Hit: {other.name}, tag: {other.tag}, layer: {LayerMask.LayerToName(other.gameObject.layer)}");
        if (other.CompareTag("Object"))
        {
            ParticleSystem enemy = Instantiate(enemyParticles,target,Quaternion.identity);
            Destroy(enemy.gameObject,0.1f);
            hashit = true;
            print("it is object");
            transform.position = Vector3.zero;
            transform.position = Vector3.MoveTowards(transform.position, transform.position, speed * Time.deltaTime);

            ReturnToPool();
        }
        else if (other.CompareTag("Enemy"))
        {            
            hashit = true;
            print("it is the enemy!");
            ParticleSystem enemy = Instantiate(enemyParticles,target,Quaternion.identity);
            Destroy(enemy.gameObject,0.1f);
            EnemyHealth enemyHealth = other.GetComponentInParent<EnemyHealth>();
            ReturnToPool();

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
                        hashit = true;

            ParticleSystem ground = Instantiate(groundParticles,target,Quaternion.identity);
            Destroy(ground.gameObject,0.1f);
            Debug.Log("GROUND");
            ReturnToPool();
        }
       

        
        // gameObject.SetActive(false);
    }
    
           
}
