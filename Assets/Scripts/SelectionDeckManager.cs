using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectionDeckManager : MonoBehaviour
{
    public Transform contentPanel; // ScrollView Content objesi
    public GameObject selectPlayerPrefab;
    public GameObject selectCaptainPrefab;
    public GameObject selectWeatherPrefab;
    public int totalSelectionLimit = 10;

    public TMP_Text selectedCountText;  // TMP Text component referans�
    public Button startButton;           // Ba�lat butonu referans�
    public Button cancelButton;          // �ptal butonu referans�

    private List<Card> randomCards = new List<Card>();
    private List<Card> selectedCards = new List<Card>();

    public int SelectedCardsCount => selectedCards.Count;
    void Start()
    {
        GenerateRandomCards();
        UpdateSelectedCountUI();
        startButton.interactable = false; // Ba�lang��ta kapal�
    }

    void GenerateRandomCards()
    {
        // �nce �nceki kartlar� ve listeyi temizle
        randomCards.Clear();

        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        // Kullan�c�n�n sahip oldu�u kartlar� al ve yeni listeye kopyala
        var playerDeck = new List<Card>(SplashSceneManager.playerCards.GetOwnedCards());

        // Listeyi rastgele kar��t�r (shuffle)
        playerDeck = playerDeck.OrderBy(x => Random.value).ToList();

        // �lk 15 kart� se� (e�er varsa) ve ID'ye g�re b�y�kten k����e s�rala
        int cardCount = Mathf.Min(15, playerDeck.Count);
        randomCards = playerDeck.Take(cardCount).OrderByDescending(card => card.cardID).ToList();

        // Kartlar� instantiate et
        foreach (var card in randomCards)
        {

            GameObject prefab = null;
            if (card.cardType == CardType.Player) prefab = selectPlayerPrefab;
            else if (card.cardType == CardType.Captain) prefab = selectCaptainPrefab;
            else if (card.cardType == CardType.Weather) prefab = selectWeatherPrefab;

            if (prefab != null)
            {
                var cardObj = Instantiate(prefab, contentPanel);
                var selectable = cardObj.GetComponent<SelectableCardV2>();
                if (selectable != null)
                {
                    selectable.selectionManager = this;  // Burada referans veriyoruz
                    selectable.SetCard(card);
                }
                var item = cardObj.GetComponent<CardSelectionItem>();
                item.Setup(card, this);
            }
        }
    }

    public bool CanSelectMore()
    {
        return selectedCards.Count < totalSelectionLimit;
    }

    public void ToggleSelection(Card card, bool isSelected)
    {
        if (isSelected)
        {
            if (!selectedCards.Contains(card) && CanSelectMore())
                selectedCards.Add(card);
        }
        else
        {
            selectedCards.Remove(card);
        }

        UpdateSelectedCountUI();  // Se�im de�i�ince saya� g�ncelle
        startButton.interactable = (selectedCards.Count == totalSelectionLimit); // Start buton durumu
    }

    void UpdateSelectedCountUI()
    {
        selectedCountText.text = $"Selected Cards: {selectedCards.Count} / {totalSelectionLimit}";
    }

    public void OnStartButton()
    {
        GameManager.Instance.selectedGameDeck = selectedCards;
        SceneManager.LoadScene("GameScene1");
    }
    public void OnCancelButton()
    {
        SceneManager.LoadScene("MenuScene");
    }

}
