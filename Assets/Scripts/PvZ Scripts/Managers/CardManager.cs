using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    private GameObject deckGameObject;
    [SerializeField]
    private GameObject slotsGameObject;
    [SerializeField]
    private GameObject[] deck;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Transform[] slotsTransforms;
    private GameObject[] slotsCards;
    private int slotIndex = 0;
    void Start()
    {
        InitializeDeck();
        slotsCards = new GameObject[slotsTransforms.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void InitializeDeck()
    {
        const int MAX_NUMBER_OF_PLANTS = Player.MAX_NUMBER_OF_PLANTS;

        for (int i = 0; i < MAX_NUMBER_OF_PLANTS; ++i)
        {
            deck[i].SetActive(player.IsPlantUnlocked(i));
        }
    }
    public int GetSlotIndex()
    {
        return slotIndex;
    }
    private bool IsIndexValidSlotIndex(int index)
    {
        if (0 > index || index > slotsTransforms.Length)
        {
            return false;
        }
        return true;
    }
    private bool IsSlotIndexValid()
    {
        return IsIndexValidSlotIndex(slotIndex);
    }

    public Transform AssignSlot(GameObject card, out int index)
    {
        index = -1;
        for(int i = 0; i < slotsCards.Length; ++i)
        {
            if (slotsCards[i] == null)
            {
                index = i;
                slotIndex = i;
                slotsCards[slotIndex] = card;
                return slotsTransforms[slotIndex++];
            }
        }
        return null;
    }
    public void DisposeSlot(int index)
    {
        if(IsIndexValidSlotIndex(index))
        {
            slotsCards[index] = null;
        }
    }
    public void SaveSlots()
    {
        for(int i = 0; i < slotIndex; ++i)
        {
            GameObject card = Instantiate(slotsCards[i], slotsTransforms[i]);
            card.transform.SetParent(slotsGameObject.transform);
            card.transform.position = slotsTransforms[i].position;
            card.transform.localScale = slotsTransforms[i].localScale;
        }
        Destroy(deckGameObject);
        Destroy(gameObject);
    }
}
