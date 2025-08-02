using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    public Transform handPanel;            // Kartlar�n g�sterilece�i UI paneli
    public GameObject cardPrefab;          // Sadece Image i�eren prefab
    private List<Card> cardsInHand = new List<Card>();

    public void AddCard(Card card)
    {
        cardsInHand.Add(card);
        DisplayHand(); // Her kart ekledi�inde UI'ya da yans�t
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
        // �nce eski kart objelerini temizle
        foreach (Transform child in handPanel)
            Destroy(child.gameObject);

        // Eldeki kartlar� Instantiate et ve g�rsellerini ayarla
        foreach (Card card in cardsInHand)
        {
            GameObject cardObj = Instantiate(cardPrefab, handPanel);
            SelectableCard selectable = cardObj.GetComponent<SelectableCard>();
            selectable.SetCard(card);
        }
    }

}
