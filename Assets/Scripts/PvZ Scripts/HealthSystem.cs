using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float hp = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeHealth(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            if (gameObject.CompareTag("Zombie"))
            {
                Animator anim = GetComponent<Animator>();
                anim.SetBool("Dead", true);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
