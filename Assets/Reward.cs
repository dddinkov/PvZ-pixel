using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    [SerializeField]
    private float flightDistance = 6.0f;
    [SerializeField]
    private float speed = 7.0f;
    private Vector2[] points;
    [SerializeField]
    private float duration = 1.6f;
    float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        points = new Vector2[3];
        Transform transform = gameObject.transform;
        points[0] = transform.position;
        points[1] = new Vector2(transform.position.x, transform.position.y + Random.Range(flightDistance / 2, flightDistance));
        points[2] = new Vector2(transform.position.x - Random.Range(flightDistance / 2, flightDistance), transform.position.y - Random.Range(flightDistance/3, flightDistance * 2 / 3));
    }

    // Update is called once per frame
    void Update()
    {
        if (t <= 1.0f)
        {
            float x = (1 - t) * (1 - t) * points[0].x + 2 * (1 - t) * t * points[1].x + t * t * points[2].x;
            float y = (1 - t) * (1 - t) * points[0].y + 2 * (1 - t) * t * points[1].y + t * t * points[2].y;
            transform.position = new Vector3(x, y, 0);
            t += Time.deltaTime / duration;
        }
    }
    
}
