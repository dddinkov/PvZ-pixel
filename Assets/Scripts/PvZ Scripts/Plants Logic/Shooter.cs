using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;

    [SerializeField]
    protected Animator animator;

    private float time = 0.0f;
    [SerializeField]
    private float shootCooldown = 3.0f;

    void Update()
    {
        time = time + Time.deltaTime;
        if(CanShoot() && time >= shootCooldown)
        {
            Shoot();
        }
    }

    protected abstract bool CanShoot();

    protected virtual void Shoot()
    {
        if(animator != null)
        {
            animator.SetTrigger("Shoot");
        }
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        time = 0.0f;
    }
}