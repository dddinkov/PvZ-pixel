using System.Collections.Generic;
using UnityEngine;

public class ZombieTracker : MonoBehaviour
{
    private readonly List<GameObject> aliveZombies = new();

    public void Register(GameObject zombie)
    {
        aliveZombies.Add(zombie);
    }

    public bool HasAliveZombies()
    {
        aliveZombies.RemoveAll(z => z == null);

        return aliveZombies.Count > 0;
    }

    public Transform GetRandomZombie()
    {
        if(!HasAliveZombies())
        {
            return null;
        }

        int index = Random.Range(0, aliveZombies.Count);

        return aliveZombies[index].transform;
    }

    public int AliveCount
    {
        get
        {
            aliveZombies.RemoveAll(z => z == null);
            return aliveZombies.Count;
        }
    }
}