using System.Data;
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
    public GameObject playerModel;
    public GameObject map;
    // public GameObject diedscreen;


    void Start()
    { 
        currentHealth = maxHealth;
        // diedscreen.SetActive(false);
        // if (diedscreen == null){
        //     Debug.LogError("fuck");
        // }


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
        playerModel.SetActive(false);
        map.SetActive(false);
        Time.timeScale = 0f;

    }


}
    
