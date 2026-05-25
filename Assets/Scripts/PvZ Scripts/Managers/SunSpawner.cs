using UnityEngine;

public class SunSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject sunPrefab;
    [SerializeField]
    private Transform leftPoint, rightPoint, upPoint, downPoint;
    [SerializeField]
    private float spawnCooldown = 5.0f;
    void Start()
    {
        InvokeRepeating(nameof(SpawnSunPrefab),0f,spawnCooldown);
    }

    private void SpawnSunPrefab()
    {
        float x = Random.Range(leftPoint.position.x, rightPoint.position.x);
        float y = Random.Range(downPoint.position.y, upPoint.position.y);
        Vector2 spawnPoint = new Vector2(x, y);
        Instantiate(sunPrefab, spawnPoint, Quaternion.identity);
    }
}