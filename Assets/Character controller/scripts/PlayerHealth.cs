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
    public GameObject player;
    public GameObject map;
    public float lastHeal;
    public float healAmount = 1f;
    public float cooldown = 5f;

    void Start()
    { 
        currentHealth = maxHealth;

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
        if (currentHealth < 100)
        {
            heal();
            Debug.Log("heal time");
        }

    } 
      
    public void takeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"Current health: {currentHealth}");

        if(currentHealth <= 0)
        {
            died();
            Debug.Log("you died");
        }
    }
    private void died()
    {
        GameManager.instance.GameOver();
        player.SetActive(false);        

        map.SetActive(false);
        Time.timeScale = 0f;

    }
    private void heal()
    {
        if (Time.time - lastHeal > cooldown)
        {
            currentHealth = Mathf.Clamp(currentHealth + healAmount, 0, maxHealth);
            healthSlider.value = currentHealth;
            lastHeal = Time.time;

        }
    }
}
    
