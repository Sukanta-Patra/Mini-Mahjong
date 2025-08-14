using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private List<Card> cards;
    [SerializeField] private int maxCardPairs;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform cardContainer;

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

            //Spawn Card
            Vector2 randomPos = new Vector2(Random.Range(-500f, 500f), Random.Range(-500f, 500f));
            SpawnCard(cardIds[i], randomPos);
        }
    }

    private void SpawnCard(int index, Vector2 pos)
    { 
            GameObject cardObject = Instantiate(cardPrefab, cardContainer);
            cardObject.transform.localPosition = pos;
            Card card = cardObject.GetComponent<Card>();
            card.SetCardId(index);
            cards.Add(card);
    }

}
