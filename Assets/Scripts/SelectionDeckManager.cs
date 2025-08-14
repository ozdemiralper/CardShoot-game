using System.Collections;                  // [en] For using coroutines / [tr] Coroutine kullan�m� i�in
using System.Collections.Generic;          // [en] For using lists / [tr] List kullan�m� i�in
using System.Linq;                         // [en] For LINQ operations / [tr] LINQ i�lemleri i�in
using TMPro;                               // [en] For TextMeshPro UI / [tr] TextMeshPro aray�z� i�in
using UnityEngine;                         // [en] Unity core namespace / [tr] Unity temel isim alan�
using UnityEngine.SceneManagement;         // [en] For scene management / [tr] Sahne y�netimi i�in
using UnityEngine.UI;                      // [en] For UI components / [tr] UI bile�enleri i�in

public class SelectionDeckManager : MonoBehaviour
{
    public Transform contentPanel;          // [en] ScrollView content object / [tr] ScrollView Content objesi
    public GameObject selectPlayerPrefab;   // [en] Prefab for player cards / [tr] Oyuncu kartlar� prefab�
    public GameObject selectCaptainPrefab;  // [en] Prefab for captain cards / [tr] Kaptan kartlar� prefab�
    public GameObject selectWeatherPrefab;  // [en] Prefab for weather cards / [tr] Hava durumu kartlar� prefab�
    public int totalSelectionLimit = 10;    // [en] Maximum number of selectable cards / [tr] Se�ilebilecek maksimum kart say�s�

    public TMP_Text selectedCountText;      // [en] TMP Text component reference / [tr] TMP Text bile�eni referans�
    public TMP_Text countdownText;          // [en] Countdown text / [tr] Geri say�m metni
    public Button startButton;              // [en] Start button reference / [tr] Ba�lat butonu referans�
    public Button cancelButton;             // [en] Cancel button reference / [tr] �ptal butonu referans�

    private List<Card> randomCards = new List<Card>();   // [en] List of random cards / [tr] Rastgele kart listesi
    private List<Card> selectedCards = new List<Card>(); // [en] List of selected cards / [tr] Se�ilen kart listesi

    public int SelectedCardsCount => selectedCards.Count; // [en] Getter for selected cards count / [tr] Se�ilen kart say�s� getter

    private float maxTime = 10f;            // [en] Maximum countdown time / [tr] Maksimum geri say�m s�resi
    private float remainingTime;            // [en] Remaining countdown time / [tr] Kalan geri say�m s�resi
    private bool gameStarted = false;       // [en] Flag to check if game has started / [tr] Oyunun ba�lad� m� kontrol�

    void Start()
    {
        GenerateRandomCards();              // [en] Generate the random card list / [tr] Rastgele kart listesini olu�tur
        UpdateSelectedCountUI();            // [en] Update selected card count UI / [tr] Se�ilen kart say�s� UI'sini g�ncelle
        startButton.interactable = false;   // [en] Disable start button initially / [tr] Ba�lat butonunu ba�lang��ta kapat
        countdownText.text = $"{maxTime}";  // [en] Show max time / [tr] Maksimum zaman� g�ster

        remainingTime = maxTime;            // [en] Set remaining time / [tr] Kalan zaman� ayarla
        StartCoroutine(CountdownTimer());   // [en] Start countdown coroutine / [tr] Geri say�m coroutine'ini ba�lat
         
        startButton.onClick.AddListener(OnStartButton); // [en] Add listener to start button / [tr] Ba�lat butonuna listener ekle
        cancelButton.onClick.AddListener(OnCancelButton); // [en] Add listener to cancel button / [tr] �ptal butonuna listener ekle
    }

