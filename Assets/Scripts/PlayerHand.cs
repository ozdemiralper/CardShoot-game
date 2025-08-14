using System.Collections.Generic; // [en] For using lists      / [tr] List kullan�m� i�in
using UnityEngine;                // [en] Unity core namespace / [tr] Unity temel isim alan�
using TMPro;                     // [en] For TextMeshPro UI    / [tr] TextMeshPro aray�z� i�in

public class PlayerHand : MonoBehaviour
{
    [Header("Card Prefabs")]              // [en] Prefabs section in inspector / [tr] Inspector'da prefablar b�l�m�
    public GameObject playerCardPrefab;   // [en] Prefab for player cards      / [tr] Oyuncu kartlar� i�in prefab
    public GameObject captainCardPrefab;  // [en] Prefab for captain cards     / [tr] Kaptan kartlar� i�in prefab
    public GameObject weatherCardPrefab;  // [en] Prefab for weather cards     / [tr] Hava durumu kartlar� i�in prefab

    public Transform handPanel;           // [en] Panel where cards will be displayed / [tr] Kartlar�n g�sterilece�i panel
    private List<Card> cardsInHand = new List<Card>(); // [en] List of cards in player's hand / [tr] Oyuncunun elindeki kart listesi

    public TMP_Text cardCountText;   // [en] Reference to TextMeshPro text component / [tr] TextMeshPro UI referans�

    void Start()
    {
        ClearHand();                // [en] Clear existing cards / [tr] Mevcut kartlar� temizle

        if (DeckManager.Instance != null && DeckManager.Instance.selectedGameDeck.Count > 0) // [en] Check if GameManager and selected cards exist / [tr] GameManager ve se�ili kartlar varsa kontrol et
        {
            List<Card> sortedCards = new List<Card>(DeckManager.Instance.selectedGameDeck); // [en] Copy selected cards list / [tr] Se�ilen kart listesinin kopyas�n� al
            sortedCards.Sort((a, b) => b.cardID.CompareTo(a.cardID));                      // [en] Sort cards by cardID descending / [tr] Kartlar� cardID'ye g�re b�y�kten k����e s�rala

            foreach (var card in sortedCards)  // [en] Add sorted cards to hand / [tr] S�ral� kartlar� ele ekle
            {
                AddCard(card);                // [en] Add a card to hand list and display / [tr] Kart� el listesine ekle ve g�ster
            }
        }
        else
        {
            Debug.LogWarning("Se�ilen kart listesi bo� veya GameManager bulunamad�."); // [en] Warning if no cards or manager / [tr] Kartlar ya da manager yoksa uyar� ver
            // [en] Optional: Add random cards if none selected / [tr] �stersen buraya rastgele kart ekleme ekleyebilirsin
        }
    }

    public void AddCard(Card card)
    {
        cardsInHand.Add(card);   // [en] Add card to hand list / [tr] Kart� el listesine ekle
        DisplayHand();           // [en] Update UI display / [tr] UI g�r�nt�s�n� g�ncelle
        UpdateCardCountText();   // [en] Update card count text / [tr] Kart say�s� metnini g�ncelle
    }

    public void ClearHand()
    {
        cardsInHand.Clear();     // [en] Clear card list / [tr] Kart listesini temizle

        foreach (Transform child in handPanel)  // [en] Destroy all card objects in UI / [tr] UI'daki t�m kart objelerini yok et
            Destroy(child.gameObject);

        UpdateCardCountText();   // [en] Update card count text / [tr] Kart say�s� metnini g�ncelle
    }

    public int GetCardCount()
    {
        return cardsInHand.Count;  // [en] Return current number of cards / [tr] Elindeki kart say�s�n� d�nd�r
    }

    public void DisplayHand()
    {
        foreach (Transform child in handPanel)  // [en] Clear existing card UI / [tr] Mevcut kart UI'lar�n� temizle
            Destroy(child.gameObject);

        foreach (Card card in cardsInHand)      // [en] Create UI objects for each card / [tr] Her kart i�in UI objesi olu�tur
        {
            GameObject selectedPrefab = GetPrefabForCard(card.cardType);  // [en] Get prefab by card type / [tr] Kart tipine g�re prefab se�
            GameObject cardObj = Instantiate(selectedPrefab, handPanel); // [en] Instantiate prefab under hand panel / [tr] Prefab� handPanel alt�na olu�tur

            SelectableCard selectable = cardObj.GetComponent<SelectableCard>(); // [en] Get SelectableCard component / [tr] SelectableCard bile�enini al
            if (selectable != null)
            {
                selectable.SetCard(card);  // [en] Assign card data to component / [tr] Kart verisini atama yap
            }
        }

        UpdateCardCountText();   // [en] Update card count text / [tr] Kart say�s� metnini g�ncelle
    }

    private GameObject GetPrefabForCard(CardType type)
    {
        switch (type) // [en] Select prefab based on card type / [tr] Kart tipine g�re prefab se�
        {
            case CardType.Player: return playerCardPrefab;   // [en] Player card prefab  / [tr] Oyuncu kart� prefab�
            case CardType.Captain: return captainCardPrefab; // [en] Captain card prefab / [tr] Kaptan kart� prefab
            case CardType.Weather: return weatherCardPrefab; // [en] Weather card prefab / [tr] Hava durumu kart� prefab�
            default: return playerCardPrefab;                // [en] Default prefab      / [tr] Varsay�lan prefab
        }
    }

    private void UpdateCardCountText()
    {
        if (cardCountText != null)                        // [en] Check if text component is assigned / [tr] Text bile�eni atanm��sa
            cardCountText.text = $"{cardsInHand.Count}";  // [en] Update with current card count      / [tr] G�ncel kart say�s�n� g�ster
    }

    public void RemoveCard(Card card)
    {
        if (cardsInHand.Contains(card))  // [en] Check if card exists in hand / [tr] Kart elde varsa
        {
            cardsInHand.Remove(card);    // [en] Remove card from list        / [tr] Kart� listeden ��kar
            DisplayHand();               // [en] Update UI                    / [tr] UI'y� g�ncelle
            UpdateCardCountText();       // [en] Update card count text       / [tr] Kart say�s� metnini g�ncelle
        }
    }
}
