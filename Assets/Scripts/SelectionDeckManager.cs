using System.Collections;
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
    public TMP_Text countdownText;      // Geri say�m metni
    public Button startButton;           // Ba�lat butonu referans�
    public Button cancelButton;          // �ptal butonu referans�

    private List<Card> randomCards = new List<Card>();
    private List<Card> selectedCards = new List<Card>();

    public int SelectedCardsCount => selectedCards.Count;

    private float maxTime = 10f;
    private float remainingTime;
    private bool gameStarted = false;

    void Start()
    {
        GenerateRandomCards();
        UpdateSelectedCountUI();
        startButton.interactable = false; // Ba�lang��ta kapal�
        countdownText.text = $"{maxTime}";

        remainingTime = maxTime;
        StartCoroutine(CountdownTimer());

        startButton.onClick.AddListener(OnStartButton);
        cancelButton.onClick.AddListener(OnCancelButton);
    }

    void GenerateRandomCards()
    {
        randomCards.Clear();

        foreach (Transform child in contentPanel)
            Destroy(child.gameObject);

        var playerDeck = new List<Card>(SplashSceneManager.playerCards.GetOwnedCards());

        // Listeyi rastgele kar��t�r
        playerDeck = playerDeck.OrderBy(x => Random.value).ToList();

        // �lk 15 kart� se� ve ID'ye g�re b�y�kten k����e s�rala
        int cardCount = Mathf.Min(15, playerDeck.Count);
        randomCards = playerDeck.Take(cardCount).OrderByDescending(card => card.cardID).ToList();

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
                    selectable.selectionManager = this;
                    selectable.SetCard(card);
                }
                var item = cardObj.GetComponent<CardSelectionItem>();
                if (item != null) item.Setup(card, this);
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

        UpdateSelectedCountUI();
        startButton.interactable = (selectedCards.Count == totalSelectionLimit);
    }

    void UpdateSelectedCountUI()
    {
        selectedCountText.text = $"Selected Cards: {selectedCards.Count} / {totalSelectionLimit}";
    }

    IEnumerator CountdownTimer()
    {
        while (remainingTime > 0 && !gameStarted)
        {
            countdownText.text = $"{Mathf.CeilToInt(remainingTime)}";
            remainingTime -= Time.deltaTime;
            yield return null;
        }

        if (!gameStarted)
        {
            gameStarted = true;
            AutoSelectCards();
        }
    }

    void AutoSelectCards()
    {
        // E�er oyuncu kart se�mediyse, en k���k ID�li son 10 kart� se�
        if (selectedCards.Count < totalSelectionLimit)
        {
            var sortedCards = randomCards.OrderBy(c => c.cardID).ToList();
            selectedCards = sortedCards.Take(totalSelectionLimit).ToList();
        }
        GameManager.Instance.selectedGameDeck = selectedCards;
        SceneManager.LoadScene("GameScene1");
    }

    public void OnStartButton()
    {
        if (gameStarted) return;
        gameStarted = true;

        GameManager.Instance.selectedGameDeck = selectedCards;
        SceneManager.LoadScene("GameScene1");
    }

    public void OnCancelButton()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
