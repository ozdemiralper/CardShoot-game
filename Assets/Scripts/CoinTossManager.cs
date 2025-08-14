using UnityEngine;                 // [en] Unity core namespace / [tr] Unity temel isim alaný
using UnityEngine.UI;              // [en] For UI components / [tr] UI bileþenleri için
using System.Collections;          // [en] For coroutines / [tr] Coroutine kullanýmý için
using TMPro;                       // [en] For TextMeshPro UI / [tr] TextMeshPro arayüzü için

public enum CoinChoice
{
    Goal, // [en] Goal / [tr] Kale
    Ball  // [en] Ball / [tr] Top
}

public enum Turn
{
    Player, // [en] Player turn / [tr] Oyuncu sýrasý
    AI      // [en] AI turn / [tr] AI sýrasý
}

public class CoinTossManager : MonoBehaviour
{
    public static CoinTossManager Instance; // [en] Singleton instance / [tr] Singleton instance

    [Header("UI Elements")]
    public GameObject coinTossPanel;    // [en] Panel containing coin toss UI / [tr] Yazý-tura paneli
    public Button btnGoal;              // [en] Button for choosing Goal / [tr] Kale seçme butonu
    public Button btnBall;              // [en] Button for choosing Ball / [tr] Top seçme butonu
    public Button btnFlip;              // [en] Button for flipping coin / [tr] Yazý-tura at butonu
    public Button btnStart;             // [en] Start game button / [tr] Baþlat butonu
    public Image coinImage;             // [en] Image of the coin / [tr] Yazý-tura görseli
    public Sprite goalSprite;           // [en] Sprite for Goal / [tr] Kale görseli
    public Sprite ballSprite;           // [en] Sprite for Ball / [tr] Top görseli
    public TMP_Text resultText;         // [en] Text for displaying result / [tr] Sonuç metni
    public TMP_Text countdownText;      // [en] Countdown display text / [tr] Geri sayým metni

    [Header("Button Scale Settings")]
    public float selectedScale = 1.2f; // [en] Scale when button is selected / [tr] Seçili buton boyutu
    private Vector3 defaultScale = Vector3.one; // [en] Default button scale / [tr] Buton varsayýlan boyutu

    [HideInInspector] public CoinChoice playerChoice; // [en] Player's choice / [tr] Oyuncu seçimi
    private Button selectedButton;                     // [en] Currently selected button / [tr] Seçili buton
    public Turn firstTurn;                             // [en] Who starts first / [tr] Ýlk sýrasý kim alacak

    private bool selectionMade = false; // [en] Has player made a choice / [tr] Seçim yapýldý mý
    private bool coinFlipped = false;   // [en] Has coin been flipped / [tr] Yazý-tura atýldý mý
    private bool gameStarted = false;   // [en] Has game started / [tr] Oyun baþladý mý

    private float maxTime = 10f;        // [en] Maximum countdown time / [tr] Maksimum geri sayým süresi
    private float remainingTime;        // [en] Remaining countdown time / [tr] Kalan süre

    void Awake()
    {
        if (Instance == null) Instance = this; // [en] Assign singleton / [tr] Singleton ata
        else Destroy(gameObject);             // [en] Destroy duplicate instances / [tr] Çoðullarýný yok et
    }

    void Start()
    {
        DeckManager.Instance.GenerateAIDeck(); // [en] Generate AI deck / [tr] AI destesi oluþtur
        Debug.Log("AI deste oluþturuldu, kart sayýsý: " + DeckManager.Instance.aiDeck.Count); // [en] Log AI deck count / [tr] AI deste sayýsýný logla

        coinTossPanel.SetActive(true);          // [en] Show coin toss panel / [tr] Paneli göster
        resultText.text = "MAKE YOUR CHOICE";   // [en] Instruction text / [tr] Talimat metni
        countdownText.text = "";                // [en] Clear countdown text / [tr] Geri sayýmý temizle

        btnGoal.onClick.AddListener(() => PlayerSelectsChoice(CoinChoice.Goal, btnGoal)); // [en] Player chooses Goal / [tr] Oyuncu kale seçimi
        btnBall.onClick.AddListener(() => PlayerSelectsChoice(CoinChoice.Ball, btnBall)); // [en] Player chooses Ball / [tr] Oyuncu top seçimi
        btnFlip.onClick.AddListener(FlipCoinButtonPressed);                                 // [en] Flip coin button / [tr] Yazý-tura at butonu
        btnStart.onClick.AddListener(StartGameManually);                                    // [en] Start game manually / [tr] Oyunu manuel baþlat

        remainingTime = maxTime;           // [en] Initialize remaining time / [tr] Kalan süreyi baþlat
        StartCoroutine(CountdownTimer());  // [en] Start countdown / [tr] Geri sayýmý baþlat
    }

    void PlayerSelectsChoice(CoinChoice choice, Button btn)
    {
        playerChoice = choice;             // [en] Assign player's choice / [tr] Oyuncu seçimini ata
        selectionMade = true;              // [en] Set selection made flag / [tr] Seçim yapýldý iþareti

        if (selectedButton != null && selectedButton != btn)
            selectedButton.transform.localScale = defaultScale; // [en] Reset previous button scale / [tr] Önceki buton ölçeðini sýfýrla

        selectedButton = btn;                                  // [en] Assign currently selected button / [tr] Seçili butonu ata
        selectedButton.transform.localScale = Vector3.one * selectedScale; // [en] Scale button / [tr] Butonu ölçeklendir

        resultText.text = $"SELECTED: {(choice == CoinChoice.Goal ? "GOAL" : "BALL")}"; // [en] Show selected choice / [tr] Seçimi göster
    }

