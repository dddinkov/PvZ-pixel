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

    private GameObject rewardInstance;
    private Vector3 rewardPosition;
    private int[] waves;
    [SerializeField]
    private int intervalAfterWaveBurst;

    void Update()
    {
        if(levelSettings != null)
        {
            time += Time.deltaTime;

            UpdateRewardPosition();

            if (TryDropReward())
            {
                EndWaves();
            }

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

    private bool TryDropReward()
    {
        if (currentWave >= waves.Length &&
            !HasAliveZombies())
        {
            if (rewardInstance != null)
            {
                rewardInstance.transform.position = rewardPosition;
                rewardInstance.SetActive(true);
            }

            return true;
        }

        return false;
    }

    private bool HasAliveZombies()
    {
        aliveZombies.RemoveAll(z => z == null);

        return aliveZombies.Count > 0;
    }

    private void UpdateRewardPosition()
    {
        foreach (GameObject zombie in aliveZombies)
        {
            if (zombie != null)
            {
                rewardPosition = zombie.transform.position;
                return;
            }
        }
    }

    private void EndWaves()
    {
        currentWave = waves.Length + 1;
    }

    public void SetLevel(LevelSettings levelSettings)
    {
        this.levelSettings = levelSettings;
        
        currentWave = 0;
        time = 0f;

        aliveZombies.Clear();

        waves = (int[])levelSettings.waves.Clone();
    }

    public void InstantiateReward()
    {
        if (levelSettings != null && levelSettings.rewardPrefab != null)
        {
            GameObject mainCanvas = GameObject.Find("Main Canvas");
            rewardInstance = Instantiate(levelSettings.rewardPrefab);

            rewardInstance.transform.SetParent(mainCanvas.transform, false);
            rewardInstance.SetActive(false);
        }
    }
}