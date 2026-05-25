using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sun : MonoBehaviour, IPointerClickHandler
{
    public float fallingSpeed = 10.0f;
    public int sunAmount = 25;
    public float maxFallDistance = 1000.0f;
    public float fallOffset = 200.0f;
    public float timeToLive = 6.0f;
    private bool dying;
    private float fallDistance;
    private float distanceTravelled;
    private float posY;
    private Rigidbody2D rb;
    private Transform destination;
    private Collider2D coll;
    public float collectSpeed = 200.0f;
    [SerializeField]
    private SoundManager ufoSoundManager;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fallDistance = fallOffset + Random.Range(0.0f, 1.0f) * maxFallDistance;
        destination = GameObject.Find("Sun Amount Text").GetComponent<Transform>();
        coll = GetComponent<Collider2D>();
        ufoSoundManager = GameObject.Find("UfoSoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!coll.isActiveAndEnabled)
        {
            rb.velocity = (destination.position - transform.position).normalized * collectSpeed * Time.deltaTime;
            return;
        }
        if (!dying)
        {
            posY = transform.position.y;
            rb.velocity = Vector3.down * fallingSpeed * Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    void Update()
    {
        if(!coll.isActiveAndEnabled)
        {
            if ((transform.position.y >= destination.position.y - 0.1f && (transform.position.x <= destination.position.x + 0.1f)))
            {
                SunManager.Instance.Add(sunAmount);
                Destroy(gameObject);
            }
            return;
        }
        if(dying)
        {
            timeToLive -= Time.deltaTime;
            if(timeToLive <= 0.0f)
            {
                Destroy(gameObject);
            }
            return;
        }

        if (distanceTravelled > fallDistance && !dying)
        {
            dying = true;
        }
        else
        {
            float distance = Mathf.Abs(posY - transform.position.y);
            distanceTravelled += fallingSpeed * Time.deltaTime;
        }
    }

    public void OnPointerClick(PointerEventData data)
    {
        coll.enabled = false;
        ufoSoundManager.PlaySound();
    }

}
