    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class PlayerHand : MonoBehaviour
    {
        public Transform handPanel;            // Kartlarýn gösterileceði UI paneli
        public GameObject cardPrefab;          // Sadece Image içeren prefab
        private List<Card> cardsInHand = new List<Card>();

        public void AddCard(Card card)
        {
            cardsInHand.Add(card);

        }
    public void RemoveCard(Card card)
    {
        if (cardsInHand.Contains(card))
            cardsInHand.Remove(card);
    }

    public void ClearHand()
        {
            cardsInHand.Clear();

            foreach (Transform child in handPanel)
                Destroy(child.gameObject);
        }

        public int GetCardCount()
        {
            return cardsInHand.Count;
        }
    }
