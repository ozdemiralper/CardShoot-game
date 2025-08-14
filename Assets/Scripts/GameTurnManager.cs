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
        Debug.Log("Oyun ba�l�yor! �lk s�ra: " + (isPlayerTurn ? "Player" : "AI"));

        // Burada oyunun normal ak���na ge�
        // �rn: PlayerHand aktif, AI logic haz�r
    }
}
