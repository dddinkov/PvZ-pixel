using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject[] zombies;
    [SerializeField]
    Transform[] spawnPoints;
    [SerializeField]
    int[] waves;
    int i;
    float time = 0.0f;
    [SerializeField]
    float spawnRate = 7.0f;
    List<GameObject> aliveZombies;
    [SerializeField]
    private GameObject reward;
    [SerializeField]
    private GameObject mainCanvas;
    private Vector3 rewardPosition;
    void Start()
    {
        i = 0;
        aliveZombies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleWaves();
    }

    void HandleWaves()
    {
        if (i > waves.Length)
        {
            return;
        }
        time += Time.deltaTime;
        if (i == waves.Length)
        {
            DropReward();
            i++;
            return;
        }

        if (time > spawnRate && waves[i] > 0)
        {
            InstantiateRandomZombie();

            time = 0;
            waves[i]--;
        }
        else if (waves[i] <= 0)
        {
            bool startWave = true;
            foreach (GameObject zombie in aliveZombies)
            {
                if (zombie != null)
                {
                    rewardPosition = zombie.transform.position;
                    startWave = false;
                }
            }
            if (startWave)
            {
                int k = aliveZombies.Count;
                aliveZombies.Clear();
                aliveZombies = new List<GameObject>();
                for (int j = 0; j < k; ++j)
                {
                    InstantiateRandomZombie();
                }
                spawnRate /= 2;
                i++;
            }
        }
    }

    void DropReward()
    {
        reward.transform.position = rewardPosition;
        reward.SetActive(true);
    }

    void InstantiateRandomZombie()
    {
        int zombie = Random.Range(0, zombies.Length);
        int spawnPoint = Random.Range(0, spawnPoints.Length);

        aliveZombies.Add(Instantiate(zombies[zombie], spawnPoints[spawnPoint]));
    }
}
