using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{        public Slider healthSlider;

        public float maxHealth = 100f;
        // [SerializeField] private float currentHealth;
        public float currentHealth;
        // public Slider easeHealthSlider;
        // private float lerpSpeed = 0.05f;


    void Start()
    {
        currentHealth = maxHealth;
    }
    void Update()
    {
        if (healthSlider != null){
            if (healthSlider.value != currentHealth)
                {
                    healthSlider.value = currentHealth;
                }
        }
        else if (healthSlider == null){
            healthSlider = FindFirstObjectByType<Slider>();
            Debug.LogError("healthslider  is == null");
        }
  
        if (Input.GetKeyDown(KeyCode.Space)){
            takeDamage(10);
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
    