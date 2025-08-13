using System.Collections.Generic; // For using lists / List kullanýmý için
using UnityEngine;                // Unity core namespace / Unity temel isim alaný
using TMPro;                     // For TextMeshPro UI / TextMeshPro arayüzü için

public class PlayerHand : MonoBehaviour
{
    [Header("Card Prefabs")]      // Prefabs section in inspector / Inspector'da prefablar bölümü
    public GameObject playerCardPrefab;   // Prefab for player cards / Oyuncu kartlarý için prefab
    public GameObject captainCardPrefab;  // Prefab for captain cards / Kaptan kartlarý için prefab
    public GameObject weatherCardPrefab;  // Prefab for weather cards / Hava durumu kartlarý için prefab

    public Transform handPanel;      // Panel where cards will be displayed / Kartlarýn gösterileceði panel
    private List<Card> cardsInHand = new List<Card>(); // List of cards in player's hand / Oyuncunun elindeki kart listesi

    public TMP_Text cardCountText;   // Reference to TextMeshPro text component / TextMeshPro UI referansý

    void Start()
    {
        ClearHand();                // Clear existing cards / Mevcut kartlarý temizle

        if (GameManager.Instance != null && GameManager.Instance.selectedGameDeck.Count > 0) // Check if GameManager and selected cards exist / GameManager ve seçili kartlar varsa kontrol et
        {
            List<Card> sortedCards = new List<Card>(GameManager.Instance.selectedGameDeck); // Copy selected cards list / Seçilen kart listesinin kopyasýný al
            sortedCards.Sort((a, b) => b.cardID.CompareTo(a.cardID));                      // Sort cards by cardID descending / Kartlarý cardID'ye göre büyükten küçüðe sýrala

            foreach (var card in sortedCards)  // Add sorted cards to hand / Sýralý kartlarý ele ekle
            {
                AddCard(card);                // Add a card to hand list and display / Kartý el listesine ekle ve göster
            }
        }
        else
        {
            Debug.LogWarning("Seçilen kart listesi boþ veya GameManager bulunamadý."); // Warning if no cards or manager / Kartlar ya da manager yoksa uyarý ver
            // Optional: Add random cards if none selected / Ýstersen buraya rastgele kart ekleme ekleyebilirsin
        }
    }

    public void AddCard(Card card)
    {
        cardsInHand.Add(card);   // Add card to hand list / Kartý el listesine ekle
        DisplayHand();           // Update UI display / UI görüntüsünü güncelle
        UpdateCardCountText();   // Update card count text / Kart sayýsý metnini güncelle
    }

    public void ClearHand()
    {
        cardsInHand.Clear();     // Clear card list / Kart listesini temizle

        foreach (Transform child in handPanel)  // Destroy all card objects in UI / UI'daki tüm kart objelerini yok et
            Destroy(child.gameObject);

        UpdateCardCountText();   // Update card count text / Kart sayýsý metnini güncelle
    }

    public int GetCardCount()
    {
        return cardsInHand.Count;  // Return current number of cards / Elindeki kart sayýsýný döndür
    }

    public void DisplayHand()
    {
        foreach (Transform child in handPanel)  // Clear existing card UI / Mevcut kart UI'larýný temizle
            Destroy(child.gameObject);

        foreach (Card card in cardsInHand)      // Create UI objects for each card / Her kart için UI objesi oluþtur
        {
            GameObject selectedPrefab = GetPrefabForCard(card.cardType);  // Get prefab by card type / Kart tipine göre prefab seç
            GameObject cardObj = Instantiate(selectedPrefab, handPanel); // Instantiate prefab under hand panel / Prefabý handPanel altýna oluþtur

            SelectableCard selectable = cardObj.GetComponent<SelectableCard>(); // Get SelectableCard component / SelectableCard bileþenini al
            if (selectable != null)
            {
                selectable.SetCard(card);  // Assign card data to component / Kart verisini atama yap
            }
        }

        UpdateCardCountText();   // Update card count text / Kart sayýsý metnini güncelle
    }

    private GameObject GetPrefabForCard(CardType type)
    {
        switch (type) // Select prefab based on card type / Kart tipine göre prefab seç
        {
            case CardType.Player: return playerCardPrefab;   // Player card prefab / Oyuncu kartý prefabý
            case CardType.Captain: return captainCardPrefab; // Captain card prefab / Kaptan kartý prefabý
            case CardType.Weather: return weatherCardPrefab; // Weather card prefab / Hava durumu kartý prefabý
            default: return playerCardPrefab;                 // Default prefab / Varsayýlan prefab
        }
    }

    private void UpdateCardCountText()
    {
        if (cardCountText != null)   // Check if text component is assigned / Text bileþeni atanmýþsa
            cardCountText.text = $"{cardsInHand.Count}";  // Update with current card count / Güncel kart sayýsýný göster
    }

    public void RemoveCard(Card card)
    {
        if (cardsInHand.Contains(card)) // Check if card exists in hand / Kart elde varsa
        {
            cardsInHand.Remove(card);    // Remove card from list / Kartý listeden çýkar
            DisplayHand();               // Update UI / UI'yý güncelle
            UpdateCardCountText();       // Update card count text / Kart sayýsý metnini güncelle
        }
    }
}
