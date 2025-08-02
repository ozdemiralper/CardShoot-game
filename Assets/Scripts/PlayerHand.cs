using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public Transform handPanel;            // Kartlarýn gösterileceði UI paneli
    public GameObject cardPrefab;          // Sadece Image içeren prefab
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

        // Eldeki kartlarý Instantiate et ve görsellerini ayarla
        foreach (Card card in cardsInHand)
        {
            GameObject cardObj = Instantiate(cardPrefab, handPanel);
            SelectableCard selectable = cardObj.GetComponent<SelectableCard>();
            selectable.SetCard(card);
        }
    }

}
