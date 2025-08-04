using UnityEngine;

public class CardSelectionManager : MonoBehaviour
{
    public static CardSelectionManager Instance;

    private SelectableCard selectedCard = null;

    [Header("Info Card Settings")]
    public Transform infoCardPanel;        // Info kartlar�n olu�turulaca�� panel
    public GameObject infoPlayerCardPrefab;      // Info kart prefab� (normal kart prefab�n�n geli�mi� versiyonu)
    public GameObject infoWeatherCardPrefab;      // Info kart prefab� (normal kart prefab�n�n geli�mi� versiyonu)
    public GameObject infoCupCardPrefab;      // Info kart prefab� (normal kart prefab�n�n geli�mi� versiyonu)
    public GameObject infoCoachCardPrefab;      // Info kart prefab� (normal kart prefab�n�n geli�mi� versiyonu)
    public GameObject infoTrophyCardPrefab;      // Info kart prefab� (normal kart prefab�n�n geli�mi� versiyonu)
    public GameObject infoExtraCardPrefab;      // Info kart prefab� (normal kart prefab�n�n geli�mi� versiyonu)

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

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
            case CardType.Cup:
                return infoCupCardPrefab;
            case CardType.Coach:
                return infoCoachCardPrefab;
            case CardType.Trophy:
                return infoTrophyCardPrefab;
            case CardType.Extra:
                return infoExtraCardPrefab;
            default:
                return infoPlayerCardPrefab; // default fallback
        }
    }


    public void DeselectAll()
    {
        if (selectedCard != null)
        {
            selectedCard.Deselect();
            selectedCard = null;
            ClearInfoCard();
        }
    }
}
