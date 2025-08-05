using UnityEngine;

public class CupAreaButtons : MonoBehaviour
{
    public Card.CupPosition cupPosition;  // Burada Card i�indeki enumu kulland�k

    public void OnButtonClick()
    {
        SelectableCard selectedCard = CardSelectionManager.Instance.GetSelectedCard();

        if (selectedCard != null && selectedCard.card.cardType == CardType.Cup)
        {
            CardPlayHandler.Instance.PlaceCupCard(selectedCard.card, cupPosition);
            CardSelectionManager.Instance.DeselectAll();
        }
    }
}
