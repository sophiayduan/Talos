using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{        
    public static Slider healthSlider;
    public static Slider easeHealthSlider;
    public static float maxHealth = 100f;
    public float currentHealth;
    [SerializeField] private ParticleSystem particles;
    private float lerpSpeed = 0.05f;
    // public Canvas DeathScreen;
    public GameObject diedscreen;


    void Start()
    { 
        currentHealth = maxHealth;
        // healthSlider.value = currentHealth;
        easeHealthSlider.value = currentHealth;
        diedscreen.SetActive(false);


    }
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Space)){
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
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, currentHealth, lerpSpeed * Time.deltaTime * 20);
        }
    }
    public void takeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"Current health: {currentHealth}");
        healthSlider.value = currentHealth;
        if(currentHealth <= 0)
        {
            died();
            Debug.Log("you died");
        }
 
    }
    void died()
    {
        diedscreen.SetActive(true);
        Time.timeScale = 0f;
        // currentHealth = maxHealth;
        easeHealthSlider.value = currentHealth;
        healthSlider.value = currentHealth;


    }


}
    
