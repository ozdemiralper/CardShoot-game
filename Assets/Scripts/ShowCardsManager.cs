using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ShowCardsManager : MonoBehaviour
{
    public Transform contentPanel;  // ScrollView Content objesi
    public GameObject playerCardPrefab;
    public GameObject captainCardPrefab;
    public GameObject weatherCardPrefab;
    public TMP_Text playerCountText;
    public TMP_Text captainCountText;
    public TMP_Text weatherCountText;

    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "MyCardsScene")
        {
            ShowCards(SplashSceneManager.playerCards.GetOwnedCards());
        }
        else if(currentScene == "AllCardsScene")
        {
            ShowCards(CardDatabase.cardList);
        }
        ShowDeckSummary();
    }

    void ShowCards(List<Card> cards)
    {
        // Önce içerikleri temizle
        foreach (Transform child in contentPanel)
            Destroy(child.gameObject);

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
        List<Card> cards = null;
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "MyCardsScene")
            cards = SplashSceneManager.playerCards.GetOwnedCards();
        else if (currentScene == "AllCardsScene")
            cards = CardDatabase.cardList;
        else
            cards = new List<Card>();

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

        // UI Text'lere yazdýr
        if (playerCountText != null)
            playerCountText.text = "" + playerCount;
        if (captainCountText != null)
            captainCountText.text = "" + captainCount;
        if (weatherCountText != null)
            weatherCountText.text = "" + weatherCount;
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
