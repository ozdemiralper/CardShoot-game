using System.Collections.Generic;
using UnityEngine;

public class CardPlayHandler : MonoBehaviour
{

    public List<Card> defenseCards = new List<Card>();
    public List<Card> midfieldCards = new List<Card>();
    public List<Card> forwardCards = new List<Card>();
    public List<Card> weatherCards = new List<Card>();
    public List<Card> cupCards = new List<Card>();
    public List<Card> coachCards = new List<Card>();
    public List<Card> extraCards = new List<Card>();

    public TMPro.TMP_Text defensePowerText;
    public TMPro.TMP_Text midfieldPowerText;
    public TMPro.TMP_Text forwardPowerText;
    public TMPro.TMP_Text totalPowerText;

    public static CardPlayHandler Instance;

    public Transform defenseArea;
    public Transform midfieldArea;
    public Transform forwardArea;
    public Transform weatherArea;
    public Transform cupArea;
    public Transform coachArea;
    public Transform extraArea;

    public GameObject playerCardPrefab;
    public GameObject weatherCardPrefab;
    public GameObject cupCardPrefab;
    public GameObject coachCardPrefab;
    public GameObject extraCardPrefab;
    

    public PlayerHand playerHand;
    private HandCardCountDisplay handCardCountDisplay;

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
            return; // Zaten oynand�ysa i�lemi iptal et

        // Weather kartlar� i�in �zel kontrol
        bool alreadyExists = weatherCards.Exists(c => c.cardPower == card.cardPower);
        if (alreadyExists)
        {
            Debug.Log("Ayn� t�rden ve g��te Weather kart� zaten var, oynanamaz.");
            return; // Kart�n tekrar oynanmas�n� engelle
        }

        // 1. Kart� elden ��kar
        playerHand.RemoveCard(card);

        // 2. UI objesini yok et
        Destroy(selectedCard.gameObject);

        // 3. Kart� oyun alan�na koy
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
            case CardType.Weather:
                targetArea = weatherArea;
                weatherCards.Add(card);
                break;
            case CardType.Cup:
                targetArea = cupArea;
                cupCards.Add(card);
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
                case CardType.Cup:
                    prefabToUse = cupCardPrefab;
                    break;
                case CardType.Coach:
                    prefabToUse = coachCardPrefab;
                    break;
                case CardType.Extra:
                    prefabToUse = extraCardPrefab;
                    break;
                default:
                    Debug.LogWarning("Bilinmeyen kart tipi, player prefab� kullan�l�yor.");
                    prefabToUse = playerCardPrefab;
                    break;
            }

            if (prefabToUse == null)
            {
                Debug.LogError("Prefab atanmad�! Kart tipi: " + card.cardType);
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

        // Sahadaki kartlar� hesapla
        int playerFieldCount = defenseArea.childCount + midfieldArea.childCount + forwardArea.childCount + weatherArea.childCount;

        // Rakip el kart say�s� ve saha kart say�s� (�rnek sabit veya kendi rakip y�netimine g�re)
        int opponentHandCount = 10; // Mesela sabit, istersen ger�ek rakip y�netiminden �ek
        int opponentFieldCount = 5; // �rnek de�er

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
