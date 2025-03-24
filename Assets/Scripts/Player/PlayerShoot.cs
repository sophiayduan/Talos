using UnityEngine;
using CameraShake;
using System;

public class PlayerShoot : MonoBehaviour
{
    public GameObject playerBullet;
    public float Cooldown = 3f;
    // public Slider cooldownSlider;
    public float maxDistance;
    private RaycastHit hit;
    public Vector3 destination;
    public float lastAttack;
    public GameObject firepoint;
    public Transform flashpoint;
public Transform aimPos;
    [SerializeField] float aimSmoothSpeed = 20f;
    public string destinationpoint;
    public Camera cam;
    public ParticleSystem muzzleFlash;
    [SerializeField] 
    PerlinShake.Params shakeParams;
    [SerializeField] private LayerMask layerMask;
    

    void Update()
    {               
        Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)){
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime );
            }
        else {
            Vector3 destination = ray.origin + ray.direction * maxDistance;  
            aimPos.position = Vector3.Lerp(aimPos.position, destination, aimSmoothSpeed * Time.deltaTime);
            destinationpoint = "air";
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            Shoot();
        }
    }
    private void Shoot(){
        if (Time.time >= lastAttack + Cooldown){
            Instantiate(playerBullet, firepoint.transform.position, Quaternion.identity);
            ParticleSystem flash = Instantiate(muzzleFlash,firepoint.transform.position,Quaternion.identity);
            Destroy(flash.gameObject, 0.1f);
            lastAttack = Time.time;
        }
    }
}
