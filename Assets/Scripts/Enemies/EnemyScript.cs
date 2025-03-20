using UnityEngine;
using UnityEngine.AI;


public class EnemyScript : MonoBehaviour
{    
    public Rigidbody rb;
    public float Maxspeed;
    private float Speed;
    public NavMeshAgent agent;

    public float SightRange;
    public float DetectionRange;
    public GameObject Target;
    public bool seePlayer;
    private Collider[] hitColliders;
    private RaycastHit Hit;

    // public bool grounded = true;

    // public float Health;

    //Attacking 
    public float Cooldown;
    public float MinAttackRange;
    public float MaxAttackRange;
    public float LastAttack;
    public GameObject EnemyBullet;
    public bool attackRange;

    public LayerMask GroundLayer;

    // private Vector3 lastKnownPosition;
    // private bool lostPlayer;

    // private float lastSeenTime;
    // public float searchTime = 5f;

    // Patrolling 
    // public Vector3 walkPoint;
    // bool walkPointSet;
    // public float walkPointRange;
    [SerializeField] private Animator _animator;
    private static int isIdleHash = Animator.StringToHash("isIdle");
    [SerializeField] private LayerMask layerMask;
    void Start()
    {
        Speed = Maxspeed;
        _animator = GetComponent<Animator>();

       
        
    }

    void Update()
    {
        if (!seePlayer)
        {
            hitColliders = Physics.OverlapSphere(transform.position, DetectionRange);
            foreach (var HitCollider in hitColliders)
            {
                if(HitCollider.tag == "Player")
                {
                    Target = HitCollider.gameObject;
                    seePlayer = true;
                    Debug.Log("ooh its the player");
                    break;
                }
                else{
                    Debug.LogError("didn't hit the player");
                    seePlayer = false;
                }
            }
        }
        else
        {
            // Vector3 origin = transform.position + Vector3.up * 1.2f;
            
            if(Physics.Raycast(transform.position, Target.transform.position - transform.position, out Hit, SightRange, layerMask)){
                Debug.Log("Raycast hit: " + Hit.collider.name);

                if (Hit.collider.tag == "Player")
                {
                    Debug.Log("okay this is the player");

                    var Heading = Target.transform.position - transform.position;
                    var Distance = Heading.magnitude;
                    // var Direction = Heading / Distance;
                    agent.SetDestination(Hit.point);

                    if(Distance <= MaxAttackRange && Distance >= MinAttackRange)
                    { 
                        attackRange = true;
                        agent.isStopped = false;
                        Attack();
                        agent.SetDestination(Hit.point);
                        //_animator.SetBool("isRunning", false);
                    }
                    else if(Distance < MinAttackRange + 1 && Distance > MinAttackRange)
                    {
                        agent.isStopped = true;
                        agent.velocity = Vector3.zero;
                        transform.LookAt(Target.transform.position);
                        attackRange = true;
                  
                        // Attack();
                    }

                    else if(Distance < MinAttackRange )
                    {
                        agent.isStopped = false;
                        attackRange = false;
                        Vector3 AwayDirection = (transform.position - Target.transform.position).normalized;
                        Vector3 MoveAway = transform.position + AwayDirection * (MinAttackRange - Distance); 
                        agent.SetDestination(MoveAway);
                        Quaternion lookRotation = Quaternion.LookRotation(Target.transform.position - transform.position);
                        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime *10f);
                       
                    }

                    else if(Distance > MaxAttackRange)
                    {
                        agent.isStopped = false;
                        attackRange = false;
                        agent.SetDestination(Hit.point);
                      
                        
                    }
                    print(Distance); 
                   
                    if(Distance > 3.1f) {
                        _animator.SetBool("isRunning", true);
                        _animator.SetBool(isIdleHash, false);

                    }
                    else if (Distance < 2.5f){
                        _animator.SetBool("isBackrun", true);
                        _animator.SetBool(isIdleHash, false);
                    }
                    else{
                        _animator.SetBool("isRunning", false);
                        _animator.SetBool(isIdleHash, true);
                        _animator.SetBool("isBackrun", false);
                    }
                    
                }   

                }
            }
        }
        
    
    
    private void Attack(){
        if (Time.time >= LastAttack + Cooldown){
            transform.LookAt(Target.transform.position);
            Instantiate(EnemyBullet, transform.position , Quaternion.identity);
            LastAttack = Time.time; //reset cooldown basically
        }
    }
}
