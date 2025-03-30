using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class PlayerShoot : MonoBehaviour
{
    public GameObject playerBullet, playerGun;
    public float cooldown, maxDistance, bulletsPerShot, spreadRange, timeBetweenShots, currentBulletAmount, maxBulletAmount, shotsAmount;

    public float spread;
    public float lastAttack;
    public GameObject firepoint;
    public Transform aimPos;
    [SerializeField] float aimSmoothSpeed = 20f;
    public ParticleSystem muzzleFlash;
    public TextMeshProUGUI text;
    [SerializeField] private LayerMask layerMask;
    public void Shoot()
    {
        StartCoroutine(ShootRoutine());
        Debug.Log("ok Shoot() is running");
    }
    void Start()
    {
        currentBulletAmount = maxBulletAmount;
        spread = Random.Range(-spreadRange,spreadRange);
    }
    void Update()
    {               
        Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        text.text = currentBulletAmount + "/" + maxBulletAmount;

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime );
        }

        else 
        {
            Vector3 destination = ray.origin + ray.direction * maxDistance;  
            aimPos.position = Vector3.Lerp(aimPos.position, destination, aimSmoothSpeed * Time.deltaTime);
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            Shoot();

        }
    }
    private void Shooting(){
        if (Time.time >= lastAttack + cooldown)
        {
            Debug.Log("shooting");
            currentBulletAmount -= 1;
            if (currentBulletAmount <= 0) {gunBreaks(); return;}
            Instantiate(playerBullet, firepoint.transform.position, Quaternion.identity);

            ParticleSystem flash = Instantiate(muzzleFlash,firepoint.transform.position,Quaternion.identity);
            Destroy(flash.gameObject, 0.1f);

            lastAttack = Time.time;
        }
    }

    IEnumerator ShootRoutine()
    {
        float count = 0f;
        while(count < bulletsPerShot)
        {
            Debug.Log("in coroutine");
            count += 1;
            Shooting();
            yield return new WaitForSeconds(timeBetweenShots);

        }
    }

    private void gunBreaks(){
        Destroy(playerGun);
    }
}
