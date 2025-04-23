using UnityEngine;
using CameraShake;
public class EnemyBullet : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Vector3 target;
    public float amount = 10;
    // public float KOtime;
    private PlayerHealth playerHealth;
    public  ParticleSystem particles;
    private bool hasHit = false;


    [SerializeField] 
    PerlinShake.Params shakeParams;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector3(player.position.x, player.position.y + 0f, player.position.z );
        if(player != null)
        {
            Debug.Log("Player != null (enemybullet)");
            playerHealth = player.GetComponent<PlayerHealth>();

            if(playerHealth != null){;
                Debug.Log("Playerhealth != null (enemybullet)");

                target = player.transform.position;
                transform.LookAt(target);
                Debug.Log("wow this is ok"); 
            }
            else {
                playerHealth = player.GetComponent<PlayerHealth>();
                Debug.LogError("playerhealth null (enemy bullet)");
            }

        }
        else 
        {
            Debug.LogError("player itself == null");
        }          
    }
    void Update()
    {   
        
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        // if(transform.position.x == target.x && transform.position.y == target.y){
        if (Vector3.Distance(transform.position, target) < 0.1f && hasHit == false){
            // DestroyEnemyBullet();
            // Instantiate(playerparticles,transform.position,Quaternion.identity);

    
        }
    }

    void DestroyEnemyBullet(){
        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {   
        if (hasHit) return;
                      
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ontriggerener tag is player");
            PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();

            if(playerHealth != null)
            {
                hasHit = true;
                playerHealth.takeDamage(amount);
                ParticleSystem enemy = Instantiate(particles,target,Quaternion.identity);
                Destroy(enemy.gameObject,1f);
                CameraShaker.Shake(new PerlinShake(shakeParams));

                Debug.Log("playerHealth took damage");

            }
            else 
            {
                Debug.LogError("playerhealth bottom = null");
            }
            Destroy(gameObject);

        }
        else  Debug.Log($"OK FINE THIS TAG {other.tag}");

    }

}


