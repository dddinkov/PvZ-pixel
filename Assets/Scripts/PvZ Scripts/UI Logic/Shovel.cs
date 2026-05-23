using UnityEngine;
using UnityEngine.EventSystems;

public class Shovel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    private Camera cam;
    private bool dragging;
    [SerializeField]
    private GameObject shovelIcon;
    private SoundManager digSoundManager;
    [SerializeField]
    private ParticleSystem digEffectPrefab;

    void Start()
    {
        cam = Camera.main;
        digSoundManager = GameObject.Find("PlantSoundManager").GetComponent<SoundManager>();
    }

    void Update()
    {
        if (!dragging)
        {
            shovelIcon.transform.position = transform.position;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!dragging) return;

        shovelIcon.transform.position = eventData.pointerCurrentRaycast.worldPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;

        Vector2 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D[] hits = Physics2D.RaycastAll(mouseWorldPos, Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                Debug.Log("Shovel hitting: " + hit.collider.name);

            if (hit.collider.CompareTag("Plant"))
            {
                digSoundManager.PlaySound();
                Instantiate(digEffectPrefab, hit.collider.transform.position, Quaternion.identity);
                Destroy(hit.collider.gameObject);
            }
            return;
            }
        }
    }
}