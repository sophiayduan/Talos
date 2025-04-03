// using Unity.VisualScripting;
// using UnityEngine;

// public class RocketLauncher : MonoBehaviour
// {
//     public float speed = 10f;
//     private Vector3 target;
//     public ParticleSystem particles;
//     public float amount = 10;
//     private bool hasInstantiated = false;
//     private bool hashit = false;
//     void Start()
//     {
//         PlayerShoot playerShoot = FindFirstObjectByType<PlayerShoot>();
//         target = playerShoot.aimPos.position; 
//         if (target == Vector3.zero)
//         {
//             Debug.LogError("Bullet target is Vector3.zero, destroying bullet.");
//             Destroy(gameObject);
//         }
//     }
//     void Update()
//     {   if (target == Vector3.zero)
//         {
//             return;
//         }
//         transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
//         if (Vector3.Distance(transform.position, target) < 0.1f && !hashit && !hasInstantiated){
//             ParticleSystem bomb = Instantiate(particles,target,Quaternion.identity);
//             Destroy(bomb.gameObject,1f);            
//             Destroy(gameObject);
            
//         }    
//     }
//     void OnTriggerEnter(Collider other)
//     {

//         if(hashit) return;
//         if (other.CompareTag("Object"))
//         {
//             print("it is object");
//             ParticleSystem bomb = Instantiate(particles,target,Quaternion.identity);
//             Destroy(bomb.gameObject,1f);               
//             Destroy(gameObject);

//             hashit = true;
//             hasInstantiated = true;

//         }
//         else if (other.CompareTag("Enemy"))
//         {            
//             print("it is the enemy!");
//             ParticleSystem bomb = Instantiate(particles,target,Quaternion.identity);
//             Destroy(bomb.gameObject,1f);                 
//             EnemyHealth enemyHealth = other.GetComponentInParent<EnemyHealth>();
//             hasInstantiated = true;
//             hashit = true;

//             if(enemyHealth != null)
//             {
//                 enemyHealth.takeDamage(amount);
//                 Destroy(gameObject);
//                 Debug.Log("enemyHealth took damage");
//             }
//             else 
//             {
//                 Debug.LogError("enemyhealth bottom = null");
//             }
//         }
//         else if (other.CompareTag("Ground")){
//             hashit = true;
//             ParticleSystem bomb = Instantiate(particles,target,Quaternion.identity);
//             Destroy(bomb.gameObject,1f);                 hasInstantiated = true;
//             Debug.Log("GROUND");
//             Destroy(gameObject);
//         }
//     }
    
// }