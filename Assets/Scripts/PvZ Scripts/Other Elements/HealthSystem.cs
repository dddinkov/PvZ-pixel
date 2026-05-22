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
    [SerializeField]
    private bool hasAnimatorHealthParameter = false;
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

        UpdateAnimatorHealthParameter();
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
                float damagedArmorHealth = armorHealthSystem.GetHealth() - damage;
                armorHealthSystem.TakeDamage(damage);
                if(damagedArmorHealth < 0)
                {
                    // If the armor is destroyed, we will take damage from the zombie itself
                    hp += damagedArmorHealth; // damagedArmorHealth is negative, so we are actually subtracting the remaining damage from the zombie's health
                }
            }
            else
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

    private void UpdateAnimatorHealthParameter()
    {
        if (hasAnimatorHealthParameter)
        {
            animator.SetFloat("Health", hp);
        }
    }
}
