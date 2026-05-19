using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager enemyManager;
    [SerializeField]
    private Transform startPoint;
    [SerializeField]
    public int enemiesToSpawn = 8;
    [SerializeField]
    public int enemiesAlive = 0;
    [SerializeField]
    private float eps = 0.5f;

    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private float time = 0f;
    [SerializeField]
    private Transform[] path;


    void Start()
    {
        enemyManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= 1f / eps && enemiesToSpawn != 0)
        {
            time = 0f;
            SpawnEnemy();
            enemiesToSpawn--;
            enemiesAlive++;
        }
    }

    private void SpawnEnemy()
    {
        Enemy script = Instantiate(enemies[0], startPoint).GetComponent<Enemy>();
        script.path = path;
    }
}
