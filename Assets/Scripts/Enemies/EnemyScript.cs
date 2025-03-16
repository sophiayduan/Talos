using System.IO;
using Unity.Mathematics;
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

    //Attacking 
    public float Cooldown;
    public float MinAttackRange;
    public float MaxAttackRange;
    public float LastAttack;
    public GameObject EnemyBullet;

    public LayerMask GroundLayer;
    private Vector3 Heading;
    private float Distance;
    public GameObject firepoint;

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
            Vector3 theorigin = transform.position;
            transform.position = new Vector3(theorigin.x, theorigin.y * 1.2f, theorigin.z);
            if(Physics.Raycast(theorigin, Target.transform.position - transform.position, out Hit, SightRange, layerMask)){
                Debug.Log("Raycast hit: " + Hit.collider.name);

                if (Hit.collider.tag == "Player")
                {
                    Animation();
                    Heading = Target.transform.position - transform.position;
                    Distance = Heading.magnitude;
                    Debug.Log("okay this is the player");

                    if(Distance <= MaxAttackRange && Distance >= MinAttackRange){Chase();Attack();}
                    else if(Distance < MinAttackRange + 1 && Distance > MinAttackRange){Stop();Attack();}
                    else if(Distance < MinAttackRange)Retreat();
                    else if(Distance > MaxAttackRange) Chase();
                }   

            }
        }
    }
        

    private void Attack(){
        if (Time.time >= LastAttack + Cooldown){
            transform.LookAt(Target.transform.position);
            Instantiate(EnemyBullet, firepoint.transform.position , Quaternion.identity);
            LastAttack = Time.time;
        }
    }
    private void Stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        transform.LookAt(Target.transform.position);
    }

    private void Retreat(){
        agent.isStopped = false;
        Vector3 AwayDirection = (transform.position - Target.transform.position).normalized;
        Vector3 MoveAway = transform.position + AwayDirection * (MinAttackRange - Distance); 
        agent.SetDestination(MoveAway);
        Quaternion lookRotation = Quaternion.LookRotation(Target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime *10f);
    }

    private void Chase(){
        agent.isStopped = false;
        agent.SetDestination(Hit.point);
    }
    private void Animation(){
        if (Distance > 3.1f) {
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
