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
        List<int> usedIndices = new List<int>();

        for (int i = 0; i < numberOfCards; i++)
        {
            int randIndex;
            do
            {
                randIndex = Random.Range(0, CardDatabase.cardList.Count);
            } while (usedIndices.Contains(randIndex));  // Ayn� kart� 2.kez alma

            usedIndices.Add(randIndex);
            Card card = CardDatabase.cardList[randIndex];

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
