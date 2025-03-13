using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Vector3 target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;

        // target = new Vector3(player.position.x, player.position.y, player.position.z);
        target = player.transform.position;
        transform.LookAt(target);

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target) < 0.5f) 
        {
            Destroy(gameObject);
        }
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);


    }
}
