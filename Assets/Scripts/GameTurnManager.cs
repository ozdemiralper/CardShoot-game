using UnityEngine;

public class GameTurnManager : MonoBehaviour
{
    public static GameTurnManager Instance;
    public CoinTossManager TurnManager;

    public bool isPlayerTurn;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetFirstTurn(Turn first)
    {
        isPlayerTurn = (first == Turn.Player);
        Debug.Log("Oyun baþlýyor! Ýlk sýra: " + (isPlayerTurn ? "Player" : "AI"));

        // Burada oyunun normal akýþýna geç
        // Örn: PlayerHand aktif, AI logic hazýr
    }
}
