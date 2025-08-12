using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableCardV2 : MonoBehaviour, IPointerClickHandler
{
    public Card card;                          // Kart verisi
    public CardDisplay cardDisplay;            // Kart g�rselini g�steren component
    public GameObject selectionHighlight;      // Se�im g�rseli (prefabda ekle, inspector�dan ba�la)

    [HideInInspector]
    public SelectionDeckManager selectionManager;  // D��ar�dan atanacak

    private Vector3 originalScale;              // Orijinal �l�ek kayd�
    private bool isSelected = false;            // Se�im durumu

    void Start()
    {
        originalScale = transform.localScale;   // Orijinal �l�e�i kaydet
        if (selectionHighlight != null)
            selectionHighlight.SetActive(false); // Ba�lang��ta se�im gizli
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (card == null)
        {
            Debug.LogWarning("SelectableCardV2: Card verisi atanmad�!");
            return;
        }

        // Se�im limiti varsa ve yeni se�im yapmaya �al���yorsak engelle
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

        // SelectionDeckManager�a durumu bildir
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
