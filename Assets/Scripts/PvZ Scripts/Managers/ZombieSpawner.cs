using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private Slider waveProgressSlider;
    [SerializeField]
    private Image flagImage;
    [SerializeField]
    private Sprite flagRaisedSprite;
    private List<Image> flagImages = new();
    private int zombiesLeftToSpawn;
    private int totalZombies;
    [SerializeField]
    private float waveProgressSlidingSpeed = 0.2f;
    private LevelManager levelManager;

    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        CalculateTotalZombies();
        zombiesLeftToSpawn = totalZombies;
        waveProgressSlider.maxValue = totalZombies-1;
        waveProgressSlider.value = 0;
        LayoutWaveFlags();
    }


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

        UpdateUI();
    }

    // This method updates the UI elements related to the wave progress.
    void UpdateUI()
    {
        if (waveProgressSlider != null)
        {
            if(waveProgressSlider.value < totalZombies - zombiesLeftToSpawn)
            {
                waveProgressSlider.value += waveProgressSlidingSpeed * Time.deltaTime;
            }
            else
            {
                waveProgressSlider.value = totalZombies - zombiesLeftToSpawn;
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
                StartCoroutine(levelManager.ShowTextWithSound("A huge wave of zombies is approaching!", 3.0f));
                time-= 5f;
                flagImages[currentWave].sprite = flagRaisedSprite;
                HandleWaveBurst();
            }
        }
    }

    void HandleWaveBurst()
    {
        int k = levelSettings.waves[currentWave];
        for (int j = 0; j < k; ++j)
        {
            aliveZombies.Add(InstantiateRandomZombie());
        }
        currentWave++;
        time -= intervalAfterWaveBurst;
    }

    private GameObject InstantiateRandomZombie()
    {
        int zombieIndex =
            Random.Range(0, levelSettings.zombiePrefabs.Length);

        int spawnIndex =
            Random.Range(0, spawnPoints.Length);

        zombiesLeftToSpawn--;

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

    public void CalculateTotalZombies()
    {
        totalZombies = 0;
        foreach(int k in waves)
        {
            totalZombies += 2 * k;
        }
    }

    private void LayoutWaveFlags()
    {
        if (flagImage == null || waves == null || waves.Length == 0 || waveProgressSlider == null)
        {
            return;
        }

        RectTransform sliderRectTransform = waveProgressSlider.GetComponent<RectTransform>();

        float sliderWidth = sliderRectTransform.rect.width;
        float flagYPosition = flagImage.rectTransform.localPosition.y;


        for(int i = 0; i < waves.Length; ++i)
        {
            // Normalized position from 0 to 1
            float normalizedX = (float)(i + 1) / waves.Length;

            // Convert to local position in the slider area
            float xPos = normalizedX * sliderWidth - sliderWidth / 2f - flagImage.rectTransform.rect.width / 3f; // Centering the flags on the slider

            // Instantiate and set position
            flagImages.Add(Instantiate(flagImage, new Vector3(xPos, flagYPosition, 0f), flagImage.transform.rotation));
            flagImages[i].transform.SetParent(flagImage.transform.parent, false);
            flagImages[i].gameObject.SetActive(true);
            flagImages[i].transform.localPosition = new Vector3(xPos, flagYPosition, 0f);
        }
    }
}