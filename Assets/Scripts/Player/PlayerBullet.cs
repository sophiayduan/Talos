using Unity.VisualScripting;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 target;
    public ParticleSystem groundParticles;
    public ParticleSystem enemyParticles;
    public float amount = 10;
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
    {   
        if (target == Vector3.zero)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) <= 0f && !hashit){
            // Instantiate(enemyParticles, transform.position, Quaternion.identity);
            // Destroy(gameObject);
            
        }    
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("i been triggered");
        if(hashit) return;
        hashit = true;
      
      
        if (other.CompareTag("Ground"))
        {
            print("it is object");
            Instantiate(groundParticles,transform.position,Quaternion.identity);
            Destroy(gameObject);


        }
        // else if (other.CompareTag("Enemy"))
        // {            
        //     print("it is the enemy!");
        //     Instantiate(enemyParticles,other.transform.position,Quaternion.identity);
        //     EnemyHealth enemyHealth = other.GetComponentInParent<EnemyHealth>();


        //     if(enemyHealth != null)
        //     {
        //         enemyHealth.takeDamage(amount);
        //         Destroy(gameObject);
        //         Debug.Log("enemyHealth took damage");
        //     }
        //     else 
        //     {
        //         Debug.LogError("enemyhealth bottom = null");
        //     }
        // }
        // else if (other.CompareTag("Ground")){

        //     Instantiate(groundParticles,target,Quaternion.identity);
            
        //     Debug.Log("GROUND OK");
        //     Destroy(gameObject);
        // }
    }
    
}
