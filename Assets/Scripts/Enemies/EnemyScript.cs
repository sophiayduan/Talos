using UnityEngine;
using UnityEngine.AI;


public class EnemyScript : MonoBehaviour
{    
    public Rigidbody rb;
    public float Maxspeed;
    private float Speed;
    // public NavMeshAgent agent;

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

    private Vector3 lastKnownPosition;
    private bool lostPlayer;

    private float lastSeenTime;
    public float searchTime = 5f;
    // Patrolling 
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    [SerializeField] private LayerMask layerMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Speed = Maxspeed;
        // agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!seePlayer)
        {
            // Patrolling();
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
            if(Physics.Raycast(transform.position, Target.transform.position -transform.position, out Hit, SightRange, layerMask)){
                Debug.Log("Raycast hit: " + Hit.collider.name);
                if (Hit.collider.tag == "Player"){
                    // agent.SetDestination(Hit.point);
                    Debug.Log("okay this is the player");
                    var Heading = Target.transform.position - transform.position;
                    var Distance = Heading.magnitude;
                    var Direction = Heading / Distance;

                    Vector3 Move = new Vector3(Direction.x * Speed, 0, Direction.z * Speed);

                    rb.linearVelocity = Move;
                    transform.forward = Move;

                    if(Distance <= MaxAttackRange && Distance >= MinAttackRange)
                    { 
                        attackRange = true;
                        rb.linearVelocity = Move;
                        transform.forward = Move;
                        // rb.linearVelocity = Vector3.zero;
                        Attack();
                        if (Distance > MinAttackRange && Distance < MinAttackRange +1 ){
                            rb.linearVelocity = Vector3.zero;
                        }
                    }
     
                    else if(Distance < MinAttackRange )
                    {
                        attackRange = false;
                        Vector3 AwayDirection = (transform.position - Target.transform.position).normalized;
                        Vector3 MoveAway = new Vector3(AwayDirection.x * Speed, 0, AwayDirection.z * Speed);
                        rb.linearVelocity = MoveAway;
                    }

                    else if(Distance > MaxAttackRange)
                    {
                        attackRange = false;
                        rb.linearVelocity = Move;
                        transform.forward = Move;

                    }
                }   
                else if (Hit.collider.tag == "Object"){
                    // lostPlayer = true;
                     //also add for noise and if reaches last known position and there is nothing, then patrol
                }
                // if (lostPlayer){
                //     agent.SetDestination(lastKnownPosition);
                //     Vector3.Distance(transform.position, lastKnownPosition);
                
 
                        
                        

                //     }

                }
            }
        }
    
    
    // private void Patrolling(){
    //     if(!walkPointSet) SearchWalkPoint();
    //     if(walkPointSet)
    //         agent.SetDestination(walkPoint);
        
    //     Vector3 distanceToWalkPoint = transform.position - walkPoint;

    //     if(distanceToWalkPoint.magnitude < 1f)
    //         walkPointSet = false;
    // }
    // private void SearchWalkPoint(){
    //     float randomZ = Random.Range(-walkPointRange, walkPointRange);
    //     float randomX = Random.Range(-walkPointRange, walkPointRange);

    //     walkPoint = new Vector3(transform.position.x + randomX,transform.position.y, transform.position.z + randomZ);

    //     if (Physics.Raycast(walkPoint, -transform.up, 2f, GroundLayer))
    //         walkPointSet = true;
    // }
    // private void Follow(){
        //tbd if want to change it do this
    // }
    private void Attack(){
        if (Time.time >= LastAttack + Cooldown){
            transform.LookAt(Target.transform.position);
            Instantiate(EnemyBullet, transform.position, Quaternion.identity);
            LastAttack = Time.time; //reset cooldown basically
        }
    }
}
