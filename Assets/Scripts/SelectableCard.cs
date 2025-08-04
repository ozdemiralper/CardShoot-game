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
            return; // E�er zaten oynand�ysa hi�bir �ey yapma

        if (Time.time - lastClickTime < doubleClickThreshold)
        {
            // �ift t�klama alg�land�
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
        card = original.Clone(); // Her prefab i�in ayr� kopya
        GetComponent<CardDisplay>().SetCard(card);

    }


}
