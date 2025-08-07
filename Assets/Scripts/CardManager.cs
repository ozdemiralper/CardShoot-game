using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro i�in

public class CardManager : MonoBehaviour
{
    public Transform handPanel;            // Oyuncunun elini g�sterecek panel
    public GameObject cardPrefab;          // Sadece Image i�eren prefab
    public PlayerHand playerHand;          // Oyuncunun elini y�neten script
    void Start()
    {
        playerHand.ClearHand();
        GiveRandomCardsToPlayer(10); // Oyuncuya 10 kart veriyoruz
    }

    void GiveRandomCardsToPlayer(int numberOfCards)
    {
        Dictionary<string, int> cardNameCounts = new Dictionary<string, int>(); // Ayn� isimli kart i�in saya�
        int weatherCount = 0;
        int captainCount = 0;

        int givenCards = 0;
        int maxSameCard = 2;      // Ayn� karttan maksimum 2
        int maxWeatherCards = 2;  // Maksimum 2 weather
        int maxCaptainCards = 2;  // Maksimum 2 captain

        while (givenCards < numberOfCards)
        {
            int randIndex = Random.Range(0, CardDatabase.cardList.Count);
            Card card = CardDatabase.cardList[randIndex];

            // Ayn� isimli karttan fazla verme
            if (cardNameCounts.ContainsKey(card.cardName) && cardNameCounts[card.cardName] >= maxSameCard)
                continue;

            // Weather s�n�r�
            if (card.cardType == CardType.Weather && weatherCount >= maxWeatherCards)
                continue;

            // Captain s�n�r�
            if (card.cardType == CardType.Captain && captainCount >= maxCaptainCards)
                continue;

            // Kart� olu�tur
            GameObject cardObj = Instantiate(cardPrefab, handPanel);
            cardObj.transform.localScale = Vector3.one;

            Sprite cardSprite = Resources.Load<Sprite>(card.imagePath);
            Image cardImage = cardObj.transform.Find("CardSprite").GetComponent<Image>();
            if (cardSprite != null && cardImage != null)
                cardImage.sprite = cardSprite;
            else
                Debug.LogWarning("Kart g�rseli veya Image componenti bulunamad�.");

            CardDisplay display = cardObj.GetComponent<CardDisplay>();
            if (display != null)
                display.SetCard(card);

            SelectableCard selectable = cardObj.GetComponent<SelectableCard>();
            if (selectable != null)
            {
                selectable.card = card;
                selectable.cardDisplay = display;
            }

            playerHand.AddCard(card);

            // Saya�lar� g�ncelle
            if (!cardNameCounts.ContainsKey(card.cardName))
                cardNameCounts[card.cardName] = 0;
            cardNameCounts[card.cardName]++;

            if (card.cardType == CardType.Weather)
                weatherCount++;

            if (card.cardType == CardType.Captain)
                captainCount++;

            givenCards++;
        }
    }
}
