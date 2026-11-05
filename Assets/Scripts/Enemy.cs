using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] private float detectionRange = 10.0f;
    [SerializeField] private float speed = 3;

    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
        health.OnDeath += Die;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if(distance < detectionRange)
        {
            float step = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        }
    }

    void Die()
    {
        Destroy(gameObject);

        if(health != null) 
            health.OnDeath -= Die;
    }
}
