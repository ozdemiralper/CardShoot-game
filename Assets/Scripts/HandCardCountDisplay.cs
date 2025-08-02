using TMPro;
using UnityEngine;

public class HandCardCountDisplay : MonoBehaviour
{
    public TMP_Text playerCardCountText;         // Elde kalan kart sayýsý (oyuncu)
    public TMP_Text opponentCardCountText;       // Elde kalan kart sayýsý (rakip)

    public TMP_Text playerFieldCardCountText;    // Sahadaki kart sayýsý (oyuncu)
    public TMP_Text opponentFieldCardCountText;  // Sahadaki kart sayýsý (rakip)

    public void SetPlayerCardCount(int count)
    {
        if (playerCardCountText != null)
            playerCardCountText.text = count.ToString();
    }

    public void SetOpponentCardCount(int count)
    {
        if (opponentCardCountText != null)
            opponentCardCountText.text = count.ToString();
    }

    public void SetPlayerFieldCardCount(int count)
    {
        if (playerFieldCardCountText != null)
            playerFieldCardCountText.text = count.ToString();
    }

    public void SetOpponentFieldCardCount(int count)
    {
        if (opponentFieldCardCountText != null)
            opponentFieldCardCountText.text = count.ToString();
    }
}
