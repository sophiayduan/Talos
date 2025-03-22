using Unity.VisualScripting;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed =10f;
    private Vector3 target;
    private PlayerShoot playerShoot;
    public ParticleSystem particles;
    public float amount = 10;
    private bool hasInstantiated;
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
         if (!hasInstantiated && Vector3.Distance(transform.position, target) < 0.1f)
        {
            hasInstantiated = true;
            Instantiate(particles,target,Quaternion.identity);
            Destroy(gameObject,0.01f);
        }    
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object"))
        {print("it is object");
        Instantiate(particles,other.transform.position,Quaternion.identity);}
        if (other.CompareTag("Enemy"))
        {            
            print("it is the enemy!");
            EnemyHealth enemyHealth = other.GetComponentInParent<EnemyHealth>();


            if(enemyHealth != null)
            {
                enemyHealth.takeDamage(amount);
                Instantiate(particles,other.transform.position,Quaternion.identity);


                Debug.Log("enemyHealth took damage");

            }
            else 
            {
                Debug.LogError("enemyhealth bottom = null");
            }
        }

    }
}
