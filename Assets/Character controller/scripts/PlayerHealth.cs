using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{        
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float maxHealth = 100f;
    public float currentHealth;
    [SerializeField] private ParticleSystem particles;
    private float lerpSpeed = 0.05f;
    public GameObject playerModel;
    private Lifetime lifetime;
    // public GameObject respawnpoint;
    public float lastHeal ;
    public float healAmount = 5f;
    public float cooldown = 5f;
    public SetSpawn setSpawn;
    void Start()
    { 
        currentHealth = maxHealth;
        lifetime = FindFirstObjectByType<Lifetime>();
    }
    void Update()
    {   
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     takeDamage(10); 
        //     healthSlider.value = currentHealth;
        //     // Instantiate(particles,transform.position,Quaternion.identity);
        // }

        if (healthSlider.value != currentHealth)
        {
            healthSlider.value = currentHealth;
        }

        if (easeHealthSlider.value - healthSlider.value < 0.1f) easeHealthSlider.value = healthSlider.value;

        if (healthSlider.value != easeHealthSlider.value)  
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, currentHealth, lerpSpeed );
        }

        
        if (currentHealth < 100f)
        {
            if (Time.time >= lastHeal+ cooldown && easeHealthSlider.value == healthSlider.value){
                Debug.Log("healing");
                currentHealth += healAmount;
                lastHeal = Time.time;
            }
        }
    }
    public void takeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"Current health: {currentHealth}");
        if(currentHealth <= 0)
        {
            Respawn();
            // if(lifetime != null && lifetime.running())
            // {
            //     Respawn();
            // }
            // else 
            // {
            //     print("respawn");
            //     GameManager.instance.GameOver();
            // }    
        }
    }
    void Respawn(){ 
        Vector3 respawnPoint;

        respawnPoint = new Vector3(147, 125, 806);
        gameObject.transform.position = respawnPoint;

        // if(SetSpawn.newSpawn != Vector3.zero) respawnPoint = SetSpawn.newSpawn;
        // else {
        //     respawnPoint = new Vector3(147, 125, 806);
        // }
        // currentHealth = maxHealth;
        // if(setSpawn != null){
        //     Debug.Log($"where: {respawnPoint}");
        //     gameObject.transform.position = respawnPoint;

        //     Debug.Log("uh i hope");
        // }
        // else if (setSpawn == null){
        //     gameObject.transform.position = respawnPoint;
        //     Debug.Log("ts null");
        // }
        // Debug.Log($"new transform.postion = {transform.position}");
    }
}
    
