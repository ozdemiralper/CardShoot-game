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

    // Kart� ve y�neticiyi ayarla, UI'y� g�ncelle
    public void Setup(Card card, SelectionDeckManager manager)
    {
        cardData = card;
        selectionManager = manager;

        // Kart g�rselini y�kle
        Sprite sprite = Resources.Load<Sprite>(card.imagePath);
        if (sprite != null)
            cardImage.sprite = sprite;
        else
            Debug.LogWarning("Kart g�rseli bulunamad�: " + card.imagePath);

        // T�m metinleri ve se�im vurgusunu kapat
        powerText.gameObject.SetActive(false);
        positionText.gameObject.SetActive(false);
        playerNameText.gameObject.SetActive(false);
        selectionHighlight.SetActive(false);

        // A��klama metnini yaz
        descriptionText.text = card.cardDescription;

        // Kart tipine g�re g�sterilecek metinleri ayarla
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
                // Di�er tiplerde sadece a��klama ve resim kal�r
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

    // BUTONA BA�LANACAK FONKS�YON: Kart t�klan�nca se�im togglesi
    public void OnCardClick()
    {
        Debug.Log($"Kart t�kland�: {cardData.cardName}, �nceki se�ili: {isSelected}");
        if (!isSelected && !selectionManager.CanSelectMore())
        {
            Debug.Log("Se�im limiti dolu!");
            return;
        }

        isSelected = !isSelected;
        selectionManager.ToggleSelection(cardData, isSelected);
        selectionHighlight.SetActive(isSelected);
        Debug.Log($"Kart se�ili durumu: {isSelected}, toplam se�ilen: {selectionManager.SelectedCardsCount}");

    }

}
