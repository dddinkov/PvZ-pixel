using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;

    private float time = 0.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;
        RaycastHit2D[] raycastHit = Physics2D.RaycastAll(firePoint.position, firePoint.TransformDirection(Vector2.right),1000.0f);
        if(time >= 3.0f && raycastHit.Length > 0)
        {
            foreach (RaycastHit2D hit in raycastHit)
            {
                if (hit.collider.gameObject.CompareTag("Zombie"))
                {
                    Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                    time = 0.0f;
                }
            }
        }
        if(time >= 10000.0f)
        {
            time = 3.0f;
        }
    }

}
