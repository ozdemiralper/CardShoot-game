using System.Collections.Generic;          // [en] For List / [tr] Liste için
using System.Linq;                         // [en] For LINQ queries / [tr] LINQ sorguları için
using UnityEngine;                         // [en] Unity core / [tr] Unity çekirdek

public class AIPlayHandler : MonoBehaviour
{
    public List<Card> defenseCards = new List<Card>();        // [en] AI defense cards     / [tr] AI defans kartları
    public List<Card> midfieldCards = new List<Card>();       // [en] AI midfield cards    / [tr] AI orta saha kartları
    public List<Card> forwardCards = new List<Card>();        // [en] AI forward cards     / [tr] AI forvet kartları
    public List<Card> weatherCards = new List<Card>();        // [en] AI weather cards     / [tr] AI hava kartları
    public List<Card> captainCards = new List<Card>();        // [en] AI captain cards     / [tr] AI kaptan kartları
    public List<Card> captainDefenseCards = new List<Card>(); // [en] AI defense captains  / [tr] AI defans kaptanları
    public List<Card> captainMidfieldCards = new List<Card>();// [en] AI midfield captains / [tr] AI orta saha kaptanları
    public List<Card> captainForwardCards = new List<Card>(); // [en] AI forward captains  / [tr] AI forvet kaptanları

    public TMPro.TMP_Text defensePowerText;   // [en] UI text for defense power  / [tr] Defans gücü UI metni
    public TMPro.TMP_Text midfieldPowerText;  // [en] UI text for midfield power / [tr] Orta saha gücü UI metni
    public TMPro.TMP_Text forwardPowerText;   // [en] UI text for forward power  / [tr] Forvet gücü UI metni
    public TMPro.TMP_Text totalPowerText;     // [en] UI text for total power    / [tr] Toplam güç UI metni

    public Transform defenseArea;              // [en] Transform for defense area  / [tr] Defans alanı Transform
    public Transform midfieldArea;             // [en] Transform for midfield area / [tr] Orta saha alanı Transform
    public Transform forwardArea;              // [en] Transform for forward area  / [tr] Forvet alanı Transform

    public Transform defenseCaptainArea;       // [en] Transform for defense captain area  / [tr] Defans kaptan alanı
    public Transform midfieldCaptainArea;      // [en] Transform for midfield captain area / [tr] Orta saha kaptan alanı
    public Transform forwardCaptainArea;       // [en] Transform for forward captain area  / [tr] Forvet kaptan alanı

    public Transform weatherArea;              // [en] Transform for weather cards / [tr] Hava kartları alanı

    public GameObject aiPlayerCardPrefab;     // [en] Prefab for AI player card / [tr] AI oyuncu kart prefabı
    public GameObject weatherCardPrefab;      // [en] Prefab for weather card   / [tr] Hava kartı prefabı
    public GameObject captainCardPrefab;      // [en] Prefab for captain card   / [tr] Kaptan kart prefabı

    public List<Card> handCards = new List<Card>(); // [en] Cards in AI hand   / [tr] AI elindeki kartlar
    public static AIPlayHandler Instance;           // [en] Singleton instance / [tr] Tekil örnek

    void Awake()
    {
        if (Instance == null) Instance = this;   // [en] Assign singleton instance / [tr] Tekil örnek ata
        else Destroy(gameObject);                // [en] Destroy duplicates        / [tr] Çoğaltılmış objeyi sil
    }

