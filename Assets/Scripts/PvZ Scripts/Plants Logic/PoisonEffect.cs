using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisoningSystem : MonoBehaviour
{
    [SerializeField]
    private float dps = 0.5f;
    [SerializeField]
    private float duration = 5f;
    private SpriteRenderer spriteRenderer;
    private Collider2D trigger;
    private SoundManager gasSoundManager;

     void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        trigger = GetComponent<Collider2D>();
        gasSoundManager = GameObject.Find("GasSoundManager")?.GetComponent<SoundManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Zombie"))
        {
            HealthSystem system = other.GetComponent<HealthSystem>();
            if (system != null)
            {
                spriteRenderer.enabled = false;
                trigger.enabled = false;
                gasSoundManager?.PlaySound();
                StartCoroutine(ApplyDamageOverTime(system));
            }
        }
    }

    private IEnumerator ApplyDamageOverTime(HealthSystem system)
    {
        ZombieStateHandler stateHandler = system.GetComponent<ZombieStateHandler>();
        stateHandler?.SetState(ZombieState.Poisoned);

        float time = 0f;
        while (time < duration)
        {
            if(system == null || !system.gameObject.activeInHierarchy) yield break; // Check if the health system is destroyed before trying to apply damage
            system.TakeDamage(dps);
            yield return new WaitForSeconds(1f);
            time += 1f;
        }

        if(stateHandler != null && stateHandler.gameObject.activeInHierarchy)
        {
            stateHandler.SetState(ZombieState.Normal);
        }

        Destroy(gameObject);
    }
}
