using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class AIUIManager : MonoBehaviour
{
    public static AIUIManager Instance;  // [en] Singleton instance / [tr] Tekil �rnek
    [Header("AI UI")]
    public Image aiAvatarImage;           // [en] AI character's avatar Image component / [tr] AI karakterinin avatar�n� g�sterecek Image componenti
    public TMP_Text aiNickText;           // [en] AI character's nickname TMP_Text      / [tr] AI karakterinin ismini g�sterecek TMP_Text
    public Sprite[] coachSprites;         // [en] Array of coach avatars                / [tr] Ko� avatarlar� i�in sprite dizisi
    private void Awake()
    {
        if (Instance == null) Instance = this;  // [en] Assign instance if null      / [tr] E�er Instance bo�sa kendimizi at�yoruz
        else Destroy(gameObject);               // [en] Destroy duplicate instance   / [tr] Yoksa objeyi siliyoruz
    }
    public void SetupAIUI()
    {
        if (aiAvatarImage != null && coachSprites.Length > 0)  // [en] Check if avatar and sprites exist   / [tr] Avatar ve sprite dizisi varsa
        {
            int index = Random.Range(0, coachSprites.Length);  // [en] Random index for sprite             / [tr] Sprite i�in rastgele index
            aiAvatarImage.sprite = coachSprites[index];        // [en] Assign selected sprite              / [tr] Se�ilen avatar� ata
        }
        if (aiNickText != null)                                // [en] Check if nickname text exists       / [tr] Nick Text component varsa
        {
            aiNickText.text = GenerateRandomNick(3);           // [en] Generate random 3-letter nick       / [tr] 3 harfli rastgele nick olu�tur
        }
    }
    private string GenerateRandomNick(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";       // [en] Characters to use                 / [tr] Kullan�lacak karakterler
        string nick = "";                                        // [en] Initialize empty string           / [tr] Ba�lang��ta bo� string
        for (int i = 0; i < length; i++)                         // [en] Loop for each character           / [tr] Her karakter i�in d�ng�
        {
            nick += chars[Random.Range(0, chars.Length)];        // [en] Append random character           / [tr] Rastgele karakter ekle
        }
        return nick;                                             // [en] Return generated nick             / [tr] Olu�turulan nicki d�nd�r
    } 
}
