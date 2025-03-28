// using System.Numerics;

// using Unity.Mathematics;
// using System.Numerics;
// using System.Numerics;
using Unity.VisualScripting;
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

    private Vector3 lastKnownPosition;
    private bool lostPlayer;

    private float lastSeenTime;
    public float searchTime = 5f;

    // Patrolling 
    public Transform walksphere;
    public Vector3 walkPoint;
    public bool walkPointSet;
    public int walkPointRange = 10;
    [SerializeField] private Animator _animator;
    private static int isIdleHash = Animator.StringToHash("isIdle");
    [SerializeField] private LayerMask layerMask;
    public float startPause;
    public float pauseTime = 0;
    public string status = "nada";
    public Vector3 shootDestination;
    public bool isPaused;
    public Transform LKPsphere;
    void Start()
    {
        Speed = Maxspeed;
    }

    void Update()
    {
        if (!seePlayer)
        {
            Debug.Log("Time to patrol");
            Patrol();
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
                    // Debug.Log("start patrol");
                    seePlayer = false;
                    Patrol();
                    

                }
            }
        }
        else
        {   
            // lastKnownPosition = transform.position + Vector3.forward * 0.5f;
            Vector3 theorigin = transform.position + Vector3.up * 2f;
            if(Physics.Raycast(theorigin, (Target.transform.position - theorigin).normalized, out Hit, SightRange, layerMask)){
                Debug.DrawRay(theorigin,  (Target.transform.position - theorigin).normalized* SightRange, Color.black, 2f);
                Debug.Log("Raycast hit: " + Hit.collider.name);

                if (Hit.collider.tag == "Player")
                {
                    Animation();
                    Heading = Target.transform.position - transform.position;
                    Distance = Heading.magnitude;
                    Debug.Log("okay this is the player");
                    status = "Hit.collider.tag = Player";
                    lastKnownPosition = Hit.point; 
                    LKPsphere.position = Hit.point;


                    if(Distance <= MaxAttackRange && Distance >= MinAttackRange) {Chase();Attack();}
                    else if(Distance < MinAttackRange + 1 && Distance > MinAttackRange){Stop();Attack();}
                    else if(Distance < MinAttackRange)Retreat();
                    else if(Distance > MaxAttackRange) Chase();

                }
                else
                {
                    status = "Hit.collider.tag != Player";
                    LastPosition();
                }   
                

            }
            else {
                status = "no raycast";
                LastPosition();
                // Patrol(); 
                // Debug.Log("donde estas");
            }

        }
    }
        

    private void LastPosition(){
        // Pause();
        agent.SetDestination(LKPsphere.position);
        Debug.DrawRay(transform.position, lastKnownPosition, Color.green);
        status = "Set Destination LKP";
        if ((transform.position - lastKnownPosition).magnitude < 0.1f ){
            status = "Reached LKP";
            return;
      
        }
    }
    private void Attack(){
        if (Time.time >= LastAttack + Cooldown){
            // transform.LookAt(Target.transform.position);
            Instantiate(EnemyBullet, firepoint.transform.position , Quaternion.identity);
            LastAttack = Time.time;
        }
    }
    private void Stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        agent.updateRotation = false;

        Vector3 targetPosition = Target.transform.position;
        targetPosition.y = transform.position.y;

        Quaternion lookRotation = Quaternion.LookRotation(targetPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime *10f);
    }

    private void Retreat(){
        agent.isStopped = false;
        Vector3 AwayDirection = (transform.position - Target.transform.position).normalized;
        Vector3 MoveAway = transform.position + AwayDirection * (MinAttackRange - Distance); 
        agent.SetDestination(MoveAway);
        Vector3 targetPosition = Target.transform.position;
        targetPosition.y = transform.position.y;

        Quaternion lookRotation = Quaternion.LookRotation(targetPosition - transform.position);
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
    private void Patrol()
    {
        Animation();
        if(!walkPointSet) SearchWalkPoint();

        if(walkPointSet)  agent.SetDestination(walkPoint); Debug.Log("set dedestination");
        
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude <= 0.1f){       
            Pause();

        }
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX,transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up , out Hit, 2.5f, GroundLayer)){
            Debug.DrawRay(walkPoint, Hit.point,Color.blue, 2f);
            
            walkPointSet = true;
        }
        else walkPointSet = false;
    }

    private void Pause()
    {     
        if (!isPaused){
            startPause = Time.time; 
            isPaused = true;   

        }     
        if (isPaused) {
            if(Time.time < startPause + pauseTime)
            {
                Debug.Log("correct!!! pause");
                status = "pausing CORRECT";

                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                agent.updateRotation = false;

                _animator.SetBool("isRunning", false);
                _animator.SetBool(isIdleHash, true);
                _animator.SetBool("isBackrun", false);
            }
    
            else {
                Debug.Log("unpause correeect!");
                status = "unpausing!";
                agent.isStopped = false; 
                agent.updateRotation = true;
                isPaused = false;
                walkPointSet = false;
                // return; 
            }
        }

    }
}
