using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Image cardSpriteImage;
    public TMP_Text powerText;
    public TMP_Text positionText;
    public TMP_Text cardNameText;

    public void SetCard(Card card)
    {
        // Kart g�rselini y�kle
        if (cardSpriteImage != null)
        {
            Sprite sprite = Resources.Load<Sprite>(card.imagePath);
            if (sprite != null)
                cardSpriteImage.sprite = sprite;
            else
                Debug.LogWarning("Kart g�rseli bulunamad�: " + card.imagePath);
        }

        // T�m alanlar� �nce temizle
        if (powerText != null) powerText.text = "";
        if (positionText != null) positionText.text = "";
        if (cardNameText != null) cardNameText.text = "";

        // Kart tipine g�re g�sterilecek alanlar
        switch (card.cardType)
        {
            case CardType.Player:
                if (powerText != null) powerText.text = card.cardPower.ToString();
                if (positionText != null) positionText.text = GetShortPositionName(card.position);
                break;

            case CardType.Weather:
                if (cardNameText != null) cardNameText.text = GetWeatherEffectName(card.cardPower);
                if (positionText != null) positionText.text = GetShortPositionName(card.position);
                break;

            case CardType.Captain:
                if (positionText != null) positionText.text = GetShortPositionName(card.position);
                break;

                // Di�er kart t�rlerinde �imdilik hi�bir alan g�sterilmiyor
        }
    }

    private string GetShortPositionName(int code)
    {
        switch (code)
        {
            case 0: return "D"; // Defans
            case 1: return "M"; // Orta Saha
            case 2: return "F"; // Forvet
            default: return "";
        }
    }

    private string GetWeatherEffectName(int type)
    {
        switch (type)
        {
            case 0: return "RAIN";
            case 1: return "SNOW";
            case 2: return "WIND";
            default: return "Hava";
        }
    }
}
