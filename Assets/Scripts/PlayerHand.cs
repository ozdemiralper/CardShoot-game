using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [Header("Card Prefabs")]
    public GameObject playerCardPrefab;
    public GameObject cupCardPrefab;
    public GameObject weatherCardPrefab;
    public GameObject coachCardPrefab;
    public GameObject trophyCardPrefab;
    public GameObject extraCardPrefab;


    public Transform handPanel;         // Sadece Image içeren prefab
    private List<Card> cardsInHand = new List<Card>();

    public void AddCard(Card card)
    {
        cardsInHand.Add(card);
        DisplayHand(); // Her kart eklediðinde UI'ya da yansýt
    }


    public void RemoveCard(Card card)
    {
        if (cardsInHand.Contains(card))
            cardsInHand.Remove(card);
    }

    public void ClearHand()
    {
        cardsInHand.Clear();

        foreach (Transform child in handPanel)
            Destroy(child.gameObject);
    }

    public int GetCardCount()
    {
        return cardsInHand.Count;
    }
  
    public void DisplayHand()
    {
        // Önce eski kart objelerini temizle
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
    }

    private GameObject GetPrefabForCard(CardType type)
    {
        switch (type)
        {
            case CardType.Player:
                return playerCardPrefab;
            case CardType.Cup:
                return cupCardPrefab;
            case CardType.Weather:
                return weatherCardPrefab;
            case CardType.Coach:
                return coachCardPrefab;
            case CardType.Trophy:
                return trophyCardPrefab;
            case CardType.Extra:
                return extraCardPrefab;
            default:
                return playerCardPrefab; // default fallback
        }
    }


}
