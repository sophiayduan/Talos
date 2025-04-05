using System.Data;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{   
    [Header(" ")]     
    public Slider healthSlider;
    public Slider easeHealthSlider;
    private float lerpSpeed = 0.05f;

    [Header(" ")]
    public float maxHealth = 50f;
    public float enemyHealth;
    [SerializeField] private ParticleSystem enemyParticles;

    public delegate void DeactivateHandler();
    public event DeactivateHandler OnDeactivate;
    void Start()
    { 
        enemyHealth = maxHealth;

    }
    void Update()
    {   
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     takeDamage(10); 
        //     healthSlider.value = enemyHealth;
        //     // Instantiate(enemyParticles,transform.position,Quaternion.identity);
        // }

        if (healthSlider.value != enemyHealth)
        {
            healthSlider.value = enemyHealth;
        }
        if (healthSlider == null) print("shit");
        if (easeHealthSlider == null) print("fuck");

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
            Deactivate();
            
        }
 
    }

    public void Deactivate()
    {
        Destroy(gameObject); // Deactivates the enemy
        OnDeactivate?.Invoke(); // Triggers the deactivation event
    }



}
    
