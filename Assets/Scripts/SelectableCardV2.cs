using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableCardV2 : MonoBehaviour, IPointerClickHandler
{
    public Card card;                          // Kart verisi
    public CardDisplay cardDisplay;            // Kart görselini gösteren component
    public GameObject selectionHighlight;      // Seçim görseli (prefabda ekle, inspector’dan baðla)

    [HideInInspector]
    public SelectionDeckManager selectionManager;  // Dýþarýdan atanacak

    private Vector3 originalScale;              // Orijinal ölçek kaydý
    private bool isSelected = false;            // Seçim durumu

    void Start()
    {
        originalScale = transform.localScale;   // Orijinal ölçeði kaydet
        if (selectionHighlight != null)
            selectionHighlight.SetActive(false); // Baþlangýçta seçim gizli
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (card == null)
        {
            Debug.LogWarning("SelectableCardV2: Card verisi atanmadý!");
            return;
        }

        // Seçim limiti varsa ve yeni seçim yapmaya çalýþýyorsak engelle
        if (!isSelected && (selectionManager == null || !selectionManager.CanSelectMore()))
            return;

        ToggleSelection();
    }

    void ToggleSelection()
    {
        isSelected = !isSelected;

        if (isSelected)
            Select();
        else
            Deselect();

        // SelectionDeckManager’a durumu bildir
        selectionManager?.ToggleSelection(card, isSelected);
    }

    public void Select()
    {
        transform.localScale = originalScale * 1.1f;
        if (selectionHighlight != null)
            selectionHighlight.SetActive(true);
    }

    public void Deselect()
    {
        transform.localScale = originalScale;
        if (selectionHighlight != null)
            selectionHighlight.SetActive(false);
    }

    public void SetCard(Card cardData)
    {
        card = cardData.Clone();
        if (cardDisplay != null)
            cardDisplay.SetCard(card);
    }
}