    public void PlayCard(Card card)
    {
        if (card.isPlayed) return;               // [en] Skip if card already played / [tr] Kart oynandıysa atla
        if ((card.cardType == CardType.Weather && GameManager.Instance.activeWeatherCards.Any(c => c.cardPower == card.cardPower && c.position == card.position)) ||
            (card.cardType == CardType.Captain && captainCards.Any(c => c.cardPower == card.cardPower))) return; // [en] Prevent duplicate weather/captain / [tr] Duplicate weather/captain önle

        card.isPlayed = true;                    // [en] Mark as played                / [tr] Oynandı olarak işaretle
        Transform targetArea = null;             // [en] Target area for instantiation / [tr] Instantiate için hedef alan

        switch (card.cardType)
        {
            case CardType.Player:
                switch (card.position)
                {
                    case 0: targetArea = defenseArea; defenseCards.Add(card); break;   // [en] Add to defense  / [tr] Defansa ekle
                    case 1: targetArea = midfieldArea; midfieldCards.Add(card); break; // [en] Add to midfield / [tr] Orta sahaya ekle
                    case 2: targetArea = forwardArea; forwardCards.Add(card); break;   // [en] Add to forward  / [tr] Forvete ekle
                }
                break;
            case CardType.Captain:
                switch (card.cardPower)
                {
                    case 0: targetArea = defenseCaptainArea; captainDefenseCards.Add(card); captainCards.Add(card); break;   // [en] Defense captain  / [tr] Defans kaptanı
                    case 1: targetArea = midfieldCaptainArea; captainMidfieldCards.Add(card); captainCards.Add(card); break; // [en] Midfield captain / [tr] Orta saha kaptanı
                    case 2: targetArea = forwardCaptainArea; captainForwardCards.Add(card); captainCards.Add(card); break;   // [en] Forward captain  / [tr] Forvet kaptanı
                }
                break;
            case CardType.Weather:
                targetArea = weatherArea;                          // [en] Weather area          / [tr] Hava kart alanı
                weatherCards.Add(card);                            // [en] Add to weather list   / [tr] Weather listesine ekle
                GameManager.Instance.activeWeatherCards.Add(card); // [en] Add to active weather / [tr] Aktif hava listesine ekle
                break;
        }

        if (targetArea != null)
        {
            GameObject prefab = card.cardType switch
            {
                CardType.Player => aiPlayerCardPrefab, // [en] Player prefab  / [tr] Oyuncu prefabı
                CardType.Captain => captainCardPrefab, // [en] Captain prefab / [tr] Kaptan prefabı
                CardType.Weather => weatherCardPrefab, // [en] Weather prefab / [tr] Hava prefabı
                _ => aiPlayerCardPrefab
            };

            GameObject cardObj = Instantiate(prefab, targetArea);              // [en] Instantiate card              / [tr] Kart oluştur
            CardDisplay display = cardObj.GetComponent<CardDisplay>();         // [en] Get CardDisplay component     / [tr] CardDisplay bileşenini al
            SelectableCard selectable = cardObj.GetComponent<SelectableCard>(); // [en] Get SelectableCard component / [tr] SelectableCard bileşenini al
            if (display != null) display.SetCard(card);                         // [en] Set card visuals             / [tr] Kart görsellerini ayarla
            if (selectable != null) selectable.card = card;                     // [en] Assign card data             / [tr] Kart verisini ata
        }

        switch (card.position)
        {
            case 0: GameManager.Instance.aiDefenseCards.Add(card); break;  // [en] Add to GameManager defense  / [tr] GameManager defans listesine ekle
            case 1: GameManager.Instance.aiMidfieldCards.Add(card); break; // [en] Add to GameManager midfield / [tr] GameManager orta saha listesine ekle
            case 2: GameManager.Instance.aiForwardCards.Add(card); break;  // [en] Add to GameManager forward  / [tr] GameManager forvet listesine ekle
        }
        if (card.cardType == CardType.Captain) GameManager.Instance.aiCaptainCards.Add(card); // [en] Add captain to GameManager / [tr] Kaptanı GameManager listesine ekle
        UpdateAllCardPowers();                     // [en] Update all powers  / [tr] Tüm güçleri güncelle
        GameManager.Instance.OnAICardPlayed(card); // [en] Notify GameManager / [tr] GameManager'ı bilgilendir
    }

