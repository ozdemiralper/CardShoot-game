using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Image cardSpriteImage;  // CardSprite objesindeki Image component
    public TMP_Text powerText;
    public TMP_Text positionText;

    public void SetCard(Card card)
    {
        Sprite sprite = Resources.Load<Sprite>(card.imagePath);
        if (sprite != null)
            cardSpriteImage.sprite = sprite;
        else
            Debug.LogWarning("Kart görseli bulunamadý: " + card.imagePath);

        // Gücü sadece sayý olarak göster
        powerText.text = card.cardPower.ToString();

        // Pozisyonu kýsaltýlmýþ haliyle göster
        positionText.text = GetShortPositionName(card.position);
    }

    private string GetShortPositionName(int code)
    {
        switch (code)
        {
            case 0: return "D"; // Defans
            case 1: return "M"; // Orta Saha (Midfielder)
            case 2: return "F"; // Forvet (Forward)
            case 3: return "T"; // Teknik Adam
            case 4: return "H"; // Extra
            case 5: return "E"; // Hava (Weather)
            default: return "?";
        }
    }
}
