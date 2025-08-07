using UnityEngine.EventSystems;  // For detecting pointer (mouse/touch) events / ��aret�i (fare/dokunma) olaylar�n� alg�lamak i�in
using UnityEngine;               // Unity core engine namespace / Unity �ekirdek motor isim alan�

public class SelectableCard : MonoBehaviour, IPointerClickHandler  // Handles card selection and double-click events / Kart se�imi ve �ift t�klama olaylar�n� y�netir
{
    public Card card;          
    public CardDisplay cardDisplay;  // Component to display card visuals / Kart g�rsellerini g�steren bile�en

    private Vector3 originalScale;  // Store original scale for scaling effect / �l�ek efektinde orijinal boyutu saklar

    private float lastClickTime = 0f;          // Time of last click / Son t�klaman�n zaman�
    private float doubleClickThreshold = 0.3f; // Max interval between clicks for double click / �ift t�klama i�in maksimum t�klama aral���

    void Start()
    {
        originalScale = transform.localScale;  // Save original scale at start / Ba�lang��ta orijinal �l�e�i kaydet
    }

    public void OnPointerClick(PointerEventData eventData)  // Called on pointer click / ��aret�i t�klamas� oldu�unda �a�r�l�r
    {
        if (card != null && card.isPlayed)  // Ignore clicks if card already played / Kart oynand�ysa t�klamay� yok say
            return;

        if (Time.time - lastClickTime < doubleClickThreshold)  // Check for double click / �ift t�klama kontrol�
        {
            CardPlayHandler.Instance.PlayCard(this);  // Play the card on double click / �ift t�klamada kart� oynat
        }
        else
        {
            CardSelectionManager.Instance.SelectCard(this);  // Single click selects card / Tek t�klama kart� se�er
        }
        lastClickTime = Time.time;  // Update last click time / Son t�klama zaman�n� g�ncelle
    }

    public void Select()  // Visual effect for selection / Se�im i�in g�rsel efekt
    {
        transform.localScale = originalScale * 1.1f;  // Slightly enlarge card / Kart� biraz b�y�t
    }

    public void Deselect()  // Visual effect for deselection / Se�im kald�rma efekti
    {
        transform.localScale = originalScale;  // Reset to original size / Orijinal boyuta d�nd�r
    }

    public void SetCard(Card original)  // Assign card data and update display / Kart verisini ata ve g�rseli g�ncelle
    {
        card = original.Clone();                         // Clone card to avoid reference issues / Referans problemlerini �nlemek i�in kopyala
        GetComponent<CardDisplay>().SetCard(card);  
    }
}
