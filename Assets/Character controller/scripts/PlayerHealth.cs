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
    public GameObject  respawnpoint;
    public float lastHeal ;
    public float healAmount = 5f;
    public float cooldown = 5f;

    void Start()
    { 
        currentHealth = maxHealth;
        lifetime = FindFirstObjectByType<Lifetime>();
    }
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Space))
        {
            takeDamage(10); 
            healthSlider.value = currentHealth;
            // Instantiate(particles,transform.position,Quaternion.identity);
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
    }
    public void takeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"Current health: {currentHealth}");
        if(currentHealth <= 0)
        {
            if(lifetime != null && lifetime.running())
            {
                Respawn();
            }
            else 
            {
                print("respawn");
                GameManager.instance.GameOver();
            }    
        }
    }
    void Respawn(){
        currentHealth = maxHealth;
        transform.position = respawnpoint.transform.position;
    }
}
    
