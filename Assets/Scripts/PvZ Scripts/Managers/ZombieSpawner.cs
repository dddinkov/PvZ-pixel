using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    private LevelSettings levelSettings;

    [SerializeField]
    private Transform[] spawnPoints;

    private int currentWave;
    private float time;

    private List<GameObject> aliveZombies = new();
    private int[] waves;
    [SerializeField]
    private int intervalAfterWaveBurst;

    void Update()
    {
        if(levelSettings != null)
        {
            time += Time.deltaTime;

            if (currentWave < waves.Length)
            {
                HandleWaves();
            }
        }
    }

    void HandleWaves()
    {
        if (time > levelSettings.spawnRate &&
            waves[currentWave] > 0)
        {
            aliveZombies.Add(InstantiateRandomZombie());

            time = 0;

            waves[currentWave]--;
        }
        else if (waves[currentWave] <= 0)
        {
            if (!HasAliveZombies())
            {
                int k = levelSettings.waves[currentWave];
                for (int j = 0; j < k; ++j)
                {
                    aliveZombies.Add(InstantiateRandomZombie());
                }
                currentWave++;
                time -= intervalAfterWaveBurst;
            }
        }
    }

    private GameObject InstantiateRandomZombie()
    {
        int zombieIndex =
            Random.Range(0, levelSettings.zombiePrefabs.Length);

        int spawnIndex =
            Random.Range(0, spawnPoints.Length);

        return Instantiate(
            levelSettings.zombiePrefabs[zombieIndex],
            spawnPoints[spawnIndex].position,
            Quaternion.identity
        );
    }

    private bool HasAliveZombies()
    {
        aliveZombies.RemoveAll(z => z == null);

        return aliveZombies.Count > 0;
    }

    public void SetLevel(LevelSettings levelSettings)
    {
        this.levelSettings = levelSettings;
        
        currentWave = 0;
        time = 0f;

        aliveZombies.Clear();

        waves = (int[])levelSettings.waves.Clone();
    }

    public Transform GetRandomAliveZombieTransform()
    {
        if (!HasAliveZombies())
        {
            return null;
        }

        int index = Random.Range(0, aliveZombies.Count);
        return aliveZombies[index].transform;
    }

    public bool AreAllZombiesDead()
    {
        return !HasAliveZombies() && currentWave >= waves.Length;
    }
}