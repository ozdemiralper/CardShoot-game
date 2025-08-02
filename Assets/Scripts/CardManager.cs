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
        List<int> usedIndices = new List<int>();

        for (int i = 0; i < numberOfCards; i++)
        {
            int randIndex;
            do
            {
                randIndex = Random.Range(0, CardDatabase.cardList.Count);
            } while (usedIndices.Contains(randIndex));  // Ayný kartý 2.kez alma

            usedIndices.Add(randIndex);
            Card card = CardDatabase.cardList[randIndex];

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
