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
    private bool isMatched = false;

    public void SetCardId(int id) => cardId = id;
    public int GetCardId() => cardId;
    public void SetCardImage(Sprite image) => cardImage = image;
    public bool IsMatched() => isMatched;
    public void SetMatched(bool matched) => isMatched = matched;

    public void OnPointerDown(PointerEventData eventData)
    {
        // Prevent multiple clicks while showing the card
        //OR if Game is over, skip card interactions
        if (isShowingCard || isMatched || GameManager.Instance.GetIsGameOver()) return; 

        GameManager.Instance.OnCardSelected(this);

        StartCoroutine(ShowCardCoroutine());
    }

    private IEnumerator ShowCardCoroutine()
    {
        isShowingCard = true;
        LeanTween.scaleX(gameObject, 0f, 0.15f);

        yield return new WaitForSeconds(0.15f);

        // Show the card image
        gameObject.GetComponent<Image>().sprite = cardImage;
        LeanTween.scaleX(gameObject, 1f, 0.15f);
    }

    private IEnumerator CloseCardCoroutine()
    {
        LeanTween.scaleX(gameObject, 0f, 0.15f);

        yield return new WaitForSeconds(0.15f);

        // Reset to plain card image
        gameObject.GetComponent<Image>().sprite = plainCardImage;
        LeanTween.scaleX(gameObject, 1f, 0.15f);

        isShowingCard = false;
    }

    public void CloseCard()
    {
        if (!isShowingCard || isMatched) return; // Prevent closing if the card is not showing
        StartCoroutine(CloseCardCoroutine());
    }

    public void DisableCard()
    {
        isMatched = true; //Safe check to ensure the card is not matched again
        LeanTween.scale(gameObject, Vector3.zero, 0.15f).setOnComplete(() => gameObject.SetActive(false));
    }

    public void ResetCard()
    {
        //Incase if I go overboard and add retry buttons or something
        StopAllCoroutines();
        transform.localScale = Vector3.one;
        isShowingCard = false;
        isMatched = false;
        gameObject.GetComponent<Image>().sprite = plainCardImage;
        gameObject.SetActive(true);
    }
}
