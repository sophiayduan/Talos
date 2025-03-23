using Unity.VisualScripting;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed =10f;
    private Vector3 target;
    private PlayerShoot playerShoot;
    public ParticleSystem particles;
    public ParticleSystem blueparticles;
    public float amount = 10;
    private bool hasInstantiated = false;
    private bool hashit = false;
    private bool triggered = false;
    void Start()
    {
        PlayerShoot playerShoot = FindFirstObjectByType<PlayerShoot>();
        target = playerShoot.aimPos.position; 
        if (target == Vector3.zero)
        {
            Debug.LogError("Bullet target is Vector3.zero, destroying bullet.");
            Destroy(gameObject,0.01f);
        }
    }
    void Update()
    {   if (target == Vector3.zero)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < 0.1f && !hashit && !hasInstantiated){
            if (target == playerShoot.cam.transform.position + playerShoot.cam.transform.forward * playerShoot.maxDistance){
                Destroy(gameObject);
            }
    
        }    
    }
    void OnTriggerEnter(Collider other)
    {

        if(hashit) return;
        triggered = true;
        if (other.CompareTag("Object"))
        {
            print("it is object");
            Instantiate(blueparticles,transform.position,Quaternion.identity);
            hashit = true;
            Destroy(gameObject);
            hasInstantiated = true;



        }
        else if (other.CompareTag("Enemy"))
        {            
            print("it is the enemy!");
            Instantiate(particles,other.transform.position,Quaternion.identity);
            EnemyHealth enemyHealth = other.GetComponentInParent<EnemyHealth>();
                    hasInstantiated = true;

            hashit = true;

            if(enemyHealth != null)
            {
                enemyHealth.takeDamage(amount);
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
            Instantiate(blueparticles,target,Quaternion.identity);
            hasInstantiated = true;
            Debug.Log("GROUND");
            Destroy(gameObject);
        }
    }
    
}
