using System.Collections;                  // [en] For using coroutines / [tr] Coroutine kullanýmý için
using System.Collections.Generic;          // [en] For using lists / [tr] List kullanýmý için
using System.Linq;                         // [en] For LINQ operations / [tr] LINQ iþlemleri için
using TMPro;                               // [en] For TextMeshPro UI / [tr] TextMeshPro arayüzü için
using UnityEngine;                         // [en] Unity core namespace / [tr] Unity temel isim alaný
using UnityEngine.SceneManagement;         // [en] For scene management / [tr] Sahne yönetimi için
using UnityEngine.UI;                      // [en] For UI components / [tr] UI bileþenleri için

public class SelectionDeckManager : MonoBehaviour
{
    public Transform contentPanel;          // [en] ScrollView content object / [tr] ScrollView Content objesi
    public GameObject selectPlayerPrefab;   // [en] Prefab for player cards / [tr] Oyuncu kartlarý prefabý
    public GameObject selectCaptainPrefab;  // [en] Prefab for captain cards / [tr] Kaptan kartlarý prefabý
    public GameObject selectWeatherPrefab;  // [en] Prefab for weather cards / [tr] Hava durumu kartlarý prefabý
    public int totalSelectionLimit = 10;    // [en] Maximum number of selectable cards / [tr] Seçilebilecek maksimum kart sayýsý

    public TMP_Text selectedCountText;      // [en] TMP Text component reference / [tr] TMP Text bileþeni referansý
    public TMP_Text countdownText;          // [en] Countdown text / [tr] Geri sayým metni
    public Button startButton;              // [en] Start button reference / [tr] Baþlat butonu referansý
    public Button cancelButton;             // [en] Cancel button reference / [tr] Ýptal butonu referansý

    private List<Card> randomCards = new List<Card>();   // [en] List of random cards / [tr] Rastgele kart listesi
    private List<Card> selectedCards = new List<Card>(); // [en] List of selected cards / [tr] Seçilen kart listesi

    public int SelectedCardsCount => selectedCards.Count; // [en] Getter for selected cards count / [tr] Seçilen kart sayýsý getter

    private float maxTime = 10f;            // [en] Maximum countdown time / [tr] Maksimum geri sayým süresi
    private float remainingTime;            // [en] Remaining countdown time / [tr] Kalan geri sayým süresi
    private bool gameStarted = false;       // [en] Flag to check if game has started / [tr] Oyunun baþladý mý kontrolü

    void Start()
    {
        GenerateRandomCards();              // [en] Generate the random card list / [tr] Rastgele kart listesini oluþtur
        UpdateSelectedCountUI();            // [en] Update selected card count UI / [tr] Seçilen kart sayýsý UI'sini güncelle
        startButton.interactable = false;   // [en] Disable start button initially / [tr] Baþlat butonunu baþlangýçta kapat
        countdownText.text = $"{maxTime}";  // [en] Show max time / [tr] Maksimum zamaný göster

        remainingTime = maxTime;            // [en] Set remaining time / [tr] Kalan zamaný ayarla
        StartCoroutine(CountdownTimer());   // [en] Start countdown coroutine / [tr] Geri sayým coroutine'ini baþlat
         
        startButton.onClick.AddListener(OnStartButton); // [en] Add listener to start button / [tr] Baþlat butonuna listener ekle
        cancelButton.onClick.AddListener(OnCancelButton); // [en] Add listener to cancel button / [tr] Ýptal butonuna listener ekle
    }

