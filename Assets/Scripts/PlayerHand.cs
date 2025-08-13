using System.Collections.Generic; // For using lists / List kullan�m� i�in
using UnityEngine;                // Unity core namespace / Unity temel isim alan�
using TMPro;                     // For TextMeshPro UI / TextMeshPro aray�z� i�in

public class PlayerHand : MonoBehaviour
{
    [Header("Card Prefabs")]      // Prefabs section in inspector / Inspector'da prefablar b�l�m�
    public GameObject playerCardPrefab;   // Prefab for player cards / Oyuncu kartlar� i�in prefab
    public GameObject captainCardPrefab;  // Prefab for captain cards / Kaptan kartlar� i�in prefab
    public GameObject weatherCardPrefab;  // Prefab for weather cards / Hava durumu kartlar� i�in prefab

    public Transform handPanel;      // Panel where cards will be displayed / Kartlar�n g�sterilece�i panel
    private List<Card> cardsInHand = new List<Card>(); // List of cards in player's hand / Oyuncunun elindeki kart listesi

    public TMP_Text cardCountText;   // Reference to TextMeshPro text component / TextMeshPro UI referans�

    void Start()
    {
        ClearHand();                // Clear existing cards / Mevcut kartlar� temizle

        if (GameManager.Instance != null && GameManager.Instance.selectedGameDeck.Count > 0) // Check if GameManager and selected cards exist / GameManager ve se�ili kartlar varsa kontrol et
        {
            List<Card> sortedCards = new List<Card>(GameManager.Instance.selectedGameDeck); // Copy selected cards list / Se�ilen kart listesinin kopyas�n� al
            sortedCards.Sort((a, b) => b.cardID.CompareTo(a.cardID));                      // Sort cards by cardID descending / Kartlar� cardID'ye g�re b�y�kten k����e s�rala

            foreach (var card in sortedCards)  // Add sorted cards to hand / S�ral� kartlar� ele ekle
            {
                AddCard(card);                // Add a card to hand list and display / Kart� el listesine ekle ve g�ster
            }
        }
        else
        {
            Debug.LogWarning("Se�ilen kart listesi bo� veya GameManager bulunamad�."); // Warning if no cards or manager / Kartlar ya da manager yoksa uyar� ver
            // Optional: Add random cards if none selected / �stersen buraya rastgele kart ekleme ekleyebilirsin
        }
    }

    public void AddCard(Card card)
    {
        cardsInHand.Add(card);   // Add card to hand list / Kart� el listesine ekle
        DisplayHand();           // Update UI display / UI g�r�nt�s�n� g�ncelle
        UpdateCardCountText();   // Update card count text / Kart say�s� metnini g�ncelle
    }

    public void ClearHand()
    {
        cardsInHand.Clear();     // Clear card list / Kart listesini temizle

        foreach (Transform child in handPanel)  // Destroy all card objects in UI / UI'daki t�m kart objelerini yok et
            Destroy(child.gameObject);

        UpdateCardCountText();   // Update card count text / Kart say�s� metnini g�ncelle
    }

    public int GetCardCount()
    {
        return cardsInHand.Count;  // Return current number of cards / Elindeki kart say�s�n� d�nd�r
    }

    public void DisplayHand()
    {
        foreach (Transform child in handPanel)  // Clear existing card UI / Mevcut kart UI'lar�n� temizle
            Destroy(child.gameObject);

        foreach (Card card in cardsInHand)      // Create UI objects for each card / Her kart i�in UI objesi olu�tur
        {
            GameObject selectedPrefab = GetPrefabForCard(card.cardType);  // Get prefab by card type / Kart tipine g�re prefab se�
            GameObject cardObj = Instantiate(selectedPrefab, handPanel); // Instantiate prefab under hand panel / Prefab� handPanel alt�na olu�tur

            SelectableCard selectable = cardObj.GetComponent<SelectableCard>(); // Get SelectableCard component / SelectableCard bile�enini al
            if (selectable != null)
            {
                selectable.SetCard(card);  // Assign card data to component / Kart verisini atama yap
            }
        }

        UpdateCardCountText();   // Update card count text / Kart say�s� metnini g�ncelle
    }

    private GameObject GetPrefabForCard(CardType type)
    {
        switch (type) // Select prefab based on card type / Kart tipine g�re prefab se�
        {
            case CardType.Player: return playerCardPrefab;   // Player card prefab / Oyuncu kart� prefab�
            case CardType.Captain: return captainCardPrefab; // Captain card prefab / Kaptan kart� prefab�
            case CardType.Weather: return weatherCardPrefab; // Weather card prefab / Hava durumu kart� prefab�
            default: return playerCardPrefab;                 // Default prefab / Varsay�lan prefab
        }
    }

    private void UpdateCardCountText()
    {
        if (cardCountText != null)   // Check if text component is assigned / Text bile�eni atanm��sa
            cardCountText.text = $"{cardsInHand.Count}";  // Update with current card count / G�ncel kart say�s�n� g�ster
    }

    public void RemoveCard(Card card)
    {
        if (cardsInHand.Contains(card)) // Check if card exists in hand / Kart elde varsa
        {
            cardsInHand.Remove(card);    // Remove card from list / Kart� listeden ��kar
            DisplayHand();               // Update UI / UI'y� g�ncelle
            UpdateCardCountText();       // Update card count text / Kart say�s� metnini g�ncelle
        }
    }
}
