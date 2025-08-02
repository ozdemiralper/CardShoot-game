using System.Collections.Generic;
using UnityEngine;

public class CardPlayHandler : MonoBehaviour
{

    public List<Card> defenseCards = new List<Card>();
    public List<Card> midfieldCards = new List<Card>();
    public List<Card> forwardCards = new List<Card>();
    //public List<Card> weatherCards = new List<Card>();

    public TMPro.TMP_Text defensePowerText;
    public TMPro.TMP_Text midfieldPowerText;
    public TMPro.TMP_Text forwardPowerText;
    //public TMPro.TMP_Text weatherPowerText;

    public static CardPlayHandler Instance;

    public Transform defenseArea;
    public Transform midfieldArea;
    public Transform forwardArea;
    public Transform weatherArea;

    public GameObject cardPrefab;
    public Card selectedCaptainCard;        // Kartýn veri modeli (Card)
    public GameObject captainCardPrefab;    // Kaptan kart prefabý (GameObject)

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
            return; // Zaten oynandýysa iþlemi iptal et

        if (card.position == 3)  // Kaptan kart
        {
            selectedCaptainCard = card; // sadece veri modelini tutuyoruz
            CaptainSlotManager.Instance.ShowCaptainSlots(); // slotlarý aç
            return; // kaptan kartý sahaya koyma, slot seçilecek
        }
        // 1. Kartý elden çýkar
        playerHand.RemoveCard(card);

        // 2. UI objesini yok et
        Destroy(selectedCard.gameObject);

        // 3. Kartý oyun alanýna koy
        Transform targetArea = null;
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
            case 5:
                targetArea = weatherArea;
               // weatherCards.Add(card);
                break;
            default:
                targetArea = midfieldArea;
                midfieldCards.Add(card);
                break;
        }

        if (targetArea != null)
        {
            GameObject cardObj = Instantiate(cardPrefab, targetArea);
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
        card.isPlayed = true;

        UpdateCardCountsUI();

        // **Burayý ekle:**
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
        //weatherPowerText.text = CalculateTotalPower(weatherCards).ToString();

    }

}
