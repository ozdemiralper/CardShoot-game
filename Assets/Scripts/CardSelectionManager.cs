using UnityEngine;

public class CardSelectionManager : MonoBehaviour
{
    public static CardSelectionManager Instance; // [en] Singleton instance / [tr] Tekil �rnek

    [Header("Info Card Settings")]
    public Transform infoCardPanel;               // [en] Panel to display info card / [tr] Bilgi kart�n� g�sterecek panel
    public GameObject infoPlayerCardPrefab;       // [en] Prefab for player info card / [tr] Oyuncu bilgi kart� prefab�
    public GameObject infoWeatherCardPrefab;      // [en] Prefab for weather info card / [tr] Hava bilgi kart� prefab�
    public GameObject infoCaptainCardPrefab;      // [en] Prefab for captain info card / [tr] Kaptan bilgi kart� prefab�

    private SelectableCard selectedCard = null;   // [en] Currently selected card / [tr] �u an se�ili kart

    void Awake()
    {
        if (Instance == null) Instance = this;   // [en] Assign singleton / [tr] Tekil �rnek ata
        else Destroy(gameObject);                // [en] Destroy duplicates / [tr] �o�alt�lm�� objeyi sil
    }

    public void SelectCard(SelectableCard card) // [en] Called when card is selected (single click) / [tr] Kart se�ildi�inde �a�r�l�r (tek t�klama)
    {
        if (selectedCard != null && selectedCard != card) // [en] Deselect previous card / [tr] �nceki kart� se�ili de�il yap
        {
            selectedCard.Deselect();         // [en] Deselect / [tr] Se�imi kald�r
            ClearInfoCard();                 // [en] Clear info panel / [tr] Bilgi panelini temizle
        }

        selectedCard = card;                 // [en] Set new selected card / [tr] Yeni se�ili kart� ata
        selectedCard.Select();               // [en] Mark as selected / [tr] Se�ili olarak i�aretle

        CreateInfoCard(card);                // [en] Show info card / [tr] Bilgi kart�n� g�ster
    }

    void ClearInfoCard() // [en] Clear info card panel / [tr] Bilgi kart� panelini temizle
    {
        foreach (Transform child in infoCardPanel) // [en] Destroy all children / [tr] T�m �ocuklar� sil
            Destroy(child.gameObject);
    }

    void CreateInfoCard(SelectableCard card) // [en] Instantiate info card for selected card / [tr] Se�ili kart i�in bilgi kart�n� olu�tur
    {
        ClearInfoCard(); // [en] Clear previous info / [tr] �nceki bilgiyi temizle

        GameObject prefabToInstantiate = GetInfoCardPrefab(card.card.cardType); // [en] Get proper prefab / [tr] Uygun prefab� al
        GameObject infoCardObj = Instantiate(prefabToInstantiate, infoCardPanel); // [en] Instantiate prefab / [tr] Prefab� olu�tur
        InfoCardDisplay display = infoCardObj.GetComponent<InfoCardDisplay>();    // [en] Get display component / [tr] G�r�n�m bile�enini al
        if (display != null)
        {
            display.SetCardInfo(card.card, card.card.cardName); // [en] Set info / [tr] Kart bilgilerini ayarla
        }
    }

    GameObject GetInfoCardPrefab(CardType type) // [en] Returns the corresponding info card prefab / [tr] �lgili bilgi kart� prefab�n� d�nd�r
    {
        return type switch
        {
            CardType.Player => infoPlayerCardPrefab,   // [en] Player card / [tr] Oyuncu kart�
            CardType.Weather => infoWeatherCardPrefab, // [en] Weather card / [tr] Hava kart�
            CardType.Captain => infoCaptainCardPrefab, // [en] Captain card / [tr] Kaptan kart�
            _ => infoPlayerCardPrefab                   // [en] Default / [tr] Varsay�lan
        };
    }
}
