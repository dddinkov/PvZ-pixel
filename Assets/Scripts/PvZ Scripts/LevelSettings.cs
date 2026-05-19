using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Game/Level Settings")]
public class LevelSettings : ScriptableObject
{
    public GameObject[] zombiePrefabs;

    [Tooltip("How many zombies each wave has")]
    public int[] waves;

    public float spawnRate = 7f;

    public GameObject rewardPrefab;
}