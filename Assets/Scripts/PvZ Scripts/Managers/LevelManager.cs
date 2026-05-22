using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        levelIndex = player.GetLevel() - 1;
    }

    public void StartLevel()
    {
        LevelSettings selectedLevel = levels[levelIndex];
        zombieSpawner.SetLevel(selectedLevel);
        zombieSpawner.InstantiateReward();
        
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
