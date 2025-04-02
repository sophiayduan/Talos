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
    void Start()
    {
        
        PlayerShoot playerShoot = FindFirstObjectByType<PlayerShoot>();
        target = playerShoot.aimPos.position; 
        if (target == Vector3.zero)
        {
            Debug.LogError("Bullet target is Vector3.zero, destroying bullet.");
            Destroy(gameObject);
        }
    }
    void Update()
    {   if (target == Vector3.zero)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < 0.1f && !hashit && !hasInstantiated){
            // if (target == Camera.main.transform.position + Camera.main.transform.forward * playerShoot.maxDistance){
            //     Destroy(gameObject);
            // }
    
        }    
    }
    void OnTriggerEnter(Collider other)
    {

        if(hashit) return;
        
        if (other.CompareTag("Object"))
        {
            print("it is object");
            ParticleSystem enemy = Instantiate(enemyParticles,target,Quaternion.identity);
            Destroy(enemy.gameObject,1f);
            hashit = true;
            Destroy(gameObject);
            hasInstantiated = true;



        }
        else if (other.CompareTag("Enemy"))
        {            
            print("it is the enemy!");
            ParticleSystem enemy = Instantiate(enemyParticles,target,Quaternion.identity);
            Destroy(enemy.gameObject,1f);
            EnemyHealth enemyHealth = other.GetComponentInParent<EnemyHealth>();
            hasInstantiated = true;

            hashit = true;

            if(enemyHealth != null)
            {
                enemyHealth.takeDamage(amount);
                // gameObject.SetActive(false);
                Destroy(gameObject);

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
            Destroy(ground.gameObject,1f);

            hasInstantiated = true;
            Debug.Log("GROUND");
            // gameObject.SetActive(false);
            Destroy(gameObject);

        }
    }
           
}
