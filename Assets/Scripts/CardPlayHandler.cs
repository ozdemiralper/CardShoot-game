using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardPlayHandler : MonoBehaviour
{
    public List<Card> defenseCards = new List<Card>();         // [en] Player defense cards          / [tr] Oyuncu defans kartlar�
    public List<Card> midfieldCards = new List<Card>();        // [en] Player midfield cards         / [tr] Oyuncu orta saha kartlar�
    public List<Card> forwardCards = new List<Card>();         // [en] Player forward cards          / [tr] Oyuncu forvet kartlar�
    public List<Card> weatherCards = new List<Card>();         // [en] Player weather cards          / [tr] Oyuncu hava kartlar�
    public List<Card> captainCards = new List<Card>();         // [en] Player captain cards          / [tr] Oyuncu kaptan kartlar�
    public List<Card> captainDefenseCards = new List<Card>();  // [en] Player defense captain cards  / [tr] Oyuncu defans kaptan kartlar�
    public List<Card> captainMidfieldCards = new List<Card>(); // [en] Player midfield captain cards / [tr] Oyuncu orta saha kaptan kartlar�
    public List<Card> captainForwardCards = new List<Card>();  // [en] Player forward captain cards  / [tr] Oyuncu forvet kaptan kartlar�
    public List<Card> coachCards = new List<Card>();           // [en] Coach cards                   / [tr] Teknik adam kartlar�

    public TMPro.TMP_Text defensePowerText;                    // [en] UI text defense               / [tr] Defans g�� metni
    public TMPro.TMP_Text midfieldPowerText;                   // [en] UI text midfield              / [tr] Orta saha g�� metni
    public TMPro.TMP_Text forwardPowerText;                    // [en] UI text forward               / [tr] Forvet g�� metni
    public TMPro.TMP_Text totalPowerText;                      // [en] UI text total                 / [tr] Toplam g�� metni

    public Transform defenseArea;                              // [en] Defense area transform        / [tr] Defans alan�
    public Transform midfieldArea;                             // [en] Midfield area transform       / [tr] Orta saha alan�
    public Transform forwardArea;                              // [en] Forward area transform        / [tr] Forvet alan�
    public Transform defenseCaptainArea;                       // [en] Defense captain area          / [tr] Defans kaptan alan�
    public Transform midfieldCaptainArea;                      // [en] Midfield captain area         / [tr] Orta saha kaptan alan�
    public Transform forwardCaptainArea;                       // [en] Forward captain area          / [tr] Forvet kaptan alan�
    public Transform weatherArea;                              // [en] Weather cards area            / [tr] Hava kartlar� alan�

    public GameObject playerCardPrefab;                        // [en] Player card prefab            / [tr] Oyuncu kart prefab
    public GameObject weatherCardPrefab;                       // [en] Weather card prefab           / [tr] Hava kart prefab
    public GameObject captainCardPrefab;                       // [en] Captain card prefab           / [tr] Kaptan kart prefab

    public PlayerHand playerHand;                              // [en] Player hand manager           / [tr] Oyuncu eli y�neticisi
    public static CardPlayHandler Instance;                    // [en] Singleton instance            / [tr] Tekil �rnek

    void Awake()
    {
        if (Instance == null) Instance = this;  // [en] Assign singleton  / [tr] Tekil �rnek ata
        else Destroy(gameObject);               // [en] Destroy duplicate / [tr] �o�alt�lm�� objeyi sil
    }

    public void PlayCard(SelectableCard selectedCard)
    {
        if (GameManager.Instance.currentTurn != GameTurn.Player) // [en] Only player can play / [tr] Sadece oyuncu oynayabilir
        {
            Debug.Log("�u an s�ra AI'de, kart oynayamazs�n!");   // [en] AI turn / [tr] AI s�ras�
            return;
        }

        Card card = selectedCard.card; // [en] Get card       / [tr] Kart� al
        if (card.isPlayed) return;     // [en] Already played / [tr] Zaten oynand�

        if ((card.cardType == CardType.Weather && GameManager.Instance.activeWeatherCards.Any(c => c.cardPower == card.cardPower && c.position == card.position)) ||
            (card.cardType == CardType.Captain && captainCards.Any(c => c.cardPower == card.cardPower))) // [en] Already on field / [tr] Zaten sahada
        {
            Debug.Log("Bu kart zaten sahada!");
            return;
        }

        Destroy(selectedCard.gameObject);     // [en] Remove from hand             / [tr] Elinden kald�r
        playerHand.RemoveCard(card);          // [en] Remove from player hand list / [tr] El listesinden kald�r
        card.isPlayed = true;                 // [en] Mark as played               / [tr] Oynand� olarak i�aretle
        Transform targetArea = null;          // [en] Target area to place card    / [tr] Kart�n konaca�� alan

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
                targetArea = weatherArea; weatherCards.Add(card); GameManager.Instance.activeWeatherCards.Add(card); // [en] Weather effect / [tr] Hava kart� ekle
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
            }; // [en] Choose prefab / [tr] Prefab se�
            GameObject cardObj = Instantiate(prefabToUse, targetArea); // [en] Instantiate card / [tr] Kart� olu�tur
            CardDisplay display = cardObj.GetComponent<CardDisplay>(); // [en] Get display / [tr] G�rsel al
            if (display != null) display.SetCard(card);

            SelectableCard selectable = cardObj.GetComponent<SelectableCard>(); // [en] Set selectable / [tr] Selectable ata
            if (selectable != null) { selectable.card = card; selectable.cardDisplay = display; }
        }

        UpdateAllPlayerCardPowers();                   // [en] Update powers / [tr] Kart g��lerini g�ncelle
        UpdatePowerTexts();                            // [en] Update UI / [tr] UI metinlerini g�ncelle
        GameManager.Instance.OnPlayerCardPlayed(card); // [en] Notify game manager / [tr] GameManager�a bildir
        GameManager.Instance.EndTurn();                // [en] End player turn / [tr] S�ray� bitir
    }

    public void UpdateAllPlayerCardPowers()
    {
        List<Card> allPlayerCards = new List<Card>(); allPlayerCards.AddRange(defenseCards); allPlayerCards.AddRange(midfieldCards); allPlayerCards.AddRange(forwardCards); // [en] All player cards / [tr] T�m oyuncu kartlar�
        List<Card> allAICards = new List<Card>(); allAICards.AddRange(GameManager.Instance.aiDefenseCards); allAICards.AddRange(GameManager.Instance.aiMidfieldCards); allAICards.AddRange(GameManager.Instance.aiForwardCards); // [en] All AI cards / [tr] T�m AI kartlar�

        foreach (var card in allPlayerCards) card.cardPower = card.originalPower; // [en] Reset power / [tr] G�c� s�f�rla
        foreach (var card in allAICards) card.cardPower = card.originalPower;

        var cardGroups = allPlayerCards.GroupBy(c => c.cardID); // [en] Duplicate check / [tr] Duplicate kontrol
        foreach (var group in cardGroups) if (group.Count() == 2) foreach (var card in group) card.cardPower = card.originalPower * 2; // [en] Double power / [tr] G�c� iki kat yap

        foreach (var captainCard in captainCards) // [en] Captain effect / [tr] Kaptan etkisi
        {
            var affected = allPlayerCards.Where(c => c.position == captainCard.position && c.cardType == CardType.Player); // [en] Cards in same position / [tr] Ayn� pozisyondaki kartlar
            foreach (var c in affected) c.cardPower *= 2; // [en] Double power / [tr] G�c� iki kat yap
        }

        // 4. Weather effect (player and AI) / [tr] Hava kart� etkisi (player ve AI)
        foreach (var w in GameManager.Instance.activeWeatherCards)
        {
            var affectedPlayer = allPlayerCards.Where(c => c.position == w.position && c.cardType == CardType.Player); // [en] Player cards affected / [tr] Etkilenen oyuncu kartlar�
            foreach (var c in affectedPlayer)
            {
                if (c.cardPower > 1) c.cardPower = 2; // [en] If captain/duplicate, set 2 / [tr] E�er kaptan/duplicate, 2 yap
                else c.cardPower = 1;                 // [en] Otherwise set 1 / [tr] Aksi halde 1 yap
            }

            var affectedAI = allAICards.Where(c => c.position == w.position && c.cardType == CardType.Player); // [en] AI cards affected / [tr] Etkilenen AI kartlar�
            foreach (var c in affectedAI)
            {
                if (c.cardPower > 1) c.cardPower = 2;
                else c.cardPower = 1;
            }
        }

        // Update card visuals in UI / [tr] Kart g�rsellerini g�ncelle
        UpdateCardVisualsInArea(defenseArea);
        UpdateCardVisualsInArea(midfieldArea);
        UpdateCardVisualsInArea(forwardArea);
    }

    private void UpdateCardVisualsInArea(Transform area) // [en] Update visuals / [tr] G�rselleri g�ncelle
    {
        foreach (Transform child in area)
        {
            CardDisplay display = child.GetComponent<CardDisplay>();
            SelectableCard selectable = child.GetComponent<SelectableCard>();
            if (display != null && selectable != null && selectable.card.cardType == CardType.Player)
                display.SetCard(selectable.card);
        }
    }

    private int CalculateTotalPower(List<Card> cards) // [en] Calculate total power / [tr] Toplam g�c� hesapla
    {
        int total = 0;
        foreach (var c in cards) total += c.cardPower;
        return total;
    }

    public void UpdatePowerTexts() // [en] Update UI texts / [tr] G�� metinlerini g�ncelle
    {
        defensePowerText.text = CalculateTotalPower(defenseCards).ToString();   // [en] Defense total / [tr] Defans toplam
        midfieldPowerText.text = CalculateTotalPower(midfieldCards).ToString(); // [en] Midfield total / [tr] Orta saha toplam
        forwardPowerText.text = CalculateTotalPower(forwardCards).ToString();   // [en] Forward total / [tr] Forvet toplam
        totalPowerText.text = (CalculateTotalPower(defenseCards) +
                               CalculateTotalPower(midfieldCards) +
                               CalculateTotalPower(forwardCards)).ToString(); // [en] Total / [tr] Toplam
    }
}

