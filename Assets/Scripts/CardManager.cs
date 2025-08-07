using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro için

public class CardManager : MonoBehaviour
{
    public Transform handPanel;            // Oyuncunun elini gösterecek panel
    public GameObject cardPrefab;          // Sadece Image içeren prefab
    public PlayerHand playerHand;          // Oyuncunun elini yöneten script
    void Start()
    {
        playerHand.ClearHand();
        GiveRandomCardsToPlayer(10); // Oyuncuya 10 kart veriyoruz
    }

    void GiveRandomCardsToPlayer(int numberOfCards)
    {
        Dictionary<string, int> cardNameCounts = new Dictionary<string, int>(); // Ayný isimli kart için sayaç
        int weatherCount = 0;
        int captainCount = 0;

        int givenCards = 0;
        int maxSameCard = 2;      // Ayný karttan maksimum 2
        int maxWeatherCards = 2;  // Maksimum 2 weather
        int maxCaptainCards = 2;  // Maksimum 2 captain

        while (givenCards < numberOfCards)
        {
            int randIndex = Random.Range(0, CardDatabase.cardList.Count);
            Card card = CardDatabase.cardList[randIndex];

            // Ayný isimli karttan fazla verme
            if (cardNameCounts.ContainsKey(card.cardName) && cardNameCounts[card.cardName] >= maxSameCard)
                continue;

            // Weather sýnýrý
            if (card.cardType == CardType.Weather && weatherCount >= maxWeatherCards)
                continue;

            // Captain sýnýrý
            if (card.cardType == CardType.Captain && captainCount >= maxCaptainCards)
                continue;

            // Kartý oluþtur
            GameObject cardObj = Instantiate(cardPrefab, handPanel);
            cardObj.transform.localScale = Vector3.one;

            Sprite cardSprite = Resources.Load<Sprite>(card.imagePath);
            Image cardImage = cardObj.transform.Find("CardSprite").GetComponent<Image>();
            if (cardSprite != null && cardImage != null)
                cardImage.sprite = cardSprite;
            else
                Debug.LogWarning("Kart görseli veya Image componenti bulunamadý.");

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

            // Sayaçlarý güncelle
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
