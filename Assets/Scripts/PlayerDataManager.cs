using UnityEngine;
using System.Linq;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;

    public string nickname;
    public Sprite selectedAvatarSprite;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("PlayerNickname"))
        {
            PlayerPrefs.SetString("PlayerNickname", "AAA"); // default nickname
            PlayerPrefs.SetInt("SelectedAvatarIndex", 0);   // default avatar index
            PlayerPrefs.Save();
        }

        nickname = PlayerPrefs.GetString("PlayerNickname");

        int avatarIndex = PlayerPrefs.GetInt("SelectedAvatarIndex", 0);

        // Coach tipindeki kartlarý filtrele
        var coachCards = CardDatabase.cardList.Where(c => c.cardType == CardType.Coach).ToList();

        if (coachCards.Count > 0)
        {
            int index = Mathf.Clamp(avatarIndex, 0, coachCards.Count - 1);
            selectedAvatarSprite = Resources.Load<Sprite>(coachCards[index].imagePath);
        }
    }

    public void SetPlayerData(string nick, Sprite avatar)
    {
        nickname = nick;
        selectedAvatarSprite = avatar;
    }
}
        