using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
    public float hp = 10.0f;
    [SerializeField]
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {

        if (!TryToTakeHealthFromZombie(damage))
        {
            hp -= damage;
        }

        // If somebody is dying
        HandleDying();

        UpdateAnimatorIfExists();
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
                armorHealthSystem.TakeDamage(damage);
            }
            else // If there is no armor, we will take damage from the zombie itself
            {
                hp -= damage;
            }
            return true;
        }
        return false;
    }

    public float GetHealth()
    {
        return hp;
    }

    private void UpdateAnimatorIfExists()
    {
        if (animator != null)
        {
            animator.SetFloat("Health", hp);
        }
    }
}
