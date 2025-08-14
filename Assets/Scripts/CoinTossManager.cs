using UnityEngine;                 // [en] Unity core namespace / [tr] Unity temel isim alan�
using UnityEngine.UI;              // [en] For UI components / [tr] UI bile�enleri i�in
using System.Collections;          // [en] For coroutines / [tr] Coroutine kullan�m� i�in
using TMPro;                       // [en] For TextMeshPro UI / [tr] TextMeshPro aray�z� i�in

public enum CoinChoice
{
    Goal, // [en] Goal / [tr] Kale
    Ball  // [en] Ball / [tr] Top
}

public enum Turn
{
    Player, // [en] Player turn / [tr] Oyuncu s�ras�
    AI      // [en] AI turn / [tr] AI s�ras�
}

public class CoinTossManager : MonoBehaviour
{
    public static CoinTossManager Instance; // [en] Singleton instance / [tr] Singleton instance

    [Header("UI Elements")]
    public GameObject coinTossPanel;    // [en] Panel containing coin toss UI / [tr] Yaz�-tura paneli
    public Button btnGoal;              // [en] Button for choosing Goal / [tr] Kale se�me butonu
    public Button btnBall;              // [en] Button for choosing Ball / [tr] Top se�me butonu
    public Button btnFlip;              // [en] Button for flipping coin / [tr] Yaz�-tura at butonu
    public Button btnStart;             // [en] Start game button / [tr] Ba�lat butonu
    public Image coinImage;             // [en] Image of the coin / [tr] Yaz�-tura g�rseli
    public Sprite goalSprite;           // [en] Sprite for Goal / [tr] Kale g�rseli
    public Sprite ballSprite;           // [en] Sprite for Ball / [tr] Top g�rseli
    public TMP_Text resultText;         // [en] Text for displaying result / [tr] Sonu� metni
    public TMP_Text countdownText;      // [en] Countdown display text / [tr] Geri say�m metni

    [Header("Button Scale Settings")]
    public float selectedScale = 1.2f; // [en] Scale when button is selected / [tr] Se�ili buton boyutu
    private Vector3 defaultScale = Vector3.one; // [en] Default button scale / [tr] Buton varsay�lan boyutu

    [HideInInspector] public CoinChoice playerChoice; // [en] Player's choice / [tr] Oyuncu se�imi
    private Button selectedButton;                     // [en] Currently selected button / [tr] Se�ili buton
    public Turn firstTurn;                             // [en] Who starts first / [tr] �lk s�ras� kim alacak

    private bool selectionMade = false; // [en] Has player made a choice / [tr] Se�im yap�ld� m�
    private bool coinFlipped = false;   // [en] Has coin been flipped / [tr] Yaz�-tura at�ld� m�
    private bool gameStarted = false;   // [en] Has game started / [tr] Oyun ba�lad� m�

    private float maxTime = 10f;        // [en] Maximum countdown time / [tr] Maksimum geri say�m s�resi
    private float remainingTime;        // [en] Remaining countdown time / [tr] Kalan s�re

    void Awake()
    {
        if (Instance == null) Instance = this; // [en] Assign singleton / [tr] Singleton ata
        else Destroy(gameObject);             // [en] Destroy duplicate instances / [tr] �o�ullar�n� yok et
    }

    void Start()
    {
        DeckManager.Instance.GenerateAIDeck(); // [en] Generate AI deck / [tr] AI destesi olu�tur
        Debug.Log("AI deste olu�turuldu, kart say�s�: " + DeckManager.Instance.aiDeck.Count); // [en] Log AI deck count / [tr] AI deste say�s�n� logla

        coinTossPanel.SetActive(true);          // [en] Show coin toss panel / [tr] Paneli g�ster
        resultText.text = "MAKE YOUR CHOICE";   // [en] Instruction text / [tr] Talimat metni
        countdownText.text = "";                // [en] Clear countdown text / [tr] Geri say�m� temizle

        btnGoal.onClick.AddListener(() => PlayerSelectsChoice(CoinChoice.Goal, btnGoal)); // [en] Player chooses Goal / [tr] Oyuncu kale se�imi
        btnBall.onClick.AddListener(() => PlayerSelectsChoice(CoinChoice.Ball, btnBall)); // [en] Player chooses Ball / [tr] Oyuncu top se�imi
        btnFlip.onClick.AddListener(FlipCoinButtonPressed);                                 // [en] Flip coin button / [tr] Yaz�-tura at butonu
        btnStart.onClick.AddListener(StartGameManually);                                    // [en] Start game manually / [tr] Oyunu manuel ba�lat

        remainingTime = maxTime;           // [en] Initialize remaining time / [tr] Kalan s�reyi ba�lat
        StartCoroutine(CountdownTimer());  // [en] Start countdown / [tr] Geri say�m� ba�lat
    }

    void PlayerSelectsChoice(CoinChoice choice, Button btn)
    {
        playerChoice = choice;             // [en] Assign player's choice / [tr] Oyuncu se�imini ata
        selectionMade = true;              // [en] Set selection made flag / [tr] Se�im yap�ld� i�areti

        if (selectedButton != null && selectedButton != btn)
            selectedButton.transform.localScale = defaultScale; // [en] Reset previous button scale / [tr] �nceki buton �l�e�ini s�f�rla

        selectedButton = btn;                                  // [en] Assign currently selected button / [tr] Se�ili butonu ata
        selectedButton.transform.localScale = Vector3.one * selectedScale; // [en] Scale button / [tr] Butonu �l�eklendir

        resultText.text = $"SELECTED: {(choice == CoinChoice.Goal ? "GOAL" : "BALL")}"; // [en] Show selected choice / [tr] Se�imi g�ster
    }

