using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;
    // a sorted (descending) array of hp thresholds for each sprite
    [SerializeField]
    private int[] healthStates;
    private int state;
    private HealthSystem healthSystem;
    private SpriteRenderer spriteRenderer;

     void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

     void LateUpdate()
    {
        spriteRenderer.sprite = sprites[state];
    }

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        healthSystem = GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(healthSystem.GetHealth() <= healthStates[state])
        {
            state++;
        }
    }
}