    public void UpdateAllCardPowers()
    {
        List<Card> allAICards = new List<Card>();             // [en] Collect all AI cards / [tr] Tüm AI kartlarını topla
        allAICards.AddRange(defenseCards);
        allAICards.AddRange(midfieldCards);
        allAICards.AddRange(forwardCards);

        List<Card> allPlayerCards = new List<Card>();         // [en] Collect all player cards / [tr] Tüm oyuncu kartlarını topla
        allPlayerCards.AddRange(GameManager.Instance.playerDefenseCards);
        allPlayerCards.AddRange(GameManager.Instance.playerMidfieldCards);
        allPlayerCards.AddRange(GameManager.Instance.playerForwardCards);

        foreach (var c in allAICards) c.cardPower = c.originalPower;      // [en] Reset AI powers      / [tr] AI güçlerini sıfırla
        foreach (var c in allPlayerCards) c.cardPower = c.originalPower;   // [en] Reset player powers / [tr] Oyuncu güçlerini sıfırla

        var counts = allAICards.GroupBy(c => c.cardID).ToDictionary(g => g.Key, g => g.Count()); // [en] Count duplicate AI cards / [tr] AI duplicate kartlarını say

        foreach (var c in allAICards)
            if (counts[c.cardID] == 2) c.cardPower = c.originalPower * 2; // [en] Double power for duplicate    / [tr] Duplicate kart için güç iki kat

        foreach (var captain in captainCards)                             // [en] Apply captain effect          / [tr] Kaptan etkisini uygula
        {
            var affected = allAICards.Where(c => c.position == captain.position && c.cardType == CardType.Player);
            foreach (var c in affected) c.cardPower *= 2;                 // [en] Double power for same position / [tr] Aynı pozisyondaki kartın gücünü iki kat yap
        }

        foreach (var w in GameManager.Instance.activeWeatherCards)        // [en] Apply weather effect           / [tr] Hava kartı etkisini uygula
        {
            var affectedPlayer = allPlayerCards.Where(c => c.position == w.position && c.cardType == CardType.Player);
            foreach (var c in affectedPlayer)
            {
                if (captainCards.Any(cap => cap.position == c.position)) c.cardPower = 2; // [en] Captain overrides weather / [tr] Kaptan hava etkisini geçersiz kılar
                else c.cardPower = 1; // [en] Weather reduces power / [tr] Hava gücü 1 yapar
            }

            var affectedAI = allAICards.Where(c => c.position == w.position && c.cardType == CardType.Player);
            foreach (var c in affectedAI)
            {
                if (captainCards.Any(cap => cap.position == c.position) || GameManager.Instance.aiCaptainCards.Any(cap => cap.position == c.position))
                    c.cardPower = 2; // [en] Captain present / [tr] Kaptan varsa güç 2
                else c.cardPower = 1;  // [en] Weather effect / [tr] Hava etkisi
            }
        }

        UpdateCardVisualsInArea(defenseArea);   // [en] Update visuals defense  / [tr] Defans görsellerini güncelle
        UpdateCardVisualsInArea(midfieldArea);  // [en] Update visuals midfield / [tr] Orta saha görsellerini güncelle
        UpdateCardVisualsInArea(forwardArea);   // [en] Update visuals forward  / [tr] Forvet görsellerini güncelle
        UpdatePowerTexts();                     // [en] Update UI power texts   / [tr] UI güç metinlerini güncelle
    }

    private void UpdateCardVisualsInArea(Transform area) // [en] Update visuals in given area / [tr] Verilen alandaki görselleri güncelle
    {
        foreach (Transform child in area)
        {
            CardDisplay display = child.GetComponent<CardDisplay>();          // [en] Get CardDisplay    / [tr] CardDisplay al
            SelectableCard selectable = child.GetComponent<SelectableCard>(); // [en] Get SelectableCard / [tr] SelectableCard al
            if (display != null && selectable != null && selectable.card.cardType == CardType.Player)
                display.SetCard(selectable.card);                             // [en] Update display     / [tr] Görseli güncelle
        }
    }

    private int CalculateTotalPower(List<Card> cards)   // [en] Calculate total power / [tr] Toplam gücü hesapla
    {
        int total = 0;
        foreach (var c in cards) total += c.cardPower;  // [en] Sum card powers       / [tr] Kart güçlerini topla
        return total;                                   // [en] Return total          / [tr] Toplamı döndür
    }

    public void UpdatePowerTexts() // [en] Update UI texts / [tr] UI metinlerini güncelle
    {
        defensePowerText.text = CalculateTotalPower(defenseCards).ToString();    // [en] Defense power  / [tr] Defans gücü
        midfieldPowerText.text = CalculateTotalPower(midfieldCards).ToString();  // [en] Midfield power / [tr] Orta saha gücü
        forwardPowerText.text = CalculateTotalPower(forwardCards).ToString();    // [en] Forward power  / [tr] Forvet gücü
        totalPowerText.text = (CalculateTotalPower(defenseCards) +               // [en] Total power    / [tr] Toplam güç
                               CalculateTotalPower(midfieldCards) +
                               CalculateTotalPower(forwardCards)).ToString();
    }
}

