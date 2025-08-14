using UnityEngine;

public class CardSelectionManager : MonoBehaviour
{
    public static CardSelectionManager Instance; // [en] Singleton instance / [tr] Tekil örnek

    [Header("Info Card Settings")]
    public Transform infoCardPanel;               // [en] Panel to display info card / [tr] Bilgi kartýný gösterecek panel
    public GameObject infoPlayerCardPrefab;       // [en] Prefab for player info card / [tr] Oyuncu bilgi kartý prefabý
    public GameObject infoWeatherCardPrefab;      // [en] Prefab for weather info card / [tr] Hava bilgi kartý prefabý
    public GameObject infoCaptainCardPrefab;      // [en] Prefab for captain info card / [tr] Kaptan bilgi kartý prefabý

    private SelectableCard selectedCard = null;   // [en] Currently selected card / [tr] Þu an seçili kart

    void Awake()
    {
        if (Instance == null) Instance = this;   // [en] Assign singleton / [tr] Tekil örnek ata
        else Destroy(gameObject);                // [en] Destroy duplicates / [tr] Çoðaltýlmýþ objeyi sil
    }

    public void SelectCard(SelectableCard card) // [en] Called when card is selected (single click) / [tr] Kart seçildiðinde çaðrýlýr (tek týklama)
    {
        if (selectedCard != null && selectedCard != card) // [en] Deselect previous card / [tr] Önceki kartý seçili deðil yap
        {
            selectedCard.Deselect();         // [en] Deselect / [tr] Seçimi kaldýr
            ClearInfoCard();                 // [en] Clear info panel / [tr] Bilgi panelini temizle
        }

        selectedCard = card;                 // [en] Set new selected card / [tr] Yeni seçili kartý ata
        selectedCard.Select();               // [en] Mark as selected / [tr] Seçili olarak iþaretle

        CreateInfoCard(card);                // [en] Show info card / [tr] Bilgi kartýný göster
    }

    void ClearInfoCard() // [en] Clear info card panel / [tr] Bilgi kartý panelini temizle
    {
        foreach (Transform child in infoCardPanel) // [en] Destroy all children / [tr] Tüm çocuklarý sil
            Destroy(child.gameObject);
    }

    void CreateInfoCard(SelectableCard card) // [en] Instantiate info card for selected card / [tr] Seçili kart için bilgi kartýný oluþtur
    {
        ClearInfoCard(); // [en] Clear previous info / [tr] Önceki bilgiyi temizle

        GameObject prefabToInstantiate = GetInfoCardPrefab(card.card.cardType); // [en] Get proper prefab / [tr] Uygun prefabý al
        GameObject infoCardObj = Instantiate(prefabToInstantiate, infoCardPanel); // [en] Instantiate prefab / [tr] Prefabý oluþtur
        InfoCardDisplay display = infoCardObj.GetComponent<InfoCardDisplay>();    // [en] Get display component / [tr] Görünüm bileþenini al
        if (display != null)
        {
            display.SetCardInfo(card.card, card.card.cardName); // [en] Set info / [tr] Kart bilgilerini ayarla
        }
    }

    GameObject GetInfoCardPrefab(CardType type) // [en] Returns the corresponding info card prefab / [tr] Ýlgili bilgi kartý prefabýný döndür
    {
        return type switch
        {
            CardType.Player => infoPlayerCardPrefab,   // [en] Player card / [tr] Oyuncu kartý
            CardType.Weather => infoWeatherCardPrefab, // [en] Weather card / [tr] Hava kartý
            CardType.Captain => infoCaptainCardPrefab, // [en] Captain card / [tr] Kaptan kartý
            _ => infoPlayerCardPrefab                   // [en] Default / [tr] Varsayýlan
        };
    }
}
