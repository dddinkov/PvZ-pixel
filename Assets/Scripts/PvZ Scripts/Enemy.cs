using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public Transform[] path;
    [SerializeField]
    public float speed = 3000f;

    private Transform target;
    private Rigidbody2D rb;
    private int i = 0;
    void Start()
    {
        target = path[i];
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(target.position, gameObject.transform.position);
        if (distance >= 0.1f)
        {
            rb.velocity = (target.position - gameObject.transform.position).normalized * Time.deltaTime * speed;
        }
        else if (i < path.Length - 1)
        {
            i++;
            target = path[i];
        }
        else
        {
            EnemyManager.enemyManager.enemiesAlive--;
            Destroy(gameObject);
        }
    }
}
