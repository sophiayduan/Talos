using System.Runtime.CompilerServices;
using JetBrains.Annotations;
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
    // public Canvas DeathScreen;
    public GameObject diedscreen;


    void Start()
    { 
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
        easeHealthSlider.value = currentHealth;
        diedscreen.SetActive(false);


    }
    void Update()
    {
        if (healthSlider.value != currentHealth)
        {
            healthSlider.value = currentHealth;
        }
        if (Input.GetKeyDown(KeyCode.Space)){
            takeDamage(10); 
            healthSlider.value = currentHealth;
            Instantiate(particles,transform.position,Quaternion.identity);

        }
        if (Mathf.Abs(easeHealthSlider.value - currentHealth) < 0.1f)  
        {
            easeHealthSlider.value = currentHealth;
        }

        if (healthSlider.value != easeHealthSlider.value)  
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, healthSlider.value, lerpSpeed );
        } 


        // if (healthSlider.value != easeHealthSlider.value)
        // {
        //     easeHealthSlider.value = Mathf.MoveTowards(easeHealthSlider.value, currentHealth, lerpSpeed * Time.deltaTime * 10);
        // }
    }

    public void takeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"Current health: {currentHealth}");

        if(currentHealth <= 0)
        {
            died();
            Debug.Log("you died");
            currentHealth = maxHealth;
        }
 
    }
    void died()
    {
        diedscreen.SetActive(true);
        Time.timeScale = 0f;
        currentHealth =
        easeHealthSlider.value = currentHealth;
        healthSlider.value = currentHealth;


    }


}
    
