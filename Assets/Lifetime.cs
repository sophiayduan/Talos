using UnityEngine;
using UnityEngine.UI;
public class Lifetime : MonoBehaviour
{
    public float lifetime;
    public float maxLifetime = 100f;
    public float deathSpeed;
    public Slider healthSlider;
    void Start()
    {
        lifetime = maxLifetime;
    }

    void Update()
    {
        if(lifetime > 0){
            lifetime -= Time.deltaTime * deathSpeed;
            healthSlider.value = lifetime;

        }
        else if (lifetime <= 0){
            GameManager.instance.GameOver();
        }

        if(Input.GetKeyDown(KeyCode.B)){
            lifetime += 5f;
        }
        
        if(lifetime > maxLifetime) lifetime = maxLifetime;

       
    }
}
