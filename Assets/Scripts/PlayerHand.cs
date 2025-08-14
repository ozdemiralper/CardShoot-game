using System.Collections.Generic; // [en] For using lists      / [tr] List kullanýmý için
using UnityEngine;                // [en] Unity core namespace / [tr] Unity temel isim alaný
using TMPro;                     // [en] For TextMeshPro UI    / [tr] TextMeshPro arayüzü için

public class PlayerHand : MonoBehaviour
{
    [Header("Card Prefabs")]              // [en] Prefabs section in inspector / [tr] Inspector'da prefablar bölümü
    public GameObject playerCardPrefab;   // [en] Prefab for player cards      / [tr] Oyuncu kartlarý için prefab
    public GameObject captainCardPrefab;  // [en] Prefab for captain cards     / [tr] Kaptan kartlarý için prefab
    public GameObject weatherCardPrefab;  // [en] Prefab for weather cards     / [tr] Hava durumu kartlarý için prefab

    public Transform handPanel;           // [en] Panel where cards will be displayed / [tr] Kartlarýn gösterileceði panel
    private List<Card> cardsInHand = new List<Card>(); // [en] List of cards in player's hand / [tr] Oyuncunun elindeki kart listesi

    public TMP_Text cardCountText;   // [en] Reference to TextMeshPro text component / [tr] TextMeshPro UI referansý

    void Start()
    {
        ClearHand();                // [en] Clear existing cards / [tr] Mevcut kartlarý temizle

        if (DeckManager.Instance != null && DeckManager.Instance.selectedGameDeck.Count > 0) // [en] Check if GameManager and selected cards exist / [tr] GameManager ve seçili kartlar varsa kontrol et
        {
            List<Card> sortedCards = new List<Card>(DeckManager.Instance.selectedGameDeck); // [en] Copy selected cards list / [tr] Seçilen kart listesinin kopyasýný al
            sortedCards.Sort((a, b) => b.cardID.CompareTo(a.cardID));                      // [en] Sort cards by cardID descending / [tr] Kartlarý cardID'ye göre büyükten küçüðe sýrala

            foreach (var card in sortedCards)  // [en] Add sorted cards to hand / [tr] Sýralý kartlarý ele ekle
            {
                AddCard(card);                // [en] Add a card to hand list and display / [tr] Kartý el listesine ekle ve göster
            }
        }
        else
        {
            Debug.LogWarning("Seçilen kart listesi boþ veya GameManager bulunamadý."); // [en] Warning if no cards or manager / [tr] Kartlar ya da manager yoksa uyarý ver
            // [en] Optional: Add random cards if none selected / [tr] Ýstersen buraya rastgele kart ekleme ekleyebilirsin
        }
    }

    public void AddCard(Card card)
    {
        cardsInHand.Add(card);   // [en] Add card to hand list / [tr] Kartý el listesine ekle
        DisplayHand();           // [en] Update UI display / [tr] UI görüntüsünü güncelle
        UpdateCardCountText();   // [en] Update card count text / [tr] Kart sayýsý metnini güncelle
    }

    public void ClearHand()
    {
        cardsInHand.Clear();     // [en] Clear card list / [tr] Kart listesini temizle

        foreach (Transform child in handPanel)  // [en] Destroy all card objects in UI / [tr] UI'daki tüm kart objelerini yok et
            Destroy(child.gameObject);

        UpdateCardCountText();   // [en] Update card count text / [tr] Kart sayýsý metnini güncelle
    }

    public int GetCardCount()
    {
        return cardsInHand.Count;  // [en] Return current number of cards / [tr] Elindeki kart sayýsýný döndür
    }

    public void DisplayHand()
    {
        foreach (Transform child in handPanel)  // [en] Clear existing card UI / [tr] Mevcut kart UI'larýný temizle
            Destroy(child.gameObject);

        foreach (Card card in cardsInHand)      // [en] Create UI objects for each card / [tr] Her kart için UI objesi oluþtur
        {
            GameObject selectedPrefab = GetPrefabForCard(card.cardType);  // [en] Get prefab by card type / [tr] Kart tipine göre prefab seç
            GameObject cardObj = Instantiate(selectedPrefab, handPanel); // [en] Instantiate prefab under hand panel / [tr] Prefabý handPanel altýna oluþtur

            SelectableCard selectable = cardObj.GetComponent<SelectableCard>(); // [en] Get SelectableCard component / [tr] SelectableCard bileþenini al
            if (selectable != null)
            {
                selectable.SetCard(card);  // [en] Assign card data to component / [tr] Kart verisini atama yap
            }
        }

        UpdateCardCountText();   // [en] Update card count text / [tr] Kart sayýsý metnini güncelle
    }

    private GameObject GetPrefabForCard(CardType type)
    {
        switch (type) // [en] Select prefab based on card type / [tr] Kart tipine göre prefab seç
        {
            case CardType.Player: return playerCardPrefab;   // [en] Player card prefab  / [tr] Oyuncu kartý prefabý
            case CardType.Captain: return captainCardPrefab; // [en] Captain card prefab / [tr] Kaptan kartý prefab
            case CardType.Weather: return weatherCardPrefab; // [en] Weather card prefab / [tr] Hava durumu kartý prefabý
            default: return playerCardPrefab;                // [en] Default prefab      / [tr] Varsayýlan prefab
        }
    }

    private void UpdateCardCountText()
    {
        if (cardCountText != null)                        // [en] Check if text component is assigned / [tr] Text bileþeni atanmýþsa
            cardCountText.text = $"{cardsInHand.Count}";  // [en] Update with current card count      / [tr] Güncel kart sayýsýný göster
    }

    public void RemoveCard(Card card)
    {
        if (cardsInHand.Contains(card))  // [en] Check if card exists in hand / [tr] Kart elde varsa
        {
            cardsInHand.Remove(card);    // [en] Remove card from list        / [tr] Kartý listeden çýkar
            DisplayHand();               // [en] Update UI                    / [tr] UI'yý güncelle
            UpdateCardCountText();       // [en] Update card count text       / [tr] Kart sayýsý metnini güncelle
        }
    }
}
