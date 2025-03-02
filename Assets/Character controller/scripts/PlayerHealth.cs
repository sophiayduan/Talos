using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{        
    public Slider Slider;


    public float maxHealth = 100f;
    // [SerializeField] private float currentHealth;
    public float currentHealth;
    [SerializeField] private ParticleSystem particles;
    // private ParticleSystem damageparticlesInstance;

    // public Slider easeHealthSlider;
    // private float lerpSpeed = 0.05f;


    void Start()
    {
        currentHealth = maxHealth;
        Slider = FindFirstObjectByType<Slider>();

    }
    void Update()
    {
        if (Slider != null){
            if (Slider.value != currentHealth)
                {
                    Slider.value = currentHealth;
                }
        }
        else if (Slider == null){
            // Slider = FindFirstObjectByType<Slider>();
            Debug.LogError("healthslider  is == null");
        }
  
        if (Input.GetKeyDown(KeyCode.Space)){
            takeDamage(10); 
            Slider.value = currentHealth;
            Instantiate(particles,transform.position,Quaternion.identity);


        }
        // if (healthSlider.value != easeHealthSlider.value)
        // {
        //     easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, currentHealth, lerpSpeed);
        // }
    }

    public void takeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"Current health: {currentHealth}");

        if(currentHealth <= 0)
        {
            // youdied();
            Debug.Log("you died");
            currentHealth = maxHealth;
        }
 
    }
    // void youdied()
    // {
    //     Destroy(gameObject);

    // }


}
    
