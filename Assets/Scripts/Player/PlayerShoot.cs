using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject playerBullet;
    public float Cooldown = 3f;
    // public Slider cooldownSlider;
    public float maxDistance = 10f;
    private RaycastHit hit;
    public Vector3 destination;
    public float lastAttack;
    public GameObject firepoint;
    public Camera cam;
    [SerializeField] private LayerMask layerMask;

    void Update()
    {               
        Vector3 screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 1);
        Ray ray = cam.ScreenPointToRay(screenCenter);


        if (Physics.Raycast(ray, out hit, maxDistance, layerMask)){

            destination = hit.point;
            Debug.Log("found");
            Debug.DrawLine(ray.origin, hit.point, Color.red, 2f);
            }
        else {
            destination = ray.origin + ray.direction * maxDistance;  
            Debug.DrawLine(ray.origin, destination, Color.blue, 2f);
       
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            Shoot();
        }
    }
    private void Shoot(){
        if (Time.time >= lastAttack + Cooldown){
            // transform.LookAt(destination.normalized);
            Instantiate(playerBullet, firepoint.transform.position, Quaternion.identity);
            lastAttack = Time.time;
        }
    }
}
