using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckCard : MonoBehaviour
{
    private const float epsilon = 0.001f;
    [SerializeField]
    CardManager cardManager;
    [SerializeField]
    private GameObject card;
    [SerializeField]
    private Transform initialTransform;
    private Transform currentTransform;
    private Transform destination;
    [SerializeField]
    private float movingSpeed = 10.0f;
    [SerializeField]
    private const int JOIN_SLOT_BUTTON_INDEX = 0;
    [SerializeField]
    private const int LEAVE_SLOT_BUTTON_INDEX = 1;
    private Button joinSlotButton;
    private Button leaveSlotButton;
    private int slotsIndex = -1;
    // Start is called before the first frame update
    void Start()
    {
        InitializeStartingPositions();
        InitializeSlotButtons();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 vec = destination.position - currentTransform.position;
        if(vec.magnitude < epsilon)
        {
            return;
        }
        vec = vec.normalized;
        currentTransform.position = Vector2.MoveTowards(currentTransform.position, destination.position, movingSpeed * Time.deltaTime);
    }

    public void JoinSlots()
    {
        if(cardManager == null)
        {
            Debug.Log("Card Manager object is null in DeckCard script.");
            return;
        }
        destination = cardManager.AssignSlot(card, out slotsIndex);
        joinSlotButton.gameObject.SetActive(false);
        leaveSlotButton.gameObject.SetActive(true);
    }

    public void ResetDestination()
    {
        cardManager.DisposeSlot(slotsIndex);
        slotsIndex = -1;
        joinSlotButton.gameObject.SetActive(true);
        leaveSlotButton.gameObject.SetActive(false);
        destination = initialTransform;
    }
    private void InitializeStartingPositions()
    {
        currentTransform = gameObject.transform;
        destination = currentTransform;
    }
    private void InitializeSlotButtons()
    {
        joinSlotButton = gameObject.transform.GetChild(0).GetComponent<Button>();
        leaveSlotButton = gameObject.transform.GetChild(1).GetComponent<Button>();
        joinSlotButton.gameObject.SetActive(true);
        leaveSlotButton.gameObject.SetActive(false);
    }
}
