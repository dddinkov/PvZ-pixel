using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5.0f;
    public float damage = 1.0f;
    protected Rigidbody2D rb;
    private float timeToLive = 20.0f;
    private SoundManager shootSoundManager;
    private SoundManager splashSoundManager;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        shootSoundManager = GameObject.Find("PopSoundManager").GetComponent<SoundManager>();
        splashSoundManager = GameObject.Find("SplashSoundManager").GetComponent<SoundManager>();
        shootSoundManager.PlaySound();
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

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("Zombie"))
        {
            HealthSystem script = trigger.gameObject.GetComponent<HealthSystem>();
            script.TakeDamage(damage);
            Destroy(gameObject);
            splashSoundManager.PlaySound();
        }
    }
}
