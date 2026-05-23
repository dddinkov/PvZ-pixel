using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectsToDeactivateAfterCardSelection;
    [SerializeField]
    private GameObject[] objectsToActivateAfterCardSelection;
    [SerializeField]
    private Text centralText;
    [SerializeField]
    private string[] textStrings;
    [SerializeField]
    private float interval;
    private Player player;
    [SerializeField] 
    private LevelSettings[] levels;
    private int levelIndex;
    [SerializeField]
    private ZombieSpawner zombieSpawner;
    [SerializeField]
    private SoundManager cinematicBoomSoundManager;
    private Reward reward;

    void Start()
    {
        levels = Resources.LoadAll<LevelSettings>("Levels").OrderBy(level => level.levelIndex).ToArray();
        player = GameObject.Find("Player").GetComponent<Player>();
        levelIndex = System.Math.Min(player.GetLevel() - 1, levels.Length - 1);
    }

    public void StartLevel()
    {
        LevelSettings selectedLevel = levels[levelIndex];
        zombieSpawner.SetLevel(selectedLevel);
        reward = Instantiate(selectedLevel.rewardPrefab.gameObject, transform).GetComponent<Reward>();
        reward.transform.SetParent(GameObject.Find("Slots & Cards Canvas").transform, false);
        reward.gameObject.SetActive(false);
        
        Debug.Log(reward.transform.position);
        
        StartCoroutine(ShowText());
        foreach(GameObject gameObject in objectsToDeactivateAfterCardSelection)
        {
            gameObject.SetActive(true);
        }
        foreach (GameObject gameObject in objectsToActivateAfterCardSelection)
        {
            gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (reward != null)
        {
            Transform randomAliveZombieTransform = zombieSpawner.GetRandomAliveZombieTransform();
            if (randomAliveZombieTransform != null)            {
                reward.transform.position = randomAliveZombieTransform.position;
            }
        }
        if (zombieSpawner.gameObject.activeSelf && zombieSpawner.AreAllZombiesDead() && reward != null && !reward.gameObject.activeSelf)
        {
            reward.gameObject.SetActive(true);
        }
    }

    IEnumerator ShowText()
    {
        centralText.gameObject.SetActive(true);

        for (int i = 0; i < textStrings.Length; i++)
        {
            if(cinematicBoomSoundManager != null)
            {
                cinematicBoomSoundManager.audioSource.pitch = 3.0f * (textStrings.Length - i) / textStrings.Length;
            }
            cinematicBoomSoundManager.PlaySound();
            centralText.text = textStrings[i];
            yield return new WaitForSeconds(interval);
        }

        centralText.gameObject.SetActive(false);
    }
}
