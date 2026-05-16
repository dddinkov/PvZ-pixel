using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float damage = 1.0f;
    public float speed = 20.0f;
    public float attackCooldown = 0.5f;
    private Rigidbody2D rb;
    private float time = 0.0f;
    private bool isMoving;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        time = attackCooldown;
        rb = GetComponent<Rigidbody2D>();
        isMoving = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.velocity = Vector2.left * speed * Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void Update()
    {
        time = time + Time.deltaTime;
        if(time >= attackCooldown + 0.2f)
        {
            if (anim.GetBool("Dead"))
            {
                isMoving = false;
                return;
            }
            isMoving = true;
            anim.SetBool("Eat", false);
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Plant") && time >= attackCooldown)
        {
            anim.SetBool("Eat", true);
            isMoving = false;
            rb.velocity = Vector3.zero;
            HealthSystem script = coll.gameObject.GetComponent<HealthSystem>();
            script.TakeHealth(damage);
            time = 0.0f;
        }
    }
}
