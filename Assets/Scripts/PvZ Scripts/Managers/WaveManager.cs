using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public Action<int> OnWaveChanged;

    private LevelSettings levelSettings;

    [SerializeField]
    private ZombieSpawner spawner;
    [SerializeField]
    private ZombieTracker tracker;

    private int currentWave;
    private int[] remainingWaveSpawns;

    private float timer;

    public int TotalZombies {get; private set;}
    public int ZombiesSpawned {get; private set;}

    private void Start()
    {
        remainingWaveSpawns = (int[]) levelSettings.waves.Clone();

        CalculateTotalZombies();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        HandleWaves();
    }

    private void HandleWaves()
    {
        if(currentWave >= remainingWaveSpawns.Length)
        {
            return;
        }

        if(ShouldSpawnZombie())
        {
            SpawnZombie();

            remainingWaveSpawns[currentWave]--;

            timer = 0f;
        }
        else if(WaveFinished())
        {
            if(!HasAliveZombies())
            {
                SpawnWaveBurst();

                currentWave++;
                OnWaveChanged?.Invoke(currentWave);
            }
        }
    }

    private bool ShouldSpawnZombie()
    {
        return timer >= levelSettings.spawnRate && remainingWaveSpawns[currentWave] > 0;
    }

    private bool WaveFinished()
    {
        return remainingWaveSpawns[currentWave] <= 0 && !tracker.HasAliveZombies();
    }

    private void SpawnZombie()
    {
        int zombieIndex = UnityEngine.Random.Range(0, levelSettings.zombiePrefabs.Length);

        GameObject zombie = spawner.Spawn(levelSettings.zombiePrefabs[zombieIndex]);

        tracker.Register(zombie);

        ZombiesSpawned++;
    }

    private void SpawnWaveBurst()
    {
        int count = levelSettings.waves[currentWave];

        for(int i = 0; i < count; i++)
        {
            SpawnZombie();
        }
    }

    private bool HasAliveZombies()
    {
        return tracker.HasAliveZombies();
    }

    private void CalculateTotalZombies()
    {
        TotalZombies = 0;

        foreach(int wave in levelSettings.waves)
        {
            TotalZombies += wave * 2;
        }
    }

    public int GetWaveCount()
    {
        return levelSettings.waves.Length;
    }

    public void InitializeLevelSettings(LevelSettings settings)
    {
        levelSettings= settings;
    }
}