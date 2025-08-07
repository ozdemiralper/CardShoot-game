using UnityEngine.EventSystems;  // For detecting pointer (mouse/touch) events / Ýþaretçi (fare/dokunma) olaylarýný algýlamak için
using UnityEngine;               // Unity core engine namespace / Unity çekirdek motor isim alaný

public class SelectableCard : MonoBehaviour, IPointerClickHandler  // Handles card selection and double-click events / Kart seçimi ve çift týklama olaylarýný yönetir
{
    public Card card;          
    public CardDisplay cardDisplay;  // Component to display card visuals / Kart görsellerini gösteren bileþen

    private Vector3 originalScale;  // Store original scale for scaling effect / Ölçek efektinde orijinal boyutu saklar

    private float lastClickTime = 0f;          // Time of last click / Son týklamanýn zamaný
    private float doubleClickThreshold = 0.3f; // Max interval between clicks for double click / Çift týklama için maksimum týklama aralýðý

    void Start()
    {
        originalScale = transform.localScale;  // Save original scale at start / Baþlangýçta orijinal ölçeði kaydet
    }

    public void OnPointerClick(PointerEventData eventData)  // Called on pointer click / Ýþaretçi týklamasý olduðunda çaðrýlýr
    {
        if (card != null && card.isPlayed)  // Ignore clicks if card already played / Kart oynandýysa týklamayý yok say
            return;

        if (Time.time - lastClickTime < doubleClickThreshold)  // Check for double click / Çift týklama kontrolü
        {
            CardPlayHandler.Instance.PlayCard(this);  // Play the card on double click / Çift týklamada kartý oynat
        }
        else
        {
            CardSelectionManager.Instance.SelectCard(this);  // Single click selects card / Tek týklama kartý seçer
        }
        lastClickTime = Time.time;  // Update last click time / Son týklama zamanýný güncelle
    }

    public void Select()  // Visual effect for selection / Seçim için görsel efekt
    {
        transform.localScale = originalScale * 1.1f;  // Slightly enlarge card / Kartý biraz büyüt
    }

    public void Deselect()  // Visual effect for deselection / Seçim kaldýrma efekti
    {
        transform.localScale = originalScale;  // Reset to original size / Orijinal boyuta döndür
    }

    public void SetCard(Card original)  // Assign card data and update display / Kart verisini ata ve görseli güncelle
    {
        card = original.Clone();                         // Clone card to avoid reference issues / Referans problemlerini önlemek için kopyala
        GetComponent<CardDisplay>().SetCard(card);  
    }
}
