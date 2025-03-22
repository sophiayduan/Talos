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
    private bool hasInstantiated;
    public string hittag = "None";
    private bool hashit = false;
    void Start()
    {
        PlayerShoot playerShoot = FindFirstObjectByType<PlayerShoot>();
        target = playerShoot.destination; 
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
         if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            // hasInstantiated = true;
            // Instantiate(particles,target,Quaternion.identity);
            // Destroy(gameObject,0.01f);
            // Destroy(gameObject);
        }    
    }
    void OnTriggerEnter(Collider other)
    {
        
        if(hashit) return;

        if (other.CompareTag("Object"))
        {
            print("it is object");
            Instantiate(blueparticles,transform.position,Quaternion.identity);
                        // hasInstantiated = true;

            hittag = "Object";
            hashit = true;

        }
        if (other.CompareTag("Enemy"))
        {            
            print("it is the enemy!");
            Instantiate(particles,other.transform.position,Quaternion.identity);
            // hasInstantiated = true;
            EnemyHealth enemyHealth = other.GetComponentInParent<EnemyHealth>();
            hittag = "Enemy";
            hashit = true;


            if(enemyHealth != null)
            {
                enemyHealth.takeDamage(amount);
                // Instantiate(particles,other.transform.position,Quaternion.identity);


                Debug.Log("enemyHealth took damage");

            }
            else 
            {
                Debug.LogError("enemyhealth bottom = null");
            }
        }
        if (other.CompareTag("Ground")){
            hashit = true;
            hittag = "Plane";
            Instantiate(blueparticles,other.transform.position,Quaternion.identity);
                        // hasInstantiated = true;

        }
        hittag = other.tag;

    }
}
