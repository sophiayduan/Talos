using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed =2f;
    private Vector3 target;
    private PlayerShoot playerShoot;
    public ParticleSystem particles;
    public float amount = 10;

    void Start()
    {
        PlayerShoot playerShoot = FindFirstObjectByType<PlayerShoot>();
        if (playerShoot == null){Debug.LogError("freak"); }
        else target = playerShoot.destination; Debug.Log("yayay");
    }
    void Update()
    {   if (target == Vector3.zero)
        {
            Debug.LogError("Bullet target is Vector3.zero, destroying bullet.");
            Destroy(gameObject);
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
         if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            Destroy(gameObject);
            Instantiate(particles,target,Quaternion.identity);

        }    
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object"))
        print("it is object");
        // Instantiate(particles,other.transform.position,Quaternion.identity);
        if (other.CompareTag("Enemy"))
        {            
            print("it is the enemy!");
            EnemyHealth enemyHealth = other.GetComponentInParent<EnemyHealth>();


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
    }
}
