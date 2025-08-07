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
    public List<Card> extraCards = new List<Card>();

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
    public Transform coachArea;
    public Transform extraArea;

    public GameObject playerCardPrefab;
    public GameObject weatherCardPrefab;
    public GameObject captainCardPrefab;
    public GameObject coachCardPrefab;
    public GameObject extraCardPrefab;
    
    public PlayerHand playerHand;
    private HandCardCountDisplay handCardCountDisplay;

    public static CardPlayHandler Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        handCardCountDisplay = FindObjectOfType<HandCardCountDisplay>();
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
                } break;

            case CardType.Weather:
                targetArea = weatherArea;
                weatherCards.Add(card);
                break;
            case CardType.Coach:
                targetArea = coachArea;
                coachCards.Add(card);
                break;
            case CardType.Extra:
                targetArea = extraArea;
                extraCards.Add(card);
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
                case CardType.Coach:
                    prefabToUse = coachCardPrefab;
                    break;
                case CardType.Extra:
                    prefabToUse = extraCardPrefab;
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
        UpdateCardCountsUI();
        UpdatePowerTexts();
    }

    private int CalculateTotalPower(List<Card> cards)
    {
        int total = 0;
        foreach (var c in cards)
            total += c.cardPower;
        return total;
    }

    void UpdateCardCountsUI()
    {
        if (handCardCountDisplay == null)
            return;
        int playerHandCount = playerHand.GetCardCount();

        // Sahadaki kartlarý hesapla
        int playerFieldCount = defenseArea.childCount + midfieldArea.childCount + forwardArea.childCount + weatherArea.childCount;

        // Rakip el kart sayýsý ve saha kart sayýsý (örnek sabit veya kendi rakip yönetimine göre)
        int opponentHandCount = 10; // Mesela sabit, istersen gerçek rakip yönetiminden çek
        int opponentFieldCount = 5; // Örnek deðer

        handCardCountDisplay.SetPlayerCardCount(playerHandCount);
        handCardCountDisplay.SetPlayerFieldCardCount(playerFieldCount);

        handCardCountDisplay.SetOpponentCardCount(opponentHandCount);
        handCardCountDisplay.SetOpponentFieldCardCount(opponentFieldCount);
    }

    public void UpdatePowerTexts()
    {
        defensePowerText.text = CalculateTotalPower(defenseCards).ToString();
        midfieldPowerText.text = CalculateTotalPower(midfieldCards).ToString();
        forwardPowerText.text = CalculateTotalPower(forwardCards).ToString();
        totalPowerText.text = (CalculateTotalPower(defenseCards) + CalculateTotalPower(midfieldCards) + CalculateTotalPower(forwardCards)).ToString();

    }



}
