using UnityEngine;
using UnityEngine.AI;



public class EnemyScript : MonoBehaviour
{    
    public NavMeshAgent agent;
    public float speed;

    public float SightRange,DetectionRange, Cooldown, MinAttackRange, MaxAttackRange, LastAttack, Distance;
    public bool seePlayer;
    private Collider[] hitColliders;
    private RaycastHit Hit;

    public GameObject EnemyBullet, firepoint, Target;
    public LayerMask GroundLayer;
    private Vector3 Heading, lastKnownPosition, walkPoint;
    public bool walkPointSet;
    public int walkPointRange = 10;
    [SerializeField] private Animator _animator;
    private static int isIdleHash = Animator.StringToHash("isIdle");
    [SerializeField] private LayerMask layerMask;
    public float startPause,pauseTime = 0;
    public bool isPaused;
    public Transform LKPsphere;
   

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
                    Debug.Log("player gone too far, byebye enemy");
                    //enemyHealth.Deactivate();

                    // Patrol();
                    

                }
            }
        }
     
        else
        {   
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
                    lastKnownPosition = Hit.point; 
                    LKPsphere.position = Hit.point;


                    if(Distance <= MaxAttackRange && Distance >= MinAttackRange) {Chase();Attack();}
                    else if(Distance < MinAttackRange + 1 && Distance > MinAttackRange){Stop();Attack();}
                    else if(Distance < MinAttackRange)Retreat();
                    else if(Distance > MaxAttackRange) Chase();

                }
                else
                {
                    LastPosition();
                }   
                

            }
            else {
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
        if ((transform.position - lastKnownPosition).magnitude < 0.1f ){
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

        if(distanceToWalkPoint.magnitude <= 0.5f){   
            // walkPointSet = false;    
            Pause();

        }
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX,transform.position.y + 10f, transform.position.z + randomZ);

        // if (Physics.Raycast(walkPoint, -transform.up , out Hit, 2.5f, GroundLayer)){
        if(Physics.Raycast(walkPoint, Vector3.down, out Hit, 20f, GroundLayer)){
            Debug.DrawRay(walkPoint, Hit.point,Color.blue, 2f);
            walkPoint = Hit.point;
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

                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                agent.updateRotation = false;

                _animator.SetBool("isRunning", false);
                _animator.SetBool(isIdleHash, true);
                _animator.SetBool("isBackrun", false);
            }
    
            else {
                Debug.Log("unpause correeect!");
                agent.isStopped = false; 
                agent.updateRotation = true;
                isPaused = false;
                walkPointSet = false;
            }
        }

    }
}
