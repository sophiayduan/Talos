using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
        public float maxHealth = 100f;
        [SerializeField] private float currentHealth;
        public Slider healthSlider;


    void Start()
    {
        currentHealth = maxHealth;
    }
    void Update()
    {
        if (healthSlider.value != currentHealth)
        {
            healthSlider.value = currentHealth;
        }
        if (Input.GetKeyDown(KeyCode.Space)){
            takeDamage(10);
        }
    }

    public void takeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"Damage Amount: {currentHealth}");

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
    