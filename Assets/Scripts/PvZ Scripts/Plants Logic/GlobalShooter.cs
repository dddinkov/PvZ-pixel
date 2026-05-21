using UnityEngine;

public class GlobalShooter : Shooter
{
    protected override bool CanShoot()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        return zombies.Length > 0;
    }
}