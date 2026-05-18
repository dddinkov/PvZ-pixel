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
    [SerializeField]
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
        time += Time.deltaTime;

        if (TryDropReward())
        {
            EndWaves();
        }

        if (i < waves.Length)
        {
            UpdateRewardPosition();
            HandleWaves();
        }
        else
        {
            // Waves ended, now what ?
        }
    }

    void HandleWaves()
    {
        if (time > spawnRate && waves[i] > 0)
        {
            aliveZombies.Add(InstantiateRandomZombie());

            time = 0;
            waves[i]--;
        }
        else if (waves[i] <= 0)
        {
            if (!HasAliveZombies())
            {
                int k = aliveZombies.Count;
                aliveZombies.Clear();
                aliveZombies = new List<GameObject>();
                for (int j = 0; j < k; ++j)
                {
                    aliveZombies.Add(InstantiateRandomZombie());
                }
                spawnRate /= 2;
                i++;
                //Debug.Log("wave index=" + i);
            }
        }
    }

    private bool TryDropReward()
    {
        if (i == waves.Length && !HasAliveZombies())
        {
            //Debug.Log("Dropping reward");
            reward.transform.position = rewardPosition;
            reward.SetActive(true);
            return true;
        }
        return false;
    }

    private GameObject InstantiateRandomZombie()
    {
        int zombie = Random.Range(0, zombies.Length);
        int spawnPoint = Random.Range(0, spawnPoints.Length);

        return Instantiate(zombies[zombie], spawnPoints[spawnPoint]);
    }

    private void EndWaves()
    {
        i = waves.Length + 1;
    }

    private bool HasAliveZombies()
    {
        foreach (GameObject zombie in aliveZombies)
        {
            if (zombie != null)
            {
                return true;
            }
        }
        return false;
    }

    private void UpdateRewardPosition()
    {
        if(IsLastWave())
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
    }

    private bool IsLastWave()
    {
        return i == waves.Length - 1;
    }
}
