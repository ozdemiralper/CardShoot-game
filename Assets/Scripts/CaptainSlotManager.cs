using UnityEngine;

public class CaptainSlotManager : MonoBehaviour
{
    public static CaptainSlotManager Instance;

    public Transform defenseCaptainSlot;
    public Transform midfieldCaptainSlot;
    public Transform forwardCaptainSlot;

    private Transform currentSlot;

    public void OnDefenseCaptainSlotClicked()
    {
        PlaceCaptainCard(defenseCaptainSlot);
    }

    public void OnMidfieldCaptainSlotClicked()
    {
        PlaceCaptainCard(midfieldCaptainSlot);
    }

    public void OnForwardCaptainSlotClicked()
    {
        PlaceCaptainCard(forwardCaptainSlot);
    }
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowCaptainSlots()
    {
        // Kaptan slotlar�n� g�r�n�r yap veya aktif et
        defenseCaptainSlot.gameObject.SetActive(true);
        midfieldCaptainSlot.gameObject.SetActive(true);
        forwardCaptainSlot.gameObject.SetActive(true);
    }

    public void HideCaptainSlots()
    {
        defenseCaptainSlot.gameObject.SetActive(false);
        midfieldCaptainSlot.gameObject.SetActive(false);
        forwardCaptainSlot.gameObject.SetActive(false);
    }

    public void PlaceCaptainCard(Transform slot)
    {
        if (CardPlayHandler.Instance.selectedCaptainCard == null)
            return;

        Card card = CardPlayHandler.Instance.selectedCaptainCard;

        if (card.isPlayed)
            return;

        // Kaptan kart prefab�n� instantiate et slotun alt�na
        GameObject cardObj = Instantiate(CardPlayHandler.Instance.captainCardPrefab, slot);

        // Kart verisini g�ster
        CardDisplay display = cardObj.GetComponent<CardDisplay>();
        if (display != null)
            display.SetCard(card);

        SelectableCard selectable = cardObj.GetComponent<SelectableCard>();
        if (selectable != null)
        {
            selectable.card = card;
            selectable.cardDisplay = display;
        }

        card.isPlayed = true;

        // PlayerHand'den kart� ��kar
        CardPlayHandler.Instance.playerHand.RemoveCard(card);

        // Se�imi s�f�rla ve slotlar� gizle
        CardPlayHandler.Instance.selectedCaptainCard = null;
        HideCaptainSlots();

        // G�� hesaplamalar� gibi ba�ka i�lemleri buradan �a��rabilirsin
        // CardPlayHandler.Instance.UpdatePowerTexts();
    }

}
