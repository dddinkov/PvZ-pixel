using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : Projectile
{
    [SerializeField]
    private float rotationSpeed = 200f;
    private Transform target;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        FindNearestTarget();
    }

    void FixedUpdate()
    {
        if(target == null)
        {
            FindNearestTarget();
        }
        if(target != null)
        {
            Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.right).z;
            rb.angularVelocity = -rotateAmount * rotationSpeed;
        }
        rb.velocity = transform.right * speed * Time.deltaTime;
    }

    void FindNearestTarget()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        float closestDistance = Mathf.Infinity;
        GameObject closestZombie = null;

        foreach (GameObject zombie in zombies)
        {
            float distance = Vector2.Distance(transform.position, zombie.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestZombie = zombie;
            }
        }

        if (closestZombie != null)
        {
            target = closestZombie.transform;
        }
    }
}
