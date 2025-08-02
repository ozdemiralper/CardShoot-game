using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro için

public class CardManager : MonoBehaviour
{
    public Transform handPanel;            // Oyuncunun elini gösterecek panel
    public GameObject cardPrefab;          // Sadece Image içeren prefab
    public PlayerHand playerHand;          // Oyuncunun elini yöneten script

    public HandCardCountDisplay handCardCountDisplay; // Kart sayýlarýný gösteren UI scripti

    void Start()
    {
        playerHand.ClearHand();
        GiveRandomCardsToPlayer(10); // Oyuncuya 10 kart veriyoruz
        UpdateCardCountUI();
    }

    void GiveRandomCardsToPlayer(int numberOfCards)
    {
        Dictionary<string, int> cardNameCounts = new Dictionary<string, int>();
        Dictionary<int, int> typeCounts = new Dictionary<int, int>(); // cardType'a göre sayým (0 = Defans, 1 = Orta Saha, vs.)

        int givenCards = 0;
        int maxSameCard = 3; // Ayný karttan en fazla 3 tane olabilir, sýnýrsýz istersen bunu kaldýr
        int maxTypeCount = 5; // Ayný mevki türünden (örneðin defans) en fazla 5 kart

        while (givenCards < numberOfCards)
        {
            int randIndex = Random.Range(0, CardDatabase.cardList.Count);
            Card card = CardDatabase.cardList[randIndex];

            // Ayný karttan 3 sýnýrý kontrolü
            if (cardNameCounts.ContainsKey(card.cardName) && cardNameCounts[card.cardName] >= maxSameCard)
                continue;

            // Ayný cardType’tan (örneðin defans) maksimum 5 kart sýnýrý kontrolü
            if (typeCounts.ContainsKey(card.position) && typeCounts[card.position] >= maxTypeCount)
                continue;

            // Kartý ver
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

            if (!typeCounts.ContainsKey(card.position))
                typeCounts[card.position] = 0;
            typeCounts[card.position]++;

            givenCards++;
        }
    }



    void UpdateCardCountUI()
    {
        if (handCardCountDisplay != null)
        {
            handCardCountDisplay.SetPlayerCardCount(playerHand.GetCardCount());
            handCardCountDisplay.SetOpponentCardCount(10); // Örnek rakip kart sayýsý, deðiþtirebilirsin
        }
    }
}
