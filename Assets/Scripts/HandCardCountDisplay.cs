using TMPro;
using UnityEngine;

public class HandCardCountDisplay : MonoBehaviour
{
    public TMP_Text playerCardCountText;
    public TMP_Text opponentCardCountText;

    public void SetPlayerCardCount(int count)
    {
        if (playerCardCountText != null)
            playerCardCountText.text = "" + count;
    }

    public void SetOpponentCardCount(int count)
    {
        if (opponentCardCountText != null)
            opponentCardCountText.text = "" + count;
    }
}