    void GenerateRandomCards()
    {
        randomCards.Clear(); // [en] Clear random cards list / [tr] Rastgele kart listesini temizle

        foreach (Transform child in contentPanel) // [en] Clear content panel / [tr] Content panel altýndaki objeleri temizle
            Destroy(child.gameObject);

        var playerDeck = new List<Card>(SplashSceneManager.playerCards.GetOwnedCards()); // [en] Get player's owned cards / [tr] Oyuncunun sahip olduðu kartlarý al

        playerDeck = playerDeck.OrderBy(x => Random.value).ToList(); // [en] Shuffle the deck / [tr] Listeyi rastgele karýþtýr

        int cardCount = Mathf.Min(15, playerDeck.Count); // [en] Take up to 15 cards / [tr] Maksimum 15 kart seç
        randomCards = playerDeck.Take(cardCount).OrderByDescending(card => card.cardID).ToList(); // [en] Sort descending by ID / [tr] ID'ye göre büyükten küçüðe sýrala

        foreach (var card in randomCards)
        {
            GameObject prefab = null; // [en] Prefab variable / [tr] Prefab deðiþkeni
            if (card.cardType == CardType.Player) prefab = selectPlayerPrefab;       // [en] Player card prefab / [tr] Oyuncu kartý prefabý
            else if (card.cardType == CardType.Captain) prefab = selectCaptainPrefab; // [en] Captain card prefab / [tr] Kaptan kartý prefabý
            else if (card.cardType == CardType.Weather) prefab = selectWeatherPrefab; // [en] Weather card prefab / [tr] Hava durumu kartý prefabý

            if (prefab != null)
            {
                var cardObj = Instantiate(prefab, contentPanel); // [en] Instantiate prefab / [tr] Prefabý oluþtur
                var selectable = cardObj.GetComponent<SelectableCardV2>(); // [en] Get SelectableCard component / [tr] SelectableCard bileþenini al
                if (selectable != null)
                {
                    selectable.selectionManager = this; // [en] Assign selection manager / [tr] Selection manager'ý ata
                    selectable.SetCard(card);           // [en] Assign card / [tr] Kartý ata
                }
                var item = cardObj.GetComponent<CardSelectionItem>(); // [en] Get card selection item component / [tr] CardSelectionItem bileþenini al
                if (item != null) item.Setup(card, this); // [en] Setup card item / [tr] Kart objesini ayarla
            }
        }
    }

    public bool CanSelectMore()
    {
        return selectedCards.Count < totalSelectionLimit; // [en] Check if more cards can be selected / [tr] Daha fazla kart seçilebilir mi kontrol et
    }

    public void ToggleSelection(Card card, bool isSelected)
    {
        if (isSelected)
        {
            if (!selectedCards.Contains(card) && CanSelectMore()) // [en] Add card if not already selected / [tr] Kart seçili deðilse ekle
                selectedCards.Add(card);
        }
        else
        {
            selectedCards.Remove(card); // [en] Remove card from selection / [tr] Kartý seçimden çýkar
        }

        UpdateSelectedCountUI(); // [en] Update UI / [tr] UI'yý güncelle
        startButton.interactable = (selectedCards.Count == totalSelectionLimit); // [en] Enable start if limit reached / [tr] Limit dolduysa baþlat aktif
    }

    void UpdateSelectedCountUI()
    {
        selectedCountText.text = $"Selected Cards: {selectedCards.Count} / {totalSelectionLimit}"; // [en] Update selected count text / [tr] Seçilen kart sayýsýný güncelle
    }

    IEnumerator CountdownTimer()
    {
        while (remainingTime > 0 && !gameStarted) // [en] Countdown loop / [tr] Geri sayým döngüsü
        {
            countdownText.text = $"{Mathf.CeilToInt(remainingTime)}"; // [en] Update countdown text / [tr] Geri sayým metnini güncelle
            remainingTime -= Time.deltaTime;                          // [en] Decrease remaining time / [tr] Kalan zamaný azalt
            yield return null;
        }

        if (!gameStarted)
        {
            gameStarted = true; // [en] Flag game started / [tr] Oyunu baþlat
            AutoSelectCards();  // [en] Auto-select cards if player did not / [tr] Oyuncu seçmediyse kartlarý otomatik seç
        }
    }

    void AutoSelectCards()
    {
        if (selectedCards.Count < totalSelectionLimit) // [en] If not enough cards selected / [tr] Yeterli kart seçilmediyse
        {
            var sortedCards = randomCards.OrderBy(c => c.cardID).ToList(); // [en] Sort by ID ascending / [tr] ID'ye göre sýrala
            selectedCards = sortedCards.Take(totalSelectionLimit).ToList(); // [en] Take first N cards / [tr] Ýlk N kartý al
        }
        DeckManager.Instance.selectedGameDeck = selectedCards; // [en] Assign selected deck / [tr] Seçilen desteyi ata
        SceneManager.LoadScene("GameScene1");                 // [en] Load game scene / [tr] Oyun sahnesini yükle
    }

    public void OnStartButton()
    {
        if (gameStarted) return; // [en] Prevent double start / [tr] Çift baþlatmayý önle
        gameStarted = true;

        DeckManager.Instance.selectedGameDeck = selectedCards; // [en] Assign selected deck / [tr] Seçilen desteyi ata
        SceneManager.LoadScene("GameScene1");                 // [en] Load game scene / [tr] Oyun sahnesini yükle
    }

    public void OnCancelButton()
    {
        SceneManager.LoadScene("MenuScene"); // [en] Return to menu scene / [tr] Menü sahnesine dön
    }
}
