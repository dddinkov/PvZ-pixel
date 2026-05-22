using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IDragHandler,  IPointerUpHandler, IPointerClickHandler, IPointerDownHandler
{
    public GameObject plantObject;
    public Sprite plantSprite;
    public float sunCost = 100.0f;
    public float cooldown = 8.0f;
    private float time = 8.0f;
    private Vector2 pos;
    private bool canDrag, startDragging;
    private GameObject draggingObject;
    private Slider slider;
    private Image cardImage;
    private SoundManager whooshSoundManager;
    private SoundManager errorSoundManager;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        cardImage = GetComponentInParent<Image>();

        pos = transform.position;
        startDragging = false;
        canDrag = false;

        draggingObject = new GameObject("Dummy");
        draggingObject.SetActive(false);
        draggingObject.AddComponent<SpriteRenderer>();
        SpriteRenderer draggingObjectSpriteRenderer = draggingObject.GetComponent<SpriteRenderer>();
        draggingObjectSpriteRenderer.sprite = plantSprite;
        draggingObject.transform.localScale = plantObject.transform.localScale;
        draggingObjectSpriteRenderer.color = Color.gray;
        draggingObjectSpriteRenderer.sortingLayerName = "Plant";

        whooshSoundManager = GameObject.Find("WhooshSoundManager").GetComponent<SoundManager>();
        errorSoundManager = GameObject.Find("ErrorSoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time < cooldown)
        {
            time += Time.deltaTime;
        }

        if (slider.value != 0.99f)
        {
            UpdateSlider();
        }
    }
    public void OnPointerClick(PointerEventData data)
    {
        if (time < cooldown || !SunManager.CanTakeSun(sunCost))
        {
            canDrag = false;
            BlinkRed();
            return;
        }
        whooshSoundManager.PlaySound();
        startDragging = true;
        canDrag = true;
    }

    public void OnPointerDown(PointerEventData data)
    {
        if(time < cooldown && !canDrag)
        {
            startDragging = false;
            BlinkRed();
            return;
        }
        if (!SunManager.CanTakeSun(sunCost))
        {
            startDragging = false;
            BlinkRed();
            return;
        }
        canDrag = false;
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        draggingObject.transform.position = pos;
        draggingObject.SetActive(true);
        slider.value = 0.99f;
    }

    public void OnDrag(PointerEventData data)
    {
        if (time < cooldown && !startDragging)
        {
            return;
        }
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        draggingObject.transform.position = pos;
    }

    public void OnPointerUp(PointerEventData data)
    {
        slider.value = 0.0f;
        if (time < cooldown && !startDragging)
        {
            return;
        }
        draggingObject.SetActive(false);
        startDragging = false;
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] raycastHits = Physics2D.RaycastAll(pos, Vector3.back);
        if(raycastHits.Length < 0)
        {
            errorSoundManager.PlaySound();
            return;
        }

        Tile tileScript = null;
        foreach(RaycastHit2D hit in raycastHits)
        {
            if(hit.collider.gameObject.CompareTag("Tile"))
            {
                tileScript = hit.collider.gameObject.GetComponent<Tile>();
                break;
            }    
        }

        if(tileScript == null)
        {
            return;
        }

        Vector3 tilePos = tileScript.PlacePlant(plantObject, sunCost);

        if (tilePos == Vector3.zero)
        {
            return;
        }

        Instantiate(plantObject, tilePos, Quaternion.identity);

        time = 0.0f;
    }

    void UpdateSlider()
    {
        if(time >= cooldown)
        {
            slider.value = 0;
        }
        else
        {
            slider.value = Mathf.Cos(time * Mathf.PI / cooldown / 2);
            if(slider.value == 0.99f)
            {
                slider.value = 1.0f;
            }
        }
    }
    public void BlinkRed()
    {
        errorSoundManager.PlaySound();
        StartCoroutine(Blink(new Color(0.5f,0.1f,0.1f), 5, 1));
    }

    public IEnumerator ColorLerpTo(Color _color, float _duration)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < _duration)
        {
            cardImage.color = Color.Lerp(cardImage.color, _color, (elapsedTime / _duration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    public IEnumerator Blink(Color _blinkIn, int _blinkCount, float _totalBlinkDuration)
    {
        // We divide the whole duration for the ammount of blinks we will perform
        float fractionalBlinkDuration = _totalBlinkDuration / _blinkCount;

        for (int blinked = 0; blinked < _blinkCount; blinked++)
        {
            // Each blink needs 2 lerps: we give each lerp half of the duration allocated for 1 blink
            float halfFractionalDuration = fractionalBlinkDuration * 0.5f;

            // Lerp to the color
            yield return StartCoroutine(ColorLerpTo(_blinkIn, halfFractionalDuration));

            // Lerp to transparent
            StartCoroutine(ColorLerpTo(Color.white, halfFractionalDuration));
        }
    }
}
