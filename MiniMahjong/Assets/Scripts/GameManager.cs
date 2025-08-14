using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private List<Card> cards;
    [SerializeField] private int maxCardPairs;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform cardContainer;
    [SerializeField] private float spacing = 10f;
    [SerializeField] private Sprite[] cardImages; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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

        Vector2 startPos = new Vector2( -totalWidth / 2f + cardWidth / 2f, totalHeight / 2f - cardHeight / 2f);

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

}