    void GenerateRandomCards()
    {
        randomCards.Clear(); // [en] Clear random cards list / [tr] Rastgele kart listesini temizle

        foreach (Transform child in contentPanel) // [en] Clear content panel / [tr] Content panel alt�ndaki objeleri temizle
            Destroy(child.gameObject);

        var playerDeck = new List<Card>(SplashSceneManager.playerCards.GetOwnedCards()); // [en] Get player's owned cards / [tr] Oyuncunun sahip oldu�u kartlar� al

        playerDeck = playerDeck.OrderBy(x => Random.value).ToList(); // [en] Shuffle the deck / [tr] Listeyi rastgele kar��t�r

        int cardCount = Mathf.Min(15, playerDeck.Count); // [en] Take up to 15 cards / [tr] Maksimum 15 kart se�
        randomCards = playerDeck.Take(cardCount).OrderByDescending(card => card.cardID).ToList(); // [en] Sort descending by ID / [tr] ID'ye g�re b�y�kten k����e s�rala

        foreach (var card in randomCards)
        {
            GameObject prefab = null; // [en] Prefab variable / [tr] Prefab de�i�keni
            if (card.cardType == CardType.Player) prefab = selectPlayerPrefab;       // [en] Player card prefab / [tr] Oyuncu kart� prefab�
            else if (card.cardType == CardType.Captain) prefab = selectCaptainPrefab; // [en] Captain card prefab / [tr] Kaptan kart� prefab�
            else if (card.cardType == CardType.Weather) prefab = selectWeatherPrefab; // [en] Weather card prefab / [tr] Hava durumu kart� prefab�

            if (prefab != null)
            {
                var cardObj = Instantiate(prefab, contentPanel); // [en] Instantiate prefab / [tr] Prefab� olu�tur
                var selectable = cardObj.GetComponent<SelectableCardV2>(); // [en] Get SelectableCard component / [tr] SelectableCard bile�enini al
                if (selectable != null)
                {
                    selectable.selectionManager = this; // [en] Assign selection manager / [tr] Selection manager'� ata
                    selectable.SetCard(card);           // [en] Assign card / [tr] Kart� ata
                }
                var item = cardObj.GetComponent<CardSelectionItem>(); // [en] Get card selection item component / [tr] CardSelectionItem bile�enini al
                if (item != null) item.Setup(card, this); // [en] Setup card item / [tr] Kart objesini ayarla
            }
        }
    }

    public bool CanSelectMore()
    {
        return selectedCards.Count < totalSelectionLimit; // [en] Check if more cards can be selected / [tr] Daha fazla kart se�ilebilir mi kontrol et
    }

    public void ToggleSelection(Card card, bool isSelected)
    {
        if (isSelected)
        {
            if (!selectedCards.Contains(card) && CanSelectMore()) // [en] Add card if not already selected / [tr] Kart se�ili de�ilse ekle
                selectedCards.Add(card);
        }
        else
        {
            selectedCards.Remove(card); // [en] Remove card from selection / [tr] Kart� se�imden ��kar
        }

        UpdateSelectedCountUI(); // [en] Update UI / [tr] UI'y� g�ncelle
        startButton.interactable = (selectedCards.Count == totalSelectionLimit); // [en] Enable start if limit reached / [tr] Limit dolduysa ba�lat aktif
    }

    void UpdateSelectedCountUI()
    {
        selectedCountText.text = $"Selected Cards: {selectedCards.Count} / {totalSelectionLimit}"; // [en] Update selected count text / [tr] Se�ilen kart say�s�n� g�ncelle
    }

    IEnumerator CountdownTimer()
    {
        while (remainingTime > 0 && !gameStarted) // [en] Countdown loop / [tr] Geri say�m d�ng�s�
        {
            countdownText.text = $"{Mathf.CeilToInt(remainingTime)}"; // [en] Update countdown text / [tr] Geri say�m metnini g�ncelle
            remainingTime -= Time.deltaTime;                          // [en] Decrease remaining time / [tr] Kalan zaman� azalt
            yield return null;
        }

        if (!gameStarted)
        {
            gameStarted = true; // [en] Flag game started / [tr] Oyunu ba�lat
            AutoSelectCards();  // [en] Auto-select cards if player did not / [tr] Oyuncu se�mediyse kartlar� otomatik se�
        }
    }

    void AutoSelectCards()
    {
        if (selectedCards.Count < totalSelectionLimit) // [en] If not enough cards selected / [tr] Yeterli kart se�ilmediyse
        {
            var sortedCards = randomCards.OrderBy(c => c.cardID).ToList(); // [en] Sort by ID ascending / [tr] ID'ye g�re s�rala
            selectedCards = sortedCards.Take(totalSelectionLimit).ToList(); // [en] Take first N cards / [tr] �lk N kart� al
        }
        DeckManager.Instance.selectedGameDeck = selectedCards; // [en] Assign selected deck / [tr] Se�ilen desteyi ata
        SceneManager.LoadScene("GameScene1");                 // [en] Load game scene / [tr] Oyun sahnesini y�kle
    }

    public void OnStartButton()
    {
        if (gameStarted) return; // [en] Prevent double start / [tr] �ift ba�latmay� �nle
        gameStarted = true;

        DeckManager.Instance.selectedGameDeck = selectedCards; // [en] Assign selected deck / [tr] Se�ilen desteyi ata
        SceneManager.LoadScene("GameScene1");                 // [en] Load game scene / [tr] Oyun sahnesini y�kle
    }

    public void OnCancelButton()
    {
        SceneManager.LoadScene("MenuScene"); // [en] Return to menu scene / [tr] Men� sahnesine d�n
    }
}
