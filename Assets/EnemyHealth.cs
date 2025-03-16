using System.Data;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{        
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float maxHealth = 100f;
    public float enemyHealth;
    [SerializeField] private ParticleSystem enemyParticles;
    private float lerpSpeed = 0.05f;


    void Start()
    { 
        enemyHealth = maxHealth;

    }
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Space))
        {
            takeDamage(10); 
            healthSlider.value = enemyHealth;
            // Instantiate(particles,transform.position,Quaternion.identity);
        }

        if (healthSlider.value != enemyHealth)
        {
            healthSlider.value = enemyHealth;
        }

        if (easeHealthSlider.value - healthSlider.value < 0.1f) easeHealthSlider.value = healthSlider.value;

        if (healthSlider.value != easeHealthSlider.value)  
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, enemyHealth, lerpSpeed );
        }
    }
    public void takeDamage(float amount)
    {
        enemyHealth -= amount;

        if(enemyHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("enemy died");
        }
 
    }



}
    
