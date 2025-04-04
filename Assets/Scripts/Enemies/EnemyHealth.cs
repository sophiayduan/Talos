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
            gameObject.SetActive(false);
            Debug.Log("enemy died");
        }
 
    }



}
    
