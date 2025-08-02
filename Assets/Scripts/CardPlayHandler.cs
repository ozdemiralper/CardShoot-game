using UnityEngine;

public class CardPlayHandler : MonoBehaviour
{
    public static CardPlayHandler Instance;

    public Transform defenseArea;
    public Transform midfieldArea;
    public Transform forwardArea;
    public Transform weatherArea;

    public GameObject cardPrefab;

    public PlayerHand playerHand;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PlayCard(SelectableCard selectedCard)
    {
        Card card = selectedCard.card;

        // 1. Kartý elden çýkar
        playerHand.RemoveCard(card);

        // 2. UI objesini yok et
        Destroy(selectedCard.gameObject);

        // 3. Kartý oyun alanýna koy
        Transform targetArea = null;
        switch (card.position)
        {
            case 0: targetArea = defenseArea; break;
            case 1: targetArea = midfieldArea; break;
            case 2: targetArea = forwardArea; break;
            case 5: targetArea = weatherArea; break;
            default: targetArea = midfieldArea; break;
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
            // Kart oyun alanýnda olduðu için seçim ve týklama olaylarýný devre dýþý býrakabilirsin istersen
        }
    }
}
