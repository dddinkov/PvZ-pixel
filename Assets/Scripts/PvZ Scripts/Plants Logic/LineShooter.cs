using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class LineShooter : Shooter
{
    protected override bool CanShoot()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(firePoint.position, Vector2.right, Mathf.Infinity);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Zombie"))
            {
                return true;
            }
        }
        return false;
    }
}
