using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIController: MonoBehaviour
{
    public Image avatarImage;
    public TMP_Text nicknameText;

    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (PlayerDataManager.Instance != null)
        {
            if (PlayerDataManager.Instance.selectedAvatarSprite != null)
                avatarImage.sprite = PlayerDataManager.Instance.selectedAvatarSprite;

            nicknameText.text = PlayerDataManager.Instance.nickname;
        }
        
    }
}
