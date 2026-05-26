using System;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;

    public event Action<GameObject> OnZombieSpawned;

    public GameObject Spawn(GameObject prefab)
    {
        int spawnIndex = UnityEngine.Random.Range(0, spawnPoints.Length);

        GameObject zombie = Instantiate(
            prefab,
            spawnPoints[spawnIndex].position,
            Quaternion.identity
        );

        OnZombieSpawned?.Invoke(zombie);

        return zombie;
    }
}