    void FlipCoinButtonPressed()
    {
        if (!selectionMade || coinFlipped) return; // [en] Cannot flip if not selected or already flipped / [tr] Se�ilmediyse veya at�ld�ysa ge�
        coinFlipped = true;                       // [en] Mark coin as flipped / [tr] Yaz�-tura at�ld� i�areti
        btnGoal.interactable = false;             // [en] Disable buttons / [tr] Butonlar� kapat
        btnBall.interactable = false;
        StartCoroutine(FlipCoinRoutine());        // [en] Start flip animation / [tr] Yaz�-tura animasyonu ba�lat
    }

    void StartGameManually()
    {
        if (gameStarted) return;                 // [en] Prevent double start / [tr] �ift ba�latmay� �nle
        gameStarted = true;                      // [en] Mark game as started / [tr] Oyunu ba�lat

        if (!coinFlipped)                        // [en] If no coin flip / [tr] E�er yaz�-tura at�lmad�ysa
        {
            firstTurn = Turn.AI;                 // [en] AI starts / [tr] AI ba�lar
            resultText.text = "NO CHOICE MADE! AI STARTS!"; // [en] Show message / [tr] Mesaj g�ster
        }

        StartCoroutine(ClosePanelAndSetTurn());  // [en] Close panel and set turn / [tr] Paneli kapat ve s�ray� ayarla
    }

    IEnumerator CountdownTimer()
    {
        while (remainingTime > 0 && !gameStarted) // [en] Countdown loop / [tr] Geri say�m d�ng�s�
        {
            countdownText.text = $"{Mathf.CeilToInt(remainingTime)}"; // [en] Update countdown text / [tr] Geri say�m metnini g�ncelle
            remainingTime -= Time.deltaTime;                          // [en] Decrease time / [tr] Kalan zaman� azalt
            yield return null;
        }

        if (!gameStarted)                        // [en] If time runs out / [tr] S�re dolduysa
        {
            gameStarted = true;                  // [en] Start game / [tr] Oyunu ba�lat
            firstTurn = Turn.AI;                 // [en] AI starts / [tr] AI ba�lar
            resultText.text = "TIME'S UP! AI STARTS!"; // [en] Show message / [tr] Mesaj g�ster
            StartCoroutine(ClosePanelAndSetTurn());   // [en] Close panel and start / [tr] Paneli kapat ve ba�lat
        }
    }

    IEnumerator FlipCoinRoutine()
    {
        resultText.text = "FLIPPING COIN..."; // [en] Show flipping message / [tr] Yaz�-tura at�l�yor mesaj�
        float duration = 1.5f;                // [en] Animation duration / [tr] Animasyon s�resi
        float elapsed = 0f;                   // [en] Elapsed time / [tr] Ge�en s�re

        while (elapsed < duration)
        {
            coinImage.transform.Rotate(Vector3.up * 720 * Time.deltaTime); // [en] Rotate coin / [tr] Yaz�-turay� d�nd�r
            elapsed += Time.deltaTime;
            yield return null;
        }

        int coin = Random.Range(0, 2);                     // [en] 0=Goal, 1=Ball / [tr] 0=Kale, 1=Top
        CoinChoice result = coin == 0 ? CoinChoice.Goal : CoinChoice.Ball; // [en] Determine result / [tr] Sonucu belirle

        coinImage.sprite = (result == CoinChoice.Goal) ? goalSprite : ballSprite; // [en] Set coin sprite / [tr] G�rseli ayarla
        coinImage.transform.rotation = Quaternion.identity;                        // [en] Reset rotation / [tr] Rotasyonu s�f�rla

        firstTurn = (playerChoice == result) ? Turn.Player : Turn.AI;             // [en] Decide first turn / [tr] �lk s�ray� belirle
        resultText.text = (firstTurn == Turn.Player) ? "YOU START!" : "AI STARTS!"; // [en] Display turn / [tr] S�ra mesaj�
    }

    IEnumerator ClosePanelAndSetTurn()
    {
        yield return new WaitForSeconds(1f);     // [en] Wait before closing / [tr] Kapatmadan �nce bekle
        coinTossPanel.SetActive(false);          // [en] Hide panel / [tr] Paneli gizle

        if (GameManager.Instance == null)        // [en] Check GameManager / [tr] GameManager kontrol�
        {
            Debug.LogError("GameManager sahnede bulunamad�!"); // [en] Error if missing / [tr] Hata mesaj�
            yield break;
        }

        GameManager.Instance.currentTurn = (firstTurn == Turn.Player) ? GameTurn.Player : GameTurn.AI; // [en] Set current turn / [tr] Mevcut s�ray� ayarla

        if (GameManager.Instance.playerHand == null || GameManager.Instance.aiHand == null) // [en] Check hands / [tr] Elleri kontrol et
        {
            Debug.LogError("PlayerHand veya AIHand referans� eksik!"); // [en] Error if missing / [tr] Hata mesaj�
            yield break;
        }

        GameManager.Instance.aiHand.InitAIHand(); // [en] Initialize AI hand / [tr] AI elini ba�lat
        GameManager.Instance.StartTurn();          // [en] Start turn / [tr] S�ray� ba�lat
    }
}
