    using UnityEngine.EventSystems;
    using UnityEngine;
using Unity.VisualScripting;

    public class SelectableCard : MonoBehaviour, IPointerClickHandler
    {
        public Card card;
        public CardDisplay cardDisplay;

        private Vector3 originalScale;

        private float lastClickTime = 0f;
        private float doubleClickThreshold = 0.3f; // 300 ms

        void Start()
        {
            originalScale = transform.localScale;
        }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (card != null && card.isPlayed)
            return; // Eðer zaten oynandýysa hiçbir þey yapma

        if (Time.time - lastClickTime < doubleClickThreshold)
        {
            // Çift týklama algýlandý
            CardPlayHandler.Instance.PlayCard(this);
        }
        else
        {
            CardSelectionManager.Instance.SelectCard(this);
        }

        lastClickTime = Time.time;
    }


    public void Select()
        {
            transform.localScale = originalScale * 1.1f;
        }

        public void Deselect()
        {
            transform.localScale = originalScale;
        }
    
    public void SetCard(Card original)
    {
        card = original.Clone(); // Her prefab için ayrý kopya
        GetComponent<CardDisplay>().SetCard(card);

    }


}
