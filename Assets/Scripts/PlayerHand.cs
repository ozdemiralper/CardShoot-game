using System.Collections.Generic;  // Collections for lists / Listeler i�in koleksiyonlar
using UnityEngine;                 // Unity engine temel namespace'i / Unity motorunun temel isim alan�

public class PlayerHand : MonoBehaviour
{
    [Header("Card Prefabs")]       // Inspector ba�l��� / Inspector'daki ba�l�k

    public GameObject playerCardPrefab;  // Prefab for player cds        / Oyuncu kartlar� i�in prefab
    public GameObject captainCardPrefab; // Prefab for captain cards     / Kaptan kartlar� i�in prefab
    public GameObject weatherCardPrefab; // Prefab for weather card      / Hava durumu kartlar� i�in prefab
    public GameObject coachCardPrefab;   // Prefab for coach cards       / Teknik adam kartlar� i�in prefab
    public GameObject trophyCardPrefab;  // Prefab for trophy cards      / Kupa kartlar� i�in prefab
    public GameObject extraCardPrefab;   // Prefab for extra cards       / Ekstra kartlar i�in prefab

    public Transform handPanel;                         // UI panel where cards in hand are displayed    / Eldeki kartlar�n g�sterildi�i UI paneli
    private List<Card> cardsInHand = new List<Card>();  // List holding current cards in hand            / Elde bulunan kartlar�n listesi

    public void AddCard(Card card)         // Add a card to the hand / Ele kart ekle
    {
        cardsInHand.Add(card);             // Add to the list        / Listeye ekle
        DisplayHand();                     // Refresh display        / G�r�nt�y� g�ncelle
    }

    public void ClearHand()                // Clear all cards from hand / Elde kartlar� temizle
    {
        cardsInHand.Clear();               

        foreach (Transform child in handPanel)
            Destroy(child.gameObject);    // Destroy all card UI objects / T�m kart UI objelerini yok et
    }

    public int GetCardCount()              // Get current number of cards in hand / Elde ka� kart var d�nd�r
    {
        return cardsInHand.Count;          
    }

    public void DisplayHand()              // Show cards in the hand panel / Kartlar� UI panelinde g�ster
    {
        foreach (Transform child in handPanel)
            Destroy(child.gameObject);    

        foreach (Card card in cardsInHand)  // For each card in hand / Elde her kart i�in
        {
            GameObject selectedPrefab = GetPrefabForCard(card.cardType);  
            GameObject cardObj = Instantiate(selectedPrefab, handPanel);  

            SelectableCard selectable = cardObj.GetComponent<SelectableCard>();  // Get selectable component / Selectable bile�enini al
            if (selectable != null)
            {
                selectable.SetCard(card);   
            }
        }
    }

    private GameObject GetPrefabForCard(CardType type)  // Return prefab for a card type / Kart tipi i�in prefab d�nd�r
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
