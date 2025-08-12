using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardPlayHandler : MonoBehaviour
{
    public List<Card> defenseCards = new List<Card>();
    public List<Card> midfieldCards = new List<Card>();
    public List<Card> forwardCards = new List<Card>();
    public List<Card> weatherCards = new List<Card>();
    public List<Card> captainCards = new List<Card>(); 
    public List<Card> captainDefenseCards = new List<Card>();
    public List<Card> captainMidfieldCards = new List<Card>();
    public List<Card> captainForwardCards = new List<Card>();
    
    public List<Card> coachCards = new List<Card>();

    public TMPro.TMP_Text defensePowerText;
    public TMPro.TMP_Text midfieldPowerText;
    public TMPro.TMP_Text forwardPowerText;
    public TMPro.TMP_Text totalPowerText;

    public Transform defenseArea;
    public Transform midfieldArea;
    public Transform forwardArea;

    public Transform defenseCaptainArea;
    public Transform midfieldCaptainArea;
    public Transform forwardCaptainArea;

    public Transform weatherArea;

    public GameObject playerCardPrefab;
    public GameObject weatherCardPrefab;
    public GameObject captainCardPrefab;
    
    public PlayerHand playerHand;

    public static CardPlayHandler Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void PlayCard(SelectableCard selectedCard)
    {
        Card card = selectedCard.card;
        if (card.isPlayed)
            return;

        if (card.cardType == CardType.Weather)
        {
            bool sameWeatherExists = weatherCards.Any(c => c.cardPower == card.cardPower);

            if (sameWeatherExists)
            {
                Debug.Log("Bu hava türü (RAIN, SNOW, WIND) zaten oyunda. Tekrar oynanamaz.");
                return;
            }
        }
        if (card.cardType == CardType.Captain)
        {
            bool sameCaptainExists = captainCards.Any(c => c.cardPower == card.cardPower);

            if (sameCaptainExists)
            {
                Debug.Log("Bu kaptan türü zaten oyunda. Tekrar oynanamaz.");
                return;
            }
        }

        Destroy(selectedCard.gameObject);
        playerHand.RemoveCard(card);
        card.isPlayed = true;
        Transform targetArea = null;
        switch (card.cardType)
        {
            case CardType.Player:
                switch (card.position)
                {
                    case 0:
                        targetArea = defenseArea;
                        defenseCards.Add(card);
                        break;
                    case 1:
                        targetArea = midfieldArea;
                        midfieldCards.Add(card);
                        break;
                    case 2:
                        targetArea = forwardArea;
                        forwardCards.Add(card);
                        break;
                }
                UpdateAllPlayerCardPowers();
                break;
            
            case CardType.Captain:
                switch (card.cardPower)
                {
                    case 0:
                        targetArea = defenseCaptainArea;
                        captainDefenseCards.Add(card);
                        captainCards.Add(card);
                        break;
                    case 1:
                        targetArea = midfieldCaptainArea;
                        captainMidfieldCards.Add(card);
                        captainCards.Add(card);
                        break;
                    case 2:
                        targetArea = forwardCaptainArea;
                        captainForwardCards.Add(card);
                        captainCards.Add(card);
                        break;
                }
                break;
            case CardType.Weather:
                targetArea = weatherArea;
                weatherCards.Add(card);
                break;
            default:
                Debug.LogWarning("Bilinmeyen kart tipi: " + card.cardType);
                break;
        }

        if (targetArea != null)
        {
            GameObject prefabToUse = null;

            switch (card.cardType)
            {
                case CardType.Player:
                    prefabToUse = playerCardPrefab;
                    break;
                case CardType.Weather:
                    prefabToUse = weatherCardPrefab;
                    break;
                case CardType.Captain:
                    prefabToUse = captainCardPrefab;
                    break;
                default:
                    Debug.LogWarning("Bilinmeyen kart tipi, player prefabý kullanýlýyor.");
                    prefabToUse = playerCardPrefab;
                    break;
            }

            if (prefabToUse == null)
            {
                Debug.LogError("Prefab atanmadý! Kart tipi: " + card.cardType);
                return;
            }

            GameObject cardObj = Instantiate(prefabToUse, targetArea);
            CardDisplay display = cardObj.GetComponent<CardDisplay>();
            if (display != null)
                display.SetCard(card);

            SelectableCard selectable = cardObj.GetComponent<SelectableCard>();
            if (selectable != null)
            {
                selectable.card = card;
                selectable.cardDisplay = display;
            }
        }
        UpdateAllPlayerCardPowers();
        UpdatePowerTexts();
    }
    private void UpdateAllPlayerCardPowers()
    {
        List<Card> allPlayerCards = new List<Card>();
        allPlayerCards.AddRange(defenseCards);
        allPlayerCards.AddRange(midfieldCards);
        allPlayerCards.AddRange(forwardCards);

        // 1. Orijinal güce sýfýrla
        foreach (var card in allPlayerCards)
        {
            card.cardPower = card.originalPower;
        }

        // 2. Duplicate kartlarý 2x yap
        Dictionary<int, int> cardIDCounts = new Dictionary<int, int>();
        foreach (var card in allPlayerCards)
        {
            if (!cardIDCounts.ContainsKey(card.cardID))
                cardIDCounts[card.cardID] = 0;
            cardIDCounts[card.cardID]++;
        }
        foreach (var card in allPlayerCards)
        {
            if (cardIDCounts[card.cardID] == 2)
                card.cardPower = card.originalPower * 2;
        }

        // 3. Hava etkisini uygula
        foreach (var weatherCard in weatherCards)
        {
            var affectedCards = allPlayerCards.Where(c => c.position == weatherCard.position && c.cardType == CardType.Player).ToList();
            foreach (var c in affectedCards)
                c.cardPower = 1;
        }

        // 4. Kaptan etkisini uygula
        foreach (var captainCard in captainCards)
        {
            var affectedCards = allPlayerCards.Where(c => c.position == captainCard.position && c.cardType == CardType.Player).ToList();
            foreach (var c in affectedCards)
                c.cardPower *= 2;
        }

        // 5. Görselleri güncelle
        UpdateCardVisualsInArea(0);
        UpdateCardVisualsInArea(1);
        UpdateCardVisualsInArea(2);
    }
    private void UpdateCardVisualsInArea(int position)
    {
        Transform area = null;

        switch (position)
        {
            case 0: area = defenseArea; break;
            case 1: area = midfieldArea; break;
            case 2: area = forwardArea; break;
            default: return;
        }

        foreach (Transform child in area)
        {
            CardDisplay display = child.GetComponent<CardDisplay>();
            SelectableCard selectable = child.GetComponent<SelectableCard>();
            if (display != null && selectable != null)
            {
                display.SetCard(selectable.card);
            }
        }
    }
    private int CalculateTotalPower(List<Card> cards)
    {
        int total = 0;
        foreach (var c in cards)
            total += c.cardPower;
        return total;
    }
    public void UpdatePowerTexts()
    {
        defensePowerText.text = CalculateTotalPower(defenseCards).ToString();
        midfieldPowerText.text = CalculateTotalPower(midfieldCards).ToString();
        forwardPowerText.text = CalculateTotalPower(forwardCards).ToString();
        totalPowerText.text = (CalculateTotalPower(defenseCards) + CalculateTotalPower(midfieldCards) + CalculateTotalPower(forwardCards)).ToString();

    }
}
