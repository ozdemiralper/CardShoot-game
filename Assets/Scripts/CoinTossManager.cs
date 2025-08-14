using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public enum CoinChoice
{
    Goal, // GOAL
    Ball  // BALL
}

public enum Turn
{
    Player,
    AI
}

public class CoinTossManager : MonoBehaviour
{
    public static CoinTossManager Instance;

    [Header("UI Elements")]
    public GameObject coinTossPanel;
    public Button btnGoal;
    public Button btnBall;
    public Button btnFlip;
    public Button btnStart;
    public Image coinImage;
    public Sprite goalSprite;
    public Sprite ballSprite;
    public TMP_Text resultText;
    public TMP_Text countdownText;

    [Header("Button Scale Settings")]
    public float selectedScale = 1.2f;
    private Vector3 defaultScale = Vector3.one;

    [HideInInspector] public CoinChoice playerChoice;
    private Button selectedButton;
    public Turn firstTurn;

    private bool selectionMade = false;
    private bool coinFlipped = false;
    private bool gameStarted = false;

    private float maxTime = 10f;
    private float remainingTime;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        coinTossPanel.SetActive(true);
        resultText.text = "MAKE YOUR CHOICE"; // Kullanýcýya görünen yazý büyük
        countdownText.text = "";

        btnGoal.onClick.AddListener(() => PlayerSelectsChoice(CoinChoice.Goal, btnGoal));
        btnBall.onClick.AddListener(() => PlayerSelectsChoice(CoinChoice.Ball, btnBall));
        btnFlip.onClick.AddListener(FlipCoinButtonPressed);
        btnStart.onClick.AddListener(StartGameManually);

        remainingTime = maxTime;
        StartCoroutine(CountdownTimer());
    }

    void PlayerSelectsChoice(CoinChoice choice, Button btn)
    {
        playerChoice = choice;
        selectionMade = true;

        if (selectedButton != null && selectedButton != btn)
        {
            selectedButton.transform.localScale = defaultScale;
        }

        selectedButton = btn;
        selectedButton.transform.localScale = Vector3.one * selectedScale;

        resultText.text = $"SELECTED: {(choice == CoinChoice.Goal ? "GOAL" : "BALL")}";
    }

    void FlipCoinButtonPressed()
    {
        if (!selectionMade || coinFlipped) return;

        coinFlipped = true;
        btnGoal.interactable = false;
        btnBall.interactable = false;
        StartCoroutine(FlipCoinRoutine());
    }

    void StartGameManually()
    {
        if (gameStarted) return;
        gameStarted = true;

        if (!coinFlipped)
        {
            firstTurn = Turn.AI;
            resultText.text = "NO CHOICE MADE! AI STARTS!";
            StartCoroutine(ClosePanelAndSetTurn());
        }
        else
        {
            StartCoroutine(ClosePanelAndSetTurn());
        }
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
            firstTurn = Turn.AI;
            resultText.text = "TIME'S UP! AI STARTS!";
            StartCoroutine(ClosePanelAndSetTurn());
        }
    }

    IEnumerator FlipCoinRoutine()
    {
        resultText.text = "FLIPPING COIN...";
        float duration = 1.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            coinImage.transform.Rotate(Vector3.up * 720 * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        int coin = Random.Range(0, 2); // 0=Goal, 1=Ball
        CoinChoice result = coin == 0 ? CoinChoice.Goal : CoinChoice.Ball;

        coinImage.sprite = (result == CoinChoice.Goal) ? goalSprite : ballSprite;
        coinImage.transform.rotation = Quaternion.identity;

        firstTurn = (playerChoice == result) ? Turn.Player : Turn.AI;
        resultText.text = (firstTurn == Turn.Player) ? "YOU START!" : "AI STARTS!";
    }

    IEnumerator ClosePanelAndSetTurn()
    {
        yield return new WaitForSeconds(1f);
        coinTossPanel.SetActive(false);
        GameTurnManager.Instance.SetFirstTurn(firstTurn);
    }
}
