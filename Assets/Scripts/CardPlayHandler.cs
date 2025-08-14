using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardPlayHandler : MonoBehaviour
{
    public List<Card> defenseCards = new List<Card>();         // [en] Player defense cards          / [tr] Oyuncu defans kartlarý
    public List<Card> midfieldCards = new List<Card>();        // [en] Player midfield cards         / [tr] Oyuncu orta saha kartlarý
    public List<Card> forwardCards = new List<Card>();         // [en] Player forward cards          / [tr] Oyuncu forvet kartlarý
    public List<Card> weatherCards = new List<Card>();         // [en] Player weather cards          / [tr] Oyuncu hava kartlarý
    public List<Card> captainCards = new List<Card>();         // [en] Player captain cards          / [tr] Oyuncu kaptan kartlarý
    public List<Card> captainDefenseCards = new List<Card>();  // [en] Player defense captain cards  / [tr] Oyuncu defans kaptan kartlarý
    public List<Card> captainMidfieldCards = new List<Card>(); // [en] Player midfield captain cards / [tr] Oyuncu orta saha kaptan kartlarý
    public List<Card> captainForwardCards = new List<Card>();  // [en] Player forward captain cards  / [tr] Oyuncu forvet kaptan kartlarý
    public List<Card> coachCards = new List<Card>();           // [en] Coach cards                   / [tr] Teknik adam kartlarý

    public TMPro.TMP_Text defensePowerText;                    // [en] UI text defense               / [tr] Defans güç metni
    public TMPro.TMP_Text midfieldPowerText;                   // [en] UI text midfield              / [tr] Orta saha güç metni
    public TMPro.TMP_Text forwardPowerText;                    // [en] UI text forward               / [tr] Forvet güç metni
    public TMPro.TMP_Text totalPowerText;                      // [en] UI text total                 / [tr] Toplam güç metni

    public Transform defenseArea;                              // [en] Defense area transform        / [tr] Defans alaný
    public Transform midfieldArea;                             // [en] Midfield area transform       / [tr] Orta saha alaný
    public Transform forwardArea;                              // [en] Forward area transform        / [tr] Forvet alaný
    public Transform defenseCaptainArea;                       // [en] Defense captain area          / [tr] Defans kaptan alaný
    public Transform midfieldCaptainArea;                      // [en] Midfield captain area         / [tr] Orta saha kaptan alaný
    public Transform forwardCaptainArea;                       // [en] Forward captain area          / [tr] Forvet kaptan alaný
    public Transform weatherArea;                              // [en] Weather cards area            / [tr] Hava kartlarý alaný

    public GameObject playerCardPrefab;                        // [en] Player card prefab            / [tr] Oyuncu kart prefab
    public GameObject weatherCardPrefab;                       // [en] Weather card prefab           / [tr] Hava kart prefab
    public GameObject captainCardPrefab;                       // [en] Captain card prefab           / [tr] Kaptan kart prefab

    public PlayerHand playerHand;                              // [en] Player hand manager           / [tr] Oyuncu eli yöneticisi
    public static CardPlayHandler Instance;                    // [en] Singleton instance            / [tr] Tekil örnek

    void Awake()
    {
        if (Instance == null) Instance = this;  // [en] Assign singleton  / [tr] Tekil örnek ata
        else Destroy(gameObject);               // [en] Destroy duplicate / [tr] Çoðaltýlmýþ objeyi sil
    }

    public void PlayCard(SelectableCard selectedCard)
    {
        if (GameManager.Instance.currentTurn != GameTurn.Player) // [en] Only player can play / [tr] Sadece oyuncu oynayabilir
        {
            Debug.Log("Þu an sýra AI'de, kart oynayamazsýn!");   // [en] AI turn / [tr] AI sýrasý
            return;
        }

        Card card = selectedCard.card; // [en] Get card       / [tr] Kartý al
        if (card.isPlayed) return;     // [en] Already played / [tr] Zaten oynandý

        if ((card.cardType == CardType.Weather && GameManager.Instance.activeWeatherCards.Any(c => c.cardPower == card.cardPower && c.position == card.position)) ||
            (card.cardType == CardType.Captain && captainCards.Any(c => c.cardPower == card.cardPower))) // [en] Already on field / [tr] Zaten sahada
        {
            Debug.Log("Bu kart zaten sahada!");
            return;
        }

        Destroy(selectedCard.gameObject);     // [en] Remove from hand             / [tr] Elinden kaldýr
        playerHand.RemoveCard(card);          // [en] Remove from player hand list / [tr] El listesinden kaldýr
        card.isPlayed = true;                 // [en] Mark as played               / [tr] Oynandý olarak iþaretle
        Transform targetArea = null;          // [en] Target area to place card    / [tr] Kartýn konacaðý alan

        switch (card.cardType)
        {
            case CardType.Player:
                switch (card.position)
                {
                    case 0: targetArea = defenseArea; defenseCards.Add(card); break;    // [en] Defense  / [tr] Defans
                    case 1: targetArea = midfieldArea; midfieldCards.Add(card); break;  // [en] Midfield / [tr] Orta saha
                    case 2: targetArea = forwardArea; forwardCards.Add(card); break;    // [en] Forward  / [tr] Forvet
                }
                break;
            case CardType.Captain:
                switch (card.cardPower)
                {
                    case 0: targetArea = defenseCaptainArea; captainDefenseCards.Add(card); captainCards.Add(card); break;   // [en] Defense captain  / [tr] Defans kaptan
                    case 1: targetArea = midfieldCaptainArea; captainMidfieldCards.Add(card); captainCards.Add(card); break; // [en] Midfield captain / [tr] Orta saha kaptan
                    case 2: targetArea = forwardCaptainArea; captainForwardCards.Add(card); captainCards.Add(card); break;   // [en] Forward captain  / [tr] Forvet kaptan
                }
                break;
            case CardType.Weather:
                targetArea = weatherArea; weatherCards.Add(card); GameManager.Instance.activeWeatherCards.Add(card); // [en] Weather effect / [tr] Hava kartý ekle
                break;
        }

        if (targetArea != null)
        {
            GameObject prefabToUse = card.cardType switch
            {
                CardType.Player => playerCardPrefab,
                CardType.Weather => weatherCardPrefab,
                CardType.Captain => captainCardPrefab,
                _ => playerCardPrefab
            }; // [en] Choose prefab / [tr] Prefab seç
            GameObject cardObj = Instantiate(prefabToUse, targetArea); // [en] Instantiate card / [tr] Kartý oluþtur
            CardDisplay display = cardObj.GetComponent<CardDisplay>(); // [en] Get display / [tr] Görsel al
            if (display != null) display.SetCard(card);

            SelectableCard selectable = cardObj.GetComponent<SelectableCard>(); // [en] Set selectable / [tr] Selectable ata
            if (selectable != null) { selectable.card = card; selectable.cardDisplay = display; }
        }

        UpdateAllPlayerCardPowers();                   // [en] Update powers / [tr] Kart güçlerini güncelle
        UpdatePowerTexts();                            // [en] Update UI / [tr] UI metinlerini güncelle
        GameManager.Instance.OnPlayerCardPlayed(card); // [en] Notify game manager / [tr] GameManager’a bildir
        GameManager.Instance.EndTurn();                // [en] End player turn / [tr] Sýrayý bitir
    }

    public void UpdateAllPlayerCardPowers()
    {
        List<Card> allPlayerCards = new List<Card>(); allPlayerCards.AddRange(defenseCards); allPlayerCards.AddRange(midfieldCards); allPlayerCards.AddRange(forwardCards); // [en] All player cards / [tr] Tüm oyuncu kartlarý
        List<Card> allAICards = new List<Card>(); allAICards.AddRange(GameManager.Instance.aiDefenseCards); allAICards.AddRange(GameManager.Instance.aiMidfieldCards); allAICards.AddRange(GameManager.Instance.aiForwardCards); // [en] All AI cards / [tr] Tüm AI kartlarý

        foreach (var card in allPlayerCards) card.cardPower = card.originalPower; // [en] Reset power / [tr] Gücü sýfýrla
        foreach (var card in allAICards) card.cardPower = card.originalPower;

        var cardGroups = allPlayerCards.GroupBy(c => c.cardID); // [en] Duplicate check / [tr] Duplicate kontrol
        foreach (var group in cardGroups) if (group.Count() == 2) foreach (var card in group) card.cardPower = card.originalPower * 2; // [en] Double power / [tr] Gücü iki kat yap

        foreach (var captainCard in captainCards) // [en] Captain effect / [tr] Kaptan etkisi
        {
            var affected = allPlayerCards.Where(c => c.position == captainCard.position && c.cardType == CardType.Player); // [en] Cards in same position / [tr] Ayný pozisyondaki kartlar
            foreach (var c in affected) c.cardPower *= 2; // [en] Double power / [tr] Gücü iki kat yap
        }

        // 4. Weather effect (player and AI) / [tr] Hava kartý etkisi (player ve AI)
        foreach (var w in GameManager.Instance.activeWeatherCards)
        {
            var affectedPlayer = allPlayerCards.Where(c => c.position == w.position && c.cardType == CardType.Player); // [en] Player cards affected / [tr] Etkilenen oyuncu kartlarý
            foreach (var c in affectedPlayer)
            {
                if (c.cardPower > 1) c.cardPower = 2; // [en] If captain/duplicate, set 2 / [tr] Eðer kaptan/duplicate, 2 yap
                else c.cardPower = 1;                 // [en] Otherwise set 1 / [tr] Aksi halde 1 yap
            }

            var affectedAI = allAICards.Where(c => c.position == w.position && c.cardType == CardType.Player); // [en] AI cards affected / [tr] Etkilenen AI kartlarý
            foreach (var c in affectedAI)
            {
                if (c.cardPower > 1) c.cardPower = 2;
                else c.cardPower = 1;
            }
        }

        // Update card visuals in UI / [tr] Kart görsellerini güncelle
        UpdateCardVisualsInArea(defenseArea);
        UpdateCardVisualsInArea(midfieldArea);
        UpdateCardVisualsInArea(forwardArea);
    }

    private void UpdateCardVisualsInArea(Transform area) // [en] Update visuals / [tr] Görselleri güncelle
    {
        foreach (Transform child in area)
        {
            CardDisplay display = child.GetComponent<CardDisplay>();
            SelectableCard selectable = child.GetComponent<SelectableCard>();
            if (display != null && selectable != null && selectable.card.cardType == CardType.Player)
                display.SetCard(selectable.card);
        }
    }

    private int CalculateTotalPower(List<Card> cards) // [en] Calculate total power / [tr] Toplam gücü hesapla
    {
        int total = 0;
        foreach (var c in cards) total += c.cardPower;
        return total;
    }

    public void UpdatePowerTexts() // [en] Update UI texts / [tr] Güç metinlerini güncelle
    {
        defensePowerText.text = CalculateTotalPower(defenseCards).ToString();   // [en] Defense total / [tr] Defans toplam
        midfieldPowerText.text = CalculateTotalPower(midfieldCards).ToString(); // [en] Midfield total / [tr] Orta saha toplam
        forwardPowerText.text = CalculateTotalPower(forwardCards).ToString();   // [en] Forward total / [tr] Forvet toplam
        totalPowerText.text = (CalculateTotalPower(defenseCards) +
                               CalculateTotalPower(midfieldCards) +
                               CalculateTotalPower(forwardCards)).ToString(); // [en] Total / [tr] Toplam
    }
}

