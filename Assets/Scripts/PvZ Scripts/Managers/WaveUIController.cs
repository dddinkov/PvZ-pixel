using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUIController : MonoBehaviour
{
    [SerializeField]
    private WaveManager waveManager;
    [SerializeField]
    private Slider progressSlider;
    [SerializeField]
    private Image flagImage;
    [SerializeField]
    private Sprite raisedFlagSprite;
    [SerializeField]
    private float slidingSpeed = 3f;

    private readonly List<Image> flags = new();

    private void Start()
    {
        InitializeSlider();
        CreateFlags();
        waveManager.OnWaveChanged += UpdateWaveFlags;
    }

    private void Update()
    {
        UpdateSlider();
    }

    private void InitializeSlider()
    {
        progressSlider.maxValue = waveManager.TotalZombies;
        progressSlider.value = 0;
    }

    private void UpdateSlider()
    {
        float target = waveManager.ZombiesSpawned;

        progressSlider.value = Mathf.MoveTowards(
            progressSlider.value,
            target,
            slidingSpeed * Time.deltaTime
        );
    }

    private void UpdateWaveFlags(int wave)
    {
        if(wave > flags.Count)
        {
            return;
        }
        
        flags[wave].sprite = raisedFlagSprite;
    }

    private void CreateFlags()
    {
        int waveCount = waveManager.GetWaveCount();

        RectTransform sliderRect = progressSlider.GetComponent<RectTransform>();

        float sliderWidth = sliderRect.rect.width;

        RectTransform flagRectTransform = flagImage.rectTransform;

        float flagYPosition = flagRectTransform.localPosition.y;

        for(int i = 0; i < waveCount; i++)
        {
            float normalizedX = (float) (i+1) / waveCount;

            float xPos = normalizedX * sliderWidth - sliderWidth / 2f - flagRectTransform.rect.width /3f;

            Vector3 pos = new Vector3(xPos, flagYPosition, 0f);

            flags.Add(Instantiate(
                flagImage,
                pos,
                flagImage.transform.rotation
            ));

            flags[i].transform.SetParent(flagImage.transform.parent, false);
            flags[i].gameObject.SetActive(true);
            flags[i].transform.localPosition = pos;
        }
    }
}