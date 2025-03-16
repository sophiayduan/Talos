using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    public GameObject playerBullet;
    // public float playerDamage;
    public float Cooldown = 3f;
    // public Slider cooldownSlider;
    private float speed = 2f;
    // public GameObject where;
    public float maxDistance = 20f;
    private RaycastHit hit;
    public Vector3 destination;
    public float lastAttack;
    public GameObject firepoint;
    public GameObject findpoint;
    public Camera cam;
    

    [SerializeField] private LayerMask layerMask;


    void Start()
    {
        
    }

    void Update()
    {               
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, layerMask))       {

            destination = hit.point;
            Debug.Log("found");
            Debug.DrawLine(ray.origin, hit.point, Color.red, 2f);
            }
        else {
            Debug.LogError("not finding ");
            destination = ray.origin + transform.forward * maxDistance;  
            Debug.DrawLine(ray.origin, hit.point, Color.blue, 2f);
       
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C received");
            Shoot();
        }
    }
    private void Shoot(){
        if (Time.time >= lastAttack + Cooldown){
            // transform.LookAt(destination.normalized);
            Instantiate(playerBullet, firepoint.transform.position, Quaternion.identity);
            lastAttack = Time.time;
        }
        // transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime)
    }
}
