using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro i�in

public class CardManager : MonoBehaviour
{
    public Transform handPanel;            // Oyuncunun elini g�sterecek panel
    public GameObject cardPrefab;          // Sadece Image i�eren prefab
    public PlayerHand playerHand;          // Oyuncunun elini y�neten script

    public HandCardCountDisplay handCardCountDisplay; // Kart say�lar�n� g�steren UI scripti

    void Start()
    {
        playerHand.ClearHand();
        GiveRandomCardsToPlayer(10); // Oyuncuya 10 kart veriyoruz
        UpdateCardCountUI();
    }

    void GiveRandomCardsToPlayer(int numberOfCards)
    {
        Dictionary<string, int> cardNameCounts = new Dictionary<string, int>();
        Dictionary<int, int> typeCounts = new Dictionary<int, int>(); // cardType'a g�re say�m (0 = Defans, 1 = Orta Saha, vs.)

        int givenCards = 0;
        int maxSameCard = 3; // Ayn� karttan en fazla 3 tane olabilir, s�n�rs�z istersen bunu kald�r
        int maxTypeCount = 5; // Ayn� mevki t�r�nden (�rne�in defans) en fazla 5 kart

        while (givenCards < numberOfCards)
        {
            int randIndex = Random.Range(0, CardDatabase.cardList.Count);
            Card card = CardDatabase.cardList[randIndex];

            // Ayn� karttan 3 s�n�r� kontrol�
            if (cardNameCounts.ContainsKey(card.cardName) && cardNameCounts[card.cardName] >= maxSameCard)
                continue;

            // Ayn� cardType�tan (�rne�in defans) maksimum 5 kart s�n�r� kontrol�
            if (typeCounts.ContainsKey(card.position) && typeCounts[card.position] >= maxTypeCount)
                continue;

            // Kart� ver
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
            handCardCountDisplay.SetOpponentCardCount(10); // �rnek rakip kart say�s�, de�i�tirebilirsin
        }
    }
}
