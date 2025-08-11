using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshPro için

public class PlayerHand : MonoBehaviour
{
    [Header("Card Prefabs")]
    public GameObject playerCardPrefab;
    public GameObject captainCardPrefab;
    public GameObject weatherCardPrefab;
    public GameObject coachCardPrefab;

    public Transform handPanel;
    private List<Card> cardsInHand = new List<Card>();

    // --- Yeni ekleme ---
    public TMP_Text cardCountText;  // Inspector'dan baðlayacaðýn TMP_Text

    public void AddCard(Card card)
    {
        cardsInHand.Add(card);
        DisplayHand();
        UpdateCardCountText();    // Sayýyý güncelle
    }

    public void ClearHand()
    {
        cardsInHand.Clear();

        foreach (Transform child in handPanel)
            Destroy(child.gameObject);

        UpdateCardCountText();    // Sayýyý güncelle
    }

    public int GetCardCount()
    {
        return cardsInHand.Count;
    }

    public void DisplayHand()
    {
        foreach (Transform child in handPanel)
            Destroy(child.gameObject);

        foreach (Card card in cardsInHand)
        {
            GameObject selectedPrefab = GetPrefabForCard(card.cardType);
            GameObject cardObj = Instantiate(selectedPrefab, handPanel);

            SelectableCard selectable = cardObj.GetComponent<SelectableCard>();
            if (selectable != null)
            {
                selectable.SetCard(card);
            }
        }

        UpdateCardCountText();    // Sayýyý güncelle
    }

    private GameObject GetPrefabForCard(CardType type)
    {
        switch (type)
        {
            case CardType.Player: return playerCardPrefab;
            case CardType.Captain: return captainCardPrefab;
            case CardType.Weather: return weatherCardPrefab;
            case CardType.Coach: return coachCardPrefab;
            default: return playerCardPrefab;
        }
    }

    // Kart sayýsý UI güncelleme fonksiyonu
    private void UpdateCardCountText()
    {
        if (cardCountText != null)
            cardCountText.text = $"{cardsInHand.Count}";
    }
    public void RemoveCard(Card card)
    {
        if (cardsInHand.Contains(card))
        {
            cardsInHand.Remove(card);
            DisplayHand();
            UpdateCardCountText();
        }
    }

}
