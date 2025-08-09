using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyDeckManager : MonoBehaviour
{
    public Transform contentPanel;  // ScrollView Content objesi
    public GameObject playerCardPrefab;
    public GameObject captainCardPrefab;
    public GameObject weatherCardPrefab;

    void Start()
    {
        ShowAllCardsInSingleGrid();
        ShowDeckSummary();
    }

    void ShowAllCardsInSingleGrid()
    {
        // Önce içerikleri temizle
        foreach (Transform child in contentPanel)
            Destroy(child.gameObject);

        List<Card> cards = SplashSceneManager.playerCards.GetOwnedCards();

        cards.Sort((a, b) => a.cardID.CompareTo(b.cardID));

        foreach (Card card in cards)
        {
            GameObject prefabToUse = GetPrefabByCardType(card.cardType);
            if (prefabToUse == null) continue;

            GameObject newCard = Instantiate(prefabToUse, contentPanel, false);
            var infoCard = newCard.GetComponent<InfoCardDisplay>();
            if (infoCard != null)
            {
                infoCard.SetCardInfo(card, card.cardName);
            }
        }
    }

    void ShowDeckSummary()
    {
        List<Card> cards = SplashSceneManager.playerCards.GetOwnedCards();

        int playerCount = 0;
        int captainCount = 0;
        int weatherCount = 0;

        foreach (var card in cards)
        {
            switch (card.cardType)
            {
                case CardType.Player: playerCount++; break;
                case CardType.Captain: captainCount++; break;
                case CardType.Weather: weatherCount++; break;
            }
        }

        Debug.Log($"Player Cards: {playerCount} | Captain Cards: {captainCount} | Weather Cards: {weatherCount}");
    }

    GameObject GetPrefabByCardType(CardType type)
    {
        switch (type)
        {
            case CardType.Player: return playerCardPrefab;
            case CardType.Captain: return captainCardPrefab;
            case CardType.Weather: return weatherCardPrefab;
            default: return null;
        }
    }

    public void OnBackButtonPressed()
    {
        SceneManager.LoadScene("MenuScene"); // Menü sahnesine döner
    }
}
