using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    [Header("Gameplay")]
    private int score = 0;
    [SerializeField] private int turns = 10;
    [SerializeField] private int maxCardPairs;
    [SerializeField] private List<Card> cards;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform cardContainer;
    [SerializeField] private float spacing = 10f;
    [SerializeField] private Sprite[] cardImages;
    private List<Card> currentPair = new List<Card>();

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI turnsText;
    [SerializeField] private TextMeshProUGUI endGameText;
    [SerializeField] private GameObject endGamePopupPanel;


    private bool isGameOver = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        InitializeCards();
    }

    private void InitializeCards()
    {
        cards.Clear();
        List<int> cardIds = new List<int>();
        for (int i = 0; i < maxCardPairs; i++)
        {
            cardIds.Add(i);
            cardIds.Add(i);
        }

        // Shuffle the card IDs
        for (int i = 0; i < cardIds.Count; i++)
        {
            int rnd = Random.Range(i, cardIds.Count);
            int temp = cardIds[i];
            cardIds[i] = cardIds[rnd];
            cardIds[rnd] = temp;
        }

        // Grid dimensions
        int totalCards = cardIds.Count;
        int columns = Mathf.CeilToInt(Mathf.Sqrt(totalCards)); // Calculate columns based on the square root of total cards
        int rows = Mathf.CeilToInt((float)totalCards / columns);

        RectTransform cardRect = cardPrefab.GetComponent<RectTransform>();

        float cardWidth = cardRect.rect.width;
        float cardHeight = cardRect.rect.height;

        float totalWidth = columns * cardWidth + (columns - 1) * spacing;
        float totalHeight = rows * cardHeight + (rows - 1) * spacing;

        Vector2 startPos = new Vector2(-totalWidth / 2f + cardWidth / 2f, totalHeight / 2f - cardHeight / 2f);

        for (int row = 0, cardIndex = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++, cardIndex++)
            {
                if (cardIndex >= totalCards) break;

                float x = startPos.x + col * (cardWidth + spacing);
                float y = startPos.y - row * (cardHeight + spacing);
                SpawnCard(cardIds[cardIndex], new Vector2(x, y));
            }
        }
    }

    private void SpawnCard(int index, Vector2 pos)
    {
        GameObject cardObject = Instantiate(cardPrefab, cardContainer);
        cardObject.transform.localPosition = pos;
        Card card = cardObject.GetComponent<Card>();
        card.SetCardId(index);
        card.SetCardImage(cardImages[index]);
        cards.Add(card);
    }

    public void OnCardSelected(Card clickedCard)
    {

        //Reduce chance (if turn based mode)
        turns--;
        turnsText.text = turns.ToString();

        //TODO: Play Tap/Card Flip SFX
        currentPair.Add(clickedCard);

        if (currentPair.Count >= 2)
        {
            // Copy the pair to local variables so further clicks won't interfere
            Card first = currentPair[0];
            Card second = currentPair[1];
            currentPair = new List<Card>();

            StartCoroutine(CheckMatch(first, second));

        }
    }

    private IEnumerator CheckMatch(Card first, Card second)
    {
        yield return new WaitForSeconds(0.5f); // Delay so player sees the second card

        if (first.GetCardId() == second.GetCardId())
        {
            //TODO: Add match SFX

            //Increase score
            score++;
            scoreText.text = score.ToString();

            first.SetMatched(true);
            second.SetMatched(true);

            yield return new WaitForSeconds(0.3f);
            first.DisableCard();
            second.DisableCard();
        }
        else
        {
            //TODO: Add mismatch SFX
            first.CloseCard();
            second.CloseCard();
        }

        //Check Game Over condition
        if (score >= maxCardPairs)
        {
            //Win
            isGameOver = true;
            endGameText.text = "You Win!";
            endGamePopupPanel.GetComponent<CanvasGroup>().alpha = 0f;
            endGamePopupPanel.SetActive(true);
            LeanTween.alphaCanvas(endGamePopupPanel.GetComponent<CanvasGroup>(), 1f, 0.25f);

        }
        else if (turns <= 0)
        {
            //Game Over - Lose
            isGameOver = true;
            endGameText.text = "Game Over!";
            endGamePopupPanel.GetComponent<CanvasGroup>().alpha = 0f;
            endGamePopupPanel.SetActive(true);
            LeanTween.alphaCanvas(endGamePopupPanel.GetComponent<CanvasGroup>(), 1f, 0.25f);
        }
    }

    public bool GetIsGameOver() => isGameOver;

    public void OnGoBackButton()
    { 
        //TODO: Go to main menu
        //Adding restart of same scene for now
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

}
