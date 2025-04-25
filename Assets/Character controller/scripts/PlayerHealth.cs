using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{        
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float maxHealth = 100f;
    public float currentHealth;
    private float lerpSpeed = 0.05f;
    private Lifetime lifetime;
    public float lastHeal ;
    public float healAmount = 5f;
    public float cooldown = 5f;
    public GameObject capsule;
    void Start()
    { 
        currentHealth = maxHealth;
        lifetime = FindFirstObjectByType<Lifetime>();
    }
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Space))
        {
            takeDamage(40); 
            healthSlider.value = currentHealth;
        }

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
        if(lifetime == null || !lifetime.running()){
                GameManager.instance.GameOver();

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
        // respawnPoint = new Vector3(0, 0, 0);

        if(SetSpawn.newSpawn != Vector3.zero && SetSpawn.newSpawn != null) 
        {
            respawnPoint = SetSpawn.newSpawn; 
            Debug.Log("NOT NULL WTF");
        }

        else {
            respawnPoint = capsule.transform.position;
            Debug.Log("it is null!");
        }

            gameObject.transform.position = respawnPoint;

        currentHealth = maxHealth;

        Debug.Log($"new transform.postion = {transform.position}");
    }
}
    
