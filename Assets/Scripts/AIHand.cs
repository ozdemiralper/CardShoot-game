using System.Collections.Generic;
using UnityEngine;

public class AIHand : MonoBehaviour
{
    public List<Card> cardsInHand = new List<Card>(); // [en] AI's cards in hand / [tr] AI'nin elindeki kartlar

    void Start()
    {
        cardsInHand = new List<Card>(DeckManager.Instance.aiDeck); // [en] Initialize AI hand from deck manager at game start / [tr] Oyun başında AI elini DeckManager üzerinden başlat
        Debug.Log("AI elindeki kart sayısı: " + cardsInHand.Count); // [en] Log hand count / [tr] Elindeki kart sayısını yazdır
    }

    public void InitAIHand()
    {
        cardsInHand = new List<Card>(DeckManager.Instance.aiDeck); // [en] Initialize or reset AI hand / [tr] AI elini başlat veya sıfırla
        Debug.Log("AI elindeki kart sayısı: " + cardsInHand.Count); // [en] Log hand count / [tr] Elindeki kart sayısını yazdır
    }

    public void PlayTurn()
    {
        if (cardsInHand.Count == 0) // [en] If no cards left, pass turn to player / [tr] Kart kalmadıysa turu oyuncuya geçir
        {
            Debug.Log("AI'nin kartı kalmadı! Player sıraya geçiyor."); // [en] Log no cards left / [tr] Kart kalmadığını yazdır
            GameManager.Instance.currentTurn = GameTurn.Player; // [en] Switch turn to player / [tr] Sırayı oyuncuya geçir
            GameManager.Instance.StartTurn(); // [en] Start player turn / [tr] Oyuncu sırasını başlat
            return;
        }

        Card cardToPlay = cardsInHand[Random.Range(0, cardsInHand.Count)]; // [en] Select a random card from hand / [tr] Elden rastgele bir kart seç

        AIPlayHandler.Instance.PlayCard(cardToPlay); // [en] Play card via AIPlayHandler / [tr] Kartı AIPlayHandler üzerinden sahaya yerleştir

        cardsInHand.Remove(cardToPlay); // [en] Remove card from hand / [tr] Kartı elden çıkar

        if (cardsInHand.Count > 0) GameManager.Instance.EndTurn(); // [en] End AI turn / [tr] AI turunu bitir
        else
        {
            GameManager.Instance.currentTurn = GameTurn.Player; // [en] Switch turn to player / [tr] Sırayı oyuncuya geçir
            GameManager.Instance.StartTurn(); // [en] Start player turn / [tr] Oyuncu sırasını başlat
        }
    }
}
