using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5.0f;
    public float damage = 1.0f;
    private Rigidbody2D rb;
    private float timeToLive = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * Time.deltaTime, 0);
    }

    private void Update()
    {
        timeToLive -= Time.deltaTime;
        if(timeToLive <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            HealthSystem script = collision.gameObject.GetComponent<HealthSystem>();
            script.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
