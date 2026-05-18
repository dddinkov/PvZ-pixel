using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        if (!TryToTakeHealthFromZombie(damage))
        {
            hp -= damage;
        }

        // If somebody is dying
        HandleDying();
    }
    
    // Handles dying logic
    private void HandleDying()
    {
        if (hp <= 0)
        {
            if (!TryZombieDyingLogic())
            {
                Destroy(gameObject);
            }
        }
    }

    // Triggers dying animation for zombies, returns true if the game object is a zombie, false otherwise
    private bool TryZombieDyingLogic()
    {
        if (gameObject.CompareTag("Zombie"))
        {
            Animator anim = GetComponent<Animator>();
            // The animator should handle the destruction of the game object, so we just trigger the dying animation and return
            anim.SetBool("Dead", true);
            return true;
        }
        return false;
    }

    // We will check for attached armor and try to take damage from it, returns true if the damage was taken, false otherwise
    private bool TryToTakeHealthFromZombie(float damage)
    {
        if (gameObject.CompareTag("Zombie"))
        {
            // We will check for attached armor and try to take damage from it
            // Suggestion: What if we are to add multiple layers of armor?
            HealthSystem armorHealthSystem = gameObject.GetComponentsInChildren<HealthSystem>().FirstOrDefault(h => h.gameObject != gameObject);
            if (armorHealthSystem != null)
            {
                armorHealthSystem.TakeHealth(damage);
            }
            else // If there is no armor, we will take damage from the zombie itself
            {
                hp -= damage;
            }
            return true;
        }
        return false;
    }
}