    void FlipCoinButtonPressed()
    {
        if (!selectionMade || coinFlipped) return; // [en] Cannot flip if not selected or already flipped / [tr] Seçilmediyse veya atýldýysa geç
        coinFlipped = true;                       // [en] Mark coin as flipped / [tr] Yazý-tura atýldý iþareti
        btnGoal.interactable = false;             // [en] Disable buttons / [tr] Butonlarý kapat
        btnBall.interactable = false;
        StartCoroutine(FlipCoinRoutine());        // [en] Start flip animation / [tr] Yazý-tura animasyonu baþlat
    }

    void StartGameManually()
    {
        if (gameStarted) return;                 // [en] Prevent double start / [tr] Çift baþlatmayý önle
        gameStarted = true;                      // [en] Mark game as started / [tr] Oyunu baþlat

        if (!coinFlipped)                        // [en] If no coin flip / [tr] Eðer yazý-tura atýlmadýysa
        {
            firstTurn = Turn.AI;                 // [en] AI starts / [tr] AI baþlar
            resultText.text = "NO CHOICE MADE! AI STARTS!"; // [en] Show message / [tr] Mesaj göster
        }

        StartCoroutine(ClosePanelAndSetTurn());  // [en] Close panel and set turn / [tr] Paneli kapat ve sýrayý ayarla
    }

    IEnumerator CountdownTimer()
    {
        while (remainingTime > 0 && !gameStarted) // [en] Countdown loop / [tr] Geri sayým döngüsü
        {
            countdownText.text = $"{Mathf.CeilToInt(remainingTime)}"; // [en] Update countdown text / [tr] Geri sayým metnini güncelle
            remainingTime -= Time.deltaTime;                          // [en] Decrease time / [tr] Kalan zamaný azalt
            yield return null;
        }

        if (!gameStarted)                        // [en] If time runs out / [tr] Süre dolduysa
        {
            gameStarted = true;                  // [en] Start game / [tr] Oyunu baþlat
            firstTurn = Turn.AI;                 // [en] AI starts / [tr] AI baþlar
            resultText.text = "TIME'S UP! AI STARTS!"; // [en] Show message / [tr] Mesaj göster
            StartCoroutine(ClosePanelAndSetTurn());   // [en] Close panel and start / [tr] Paneli kapat ve baþlat
        }
    }

    IEnumerator FlipCoinRoutine()
    {
        resultText.text = "FLIPPING COIN..."; // [en] Show flipping message / [tr] Yazý-tura atýlýyor mesajý
        float duration = 1.5f;                // [en] Animation duration / [tr] Animasyon süresi
        float elapsed = 0f;                   // [en] Elapsed time / [tr] Geçen süre

        while (elapsed < duration)
        {
            coinImage.transform.Rotate(Vector3.up * 720 * Time.deltaTime); // [en] Rotate coin / [tr] Yazý-turayý döndür
            elapsed += Time.deltaTime;
            yield return null;
        }

        int coin = Random.Range(0, 2);                     // [en] 0=Goal, 1=Ball / [tr] 0=Kale, 1=Top
        CoinChoice result = coin == 0 ? CoinChoice.Goal : CoinChoice.Ball; // [en] Determine result / [tr] Sonucu belirle

        coinImage.sprite = (result == CoinChoice.Goal) ? goalSprite : ballSprite; // [en] Set coin sprite / [tr] Görseli ayarla
        coinImage.transform.rotation = Quaternion.identity;                        // [en] Reset rotation / [tr] Rotasyonu sýfýrla

        firstTurn = (playerChoice == result) ? Turn.Player : Turn.AI;             // [en] Decide first turn / [tr] Ýlk sýrayý belirle
        resultText.text = (firstTurn == Turn.Player) ? "YOU START!" : "AI STARTS!"; // [en] Display turn / [tr] Sýra mesajý
    }

    IEnumerator ClosePanelAndSetTurn()
    {
        yield return new WaitForSeconds(1f);     // [en] Wait before closing / [tr] Kapatmadan önce bekle
        coinTossPanel.SetActive(false);          // [en] Hide panel / [tr] Paneli gizle

        if (GameManager.Instance == null)        // [en] Check GameManager / [tr] GameManager kontrolü
        {
            Debug.LogError("GameManager sahnede bulunamadý!"); // [en] Error if missing / [tr] Hata mesajý
            yield break;
        }

        GameManager.Instance.currentTurn = (firstTurn == Turn.Player) ? GameTurn.Player : GameTurn.AI; // [en] Set current turn / [tr] Mevcut sýrayý ayarla

        if (GameManager.Instance.playerHand == null || GameManager.Instance.aiHand == null) // [en] Check hands / [tr] Elleri kontrol et
        {
            Debug.LogError("PlayerHand veya AIHand referansý eksik!"); // [en] Error if missing / [tr] Hata mesajý
            yield break;
        }

        GameManager.Instance.aiHand.InitAIHand(); // [en] Initialize AI hand / [tr] AI elini baþlat
        GameManager.Instance.StartTurn();          // [en] Start turn / [tr] Sýrayý baþlat
    }
}
