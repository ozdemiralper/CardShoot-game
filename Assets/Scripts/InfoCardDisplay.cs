using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class InfoCardDisplay : MonoBehaviour
{
    public Image cardSpriteImage;
    public TMP_Text powerText;
    public TMP_Text positionText;
    public TMP_Text playerNameText;
    public TMP_Text descriptionText;

    public void SetCardInfo(Card card, string playerName)
    {
        Sprite sprite = Resources.Load<Sprite>(card.imagePath);
        if (sprite != null)
            cardSpriteImage.sprite = sprite;
        else
            Debug.LogWarning("Kart g�rseli bulunamad�: " + card.imagePath);

        // Ba�ta hepsini kapatal�m
        powerText.gameObject.SetActive(false);
        positionText.gameObject.SetActive(false);
        playerNameText.gameObject.SetActive(false);

        descriptionText.text = card.cardDescription;

        switch (card.cardType)
        {
            case CardType.Player:
                powerText.text = card.cardPower.ToString();
                powerText.gameObject.SetActive(true);

                positionText.text = GetShortPositionName(card.position);
                positionText.gameObject.SetActive(true);

                playerNameText.text = playerName;
                playerNameText.gameObject.SetActive(true);
                break;

            case CardType.Weather:
                playerNameText.text = GetWeatherEffectName(card.cardPower); // "RAIN", "SNOW", vs.
                playerNameText.gameObject.SetActive(true);

                positionText.text = GetShortPositionName(card.position); // Etki alan� olarak g�steriyoruz
                positionText.gameObject.SetActive(true);

                // �sim g�sterilmeyecek
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
                // Sadece a��klama ve resim g�sterilir
                break;
        }
    }

    private string GetShortPositionName(int code)
    {
        return code switch
        {
            0 => "D",
            1 => "M",
            2 => "F",
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
}
