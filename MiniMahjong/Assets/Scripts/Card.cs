using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Sprite plainCardImage;
    private int cardId;
    private Sprite cardImage;
    private bool isShowingCard = false;

    public void SetCardId(int id)
    {
        cardId = id;
    }

    public int GetCardId()
    {
        return cardId;
    }
    public void SetCardImage(Sprite image)
    {
        cardImage = image;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isShowingCard) return; // Prevent multiple clicks while showing the card
        Debug.Log("Card clicked with ID: " + cardId);
        StartCoroutine(ShowCard());
    }

    private IEnumerator ShowCard()
    {
        isShowingCard = true;
        LeanTween.scaleX(gameObject, 0f, 0.15f);

        yield return new WaitForSeconds(0.15f);
        // Show the card image
        gameObject.GetComponent<Image>().sprite = cardImage;

        LeanTween.scaleX(gameObject, 1f, 0.15f);
        yield return new WaitForSeconds(1f); // Wait and show card for a while

        LeanTween.scaleX(gameObject, 0f, 0.15f);

        yield return new WaitForSeconds(0.15f);
        gameObject.GetComponent<Image>().sprite = plainCardImage;

        LeanTween.scaleX(gameObject, 1f, 0.15f);

        yield return new WaitForSeconds(0.15f);
        isShowingCard = false;
    }
}
