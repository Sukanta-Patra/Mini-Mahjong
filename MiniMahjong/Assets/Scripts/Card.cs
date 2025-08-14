using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private int cardId;

    public void SetCardId(int id)
    {
        cardId = id;
    }   

    public int GetCardId()
    {
        return cardId;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Card clicked with ID: " + cardId);
    }
}
