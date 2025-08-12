using UnityEngine;

public class CardSelectionManager : MonoBehaviour
{
    public static CardSelectionManager Instance;

    [Header("Info Card Settings")]
    public Transform infoCardPanel;
    public GameObject infoPlayerCardPrefab;
    public GameObject infoWeatherCardPrefab;
    public GameObject infoCaptainCardPrefab;

    private SelectableCard selectedCard = null;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Kart seçildiðinde çaðrýlýr (tek týklama)
    public void SelectCard(SelectableCard card)
    {
        if (selectedCard != null && selectedCard != card)
        {
            selectedCard.Deselect();
            ClearInfoCard();
        }

        selectedCard = card;
        selectedCard.Select();

        CreateInfoCard(card);
    }
    void ClearInfoCard()
    {
        foreach (Transform child in infoCardPanel)
            Destroy(child.gameObject);
    }
    void CreateInfoCard(SelectableCard card)
    {
        ClearInfoCard();

        GameObject prefabToInstantiate = GetInfoCardPrefab(card.card.cardType);
        GameObject infoCardObj = Instantiate(prefabToInstantiate, infoCardPanel);
        InfoCardDisplay display = infoCardObj.GetComponent<InfoCardDisplay>();
        if (display != null)
        {
            display.SetCardInfo(card.card, card.card.cardName);
        }
    }
    GameObject GetInfoCardPrefab(CardType type)
    {
        switch (type)
        {
            case CardType.Player:
                return infoPlayerCardPrefab;
            case CardType.Weather:
                return infoWeatherCardPrefab;
            case CardType.Captain:
                return infoCaptainCardPrefab;
            default:
                return infoPlayerCardPrefab;
        }
    }

}
