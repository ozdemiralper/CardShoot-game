using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class AIUIManager : MonoBehaviour
{
    public static AIUIManager Instance;  // [en] Singleton instance / [tr] Tekil örnek
    [Header("AI UI")]
    public Image aiAvatarImage;           // [en] AI character's avatar Image component / [tr] AI karakterinin avatarýný gösterecek Image componenti
    public TMP_Text aiNickText;           // [en] AI character's nickname TMP_Text      / [tr] AI karakterinin ismini gösterecek TMP_Text
    public Sprite[] coachSprites;         // [en] Array of coach avatars                / [tr] Koç avatarlarý için sprite dizisi
    private void Awake()
    {
        if (Instance == null) Instance = this;  // [en] Assign instance if null      / [tr] Eðer Instance boþsa kendimizi atýyoruz
        else Destroy(gameObject);               // [en] Destroy duplicate instance   / [tr] Yoksa objeyi siliyoruz
    }
    public void SetupAIUI()
    {
        if (aiAvatarImage != null && coachSprites.Length > 0)  // [en] Check if avatar and sprites exist   / [tr] Avatar ve sprite dizisi varsa
        {
            int index = Random.Range(0, coachSprites.Length);  // [en] Random index for sprite             / [tr] Sprite için rastgele index
            aiAvatarImage.sprite = coachSprites[index];        // [en] Assign selected sprite              / [tr] Seçilen avatarý ata
        }
        if (aiNickText != null)                                // [en] Check if nickname text exists       / [tr] Nick Text component varsa
        {
            aiNickText.text = GenerateRandomNick(3);           // [en] Generate random 3-letter nick       / [tr] 3 harfli rastgele nick oluþtur
        }
    }
    private string GenerateRandomNick(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";       // [en] Characters to use                 / [tr] Kullanýlacak karakterler
        string nick = "";                                        // [en] Initialize empty string           / [tr] Baþlangýçta boþ string
        for (int i = 0; i < length; i++)                         // [en] Loop for each character           / [tr] Her karakter için döngü
        {
            nick += chars[Random.Range(0, chars.Length)];        // [en] Append random character           / [tr] Rastgele karakter ekle
        }
        return nick;                                             // [en] Return generated nick             / [tr] Oluþturulan nicki döndür
    } 
}
