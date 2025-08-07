using System.Collections.Generic;  // Collections for lists / Listeler için koleksiyonlar
using UnityEngine;                 // Unity engine temel namespace'i / Unity motorunun temel isim alaný

public class PlayerHand : MonoBehaviour
{
    [Header("Card Prefabs")]       // Inspector baþlýðý / Inspector'daki baþlýk

    public GameObject playerCardPrefab;  // Prefab for player cds        / Oyuncu kartlarý için prefab
    public GameObject captainCardPrefab; // Prefab for captain cards     / Kaptan kartlarý için prefab
    public GameObject weatherCardPrefab; // Prefab for weather card      / Hava durumu kartlarý için prefab
    public GameObject coachCardPrefab;   // Prefab for coach cards       / Teknik adam kartlarý için prefab
    public GameObject trophyCardPrefab;  // Prefab for trophy cards      / Kupa kartlarý için prefab
    public GameObject extraCardPrefab;   // Prefab for extra cards       / Ekstra kartlar için prefab

    public Transform handPanel;                         // UI panel where cards in hand are displayed    / Eldeki kartlarýn gösterildiði UI paneli
    private List<Card> cardsInHand = new List<Card>();  // List holding current cards in hand            / Elde bulunan kartlarýn listesi

    public void AddCard(Card card)         // Add a card to the hand / Ele kart ekle
    {
        cardsInHand.Add(card);             // Add to the list        / Listeye ekle
        DisplayHand();                     // Refresh display        / Görüntüyü güncelle
    }

    public void ClearHand()                // Clear all cards from hand / Elde kartlarý temizle
    {
        cardsInHand.Clear();               

        foreach (Transform child in handPanel)
            Destroy(child.gameObject);    // Destroy all card UI objects / Tüm kart UI objelerini yok et
    }

    public int GetCardCount()              // Get current number of cards in hand / Elde kaç kart var döndür
    {
        return cardsInHand.Count;          
    }

    public void DisplayHand()              // Show cards in the hand panel / Kartlarý UI panelinde göster
    {
        foreach (Transform child in handPanel)
            Destroy(child.gameObject);    

        foreach (Card card in cardsInHand)  // For each card in hand / Elde her kart için
        {
            GameObject selectedPrefab = GetPrefabForCard(card.cardType);  
            GameObject cardObj = Instantiate(selectedPrefab, handPanel);  

            SelectableCard selectable = cardObj.GetComponent<SelectableCard>();  // Get selectable component / Selectable bileþenini al
            if (selectable != null)
            {
                selectable.SetCard(card);   
            }
        }
    }

    private GameObject GetPrefabForCard(CardType type)  // Return prefab for a card type / Kart tipi için prefab döndür
    {
        switch (type)
        {
            case CardType.Player: return playerCardPrefab; 
            case CardType.Captain: return captainCardPrefab;      
            case CardType.Weather: return weatherCardPrefab; 
            case CardType.Coach: return coachCardPrefab;   
            case CardType.Trophy: return trophyCardPrefab;  
            case CardType.Extra: return extraCardPrefab;  
            default: return playerCardPrefab;  
        }
    }
}
