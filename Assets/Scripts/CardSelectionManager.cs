using UnityEngine;

public class CardSelectionManager : MonoBehaviour
{
    public static CardSelectionManager Instance;

    private SelectableCard selectedCard = null;

    [Header("Info Card Settings")]
    public Transform infoCardPanel;        // Info kartlarýn oluþturulacaðý panel
    public GameObject infoCardPrefab;      // Info kart prefabý (normal kart prefabýnýn geliþmiþ versiyonu)

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

        GameObject infoCardObj = Instantiate(infoCardPrefab, infoCardPanel);
        InfoCardDisplay display = infoCardObj.GetComponent<InfoCardDisplay>();
        if (display != null)
        {
            // Örnek olarak oyuncu adý "Player1" olarak verdim, sen dinamik ayarlayabilirsin
            display.SetCardInfo(card.card, "Player1");
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
