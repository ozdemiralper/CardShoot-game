using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardSelectionItem : MonoBehaviour
{
    public Image cardImage;
    public TMP_Text powerText;
    public TMP_Text positionText;
    public TMP_Text playerNameText;
    public TMP_Text descriptionText;
    public GameObject selectionHighlight;

    private Card cardData;
    private SelectionDeckManager selectionManager;
    private bool isSelected = false;

    // Kartý ve yöneticiyi ayarla, UI'yý güncelle
    public void Setup(Card card, SelectionDeckManager manager)
    {
        cardData = card;
        selectionManager = manager;

        // Kart görselini yükle
        Sprite sprite = Resources.Load<Sprite>(card.imagePath);
        if (sprite != null)
            cardImage.sprite = sprite;
        else
            Debug.LogWarning("Kart görseli bulunamadý: " + card.imagePath);

        // Tüm metinleri ve seçim vurgusunu kapat
        powerText.gameObject.SetActive(false);
        positionText.gameObject.SetActive(false);
        playerNameText.gameObject.SetActive(false);
        selectionHighlight.SetActive(false);

        // Açýklama metnini yaz
        descriptionText.text = card.cardDescription;

        // Kart tipine göre gösterilecek metinleri ayarla
        switch (card.cardType)
        {
            case CardType.Player:
                powerText.text = card.cardPower.ToString();
                powerText.gameObject.SetActive(true);

                positionText.text = GetShortPositionName(card.position);
                positionText.gameObject.SetActive(true);

                playerNameText.text = card.cardName;
                playerNameText.gameObject.SetActive(true);
                break;

            case CardType.Weather:
                playerNameText.text = GetWeatherEffectName(card.cardPower);
                playerNameText.gameObject.SetActive(true);

                positionText.text = GetShortPositionName(card.position);
                positionText.gameObject.SetActive(true);
                break;

            case CardType.Captain:
                powerText.text = "2X";
                powerText.gameObject.SetActive(true);

                positionText.text = GetShortPositionName(card.position);
                positionText.gameObject.SetActive(true);

                playerNameText.text = "CAPTAIN";
                playerNameText.gameObject.SetActive(true);
                break;

            default:
                // Diðer tiplerde sadece açýklama ve resim kalýr
                break;
        }
    }

    private string GetShortPositionName(int code)
    {
        return code switch
        {
            0 => "D", // Defans
            1 => "M", // Orta Saha
            2 => "F", // Forvet
            _ => "?"
        };
    }

    private string GetWeatherEffectName(int type)
    {
        return type switch
        {
            0 => "RAIN",
            1 => "SNOW",
            2 => "WIND",
            3 => "NORMAL",
            _ => "?"
        };
    }

    // BUTONA BAÐLANACAK FONKSÝYON: Kart týklanýnca seçim togglesi
    public void OnCardClick()
    {
        Debug.Log($"Kart týklandý: {cardData.cardName}, önceki seçili: {isSelected}");
        if (!isSelected && !selectionManager.CanSelectMore())
        {
            Debug.Log("Seçim limiti dolu!");
            return;
        }

        isSelected = !isSelected;
        selectionManager.ToggleSelection(cardData, isSelected);
        selectionHighlight.SetActive(isSelected);
        Debug.Log($"Kart seçili durumu: {isSelected}, toplam seçilen: {selectionManager.SelectedCardsCount}");

    }

}
