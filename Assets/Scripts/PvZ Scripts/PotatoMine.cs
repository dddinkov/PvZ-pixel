using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoMine : MonoBehaviour
{
    [SerializeField]
    float damage;
    [SerializeField]
    float cooldown;
    [SerializeField]
    float distance;
    Animator animator;
    Collider2D trigger;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        trigger = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            animator.SetBool("awake", true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Zombie" && animator.GetBool("awake"))
        {
            Explode();
        }
    }

    void Explode()
    {
        RaycastHit2D[] rHits = Physics2D.RaycastAll(gameObject.transform.position, Vector2.right, distance);
        RaycastHit2D[] lHits = Physics2D.RaycastAll(gameObject.transform.position, Vector2.left, distance);

        animator.SetBool("explode", true);

        DamageZombies(rHits);
        DamageZombies(lHits);

        trigger.enabled = false;
    }

    void DamageZombies(RaycastHit2D[] hits)
    {
        foreach (RaycastHit2D hit in hits)
        {
            GameObject target = hit.transform.gameObject;
            if (target.CompareTag("Zombie"))
            {
                HealthSystem hs = target.GetComponent<HealthSystem>();
                hs.TakeHealth(damage);
            }
        }
    }
}
