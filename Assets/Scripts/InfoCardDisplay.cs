using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
            Debug.LogWarning("Kart görseli bulunamadý: " + card.imagePath);

        powerText.text = card.cardPower.ToString();
        positionText.text = GetShortPositionName(card.position);
        playerNameText.text = card.cardName;
        descriptionText.text = card.cardDescription;
    }

    private string GetShortPositionName(int code)
    {
        switch (code)
        {
            case 0: return "D";
            case 1: return "M";
            case 2: return "F";
            case 3: return "T";
            case 4: return "H";
            case 5: return "E";
            default: return "?";
        }
    }
}
