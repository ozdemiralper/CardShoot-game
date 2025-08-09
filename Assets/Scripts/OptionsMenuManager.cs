using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionsMenuManager : MonoBehaviour
{
    [Header("Input Alanlarý")]
    public TMP_InputField nameInputField;
    public TMP_InputField nicknameInputField;

    [Header("Coach (Avatar) Seçimi")]
    public TMP_Dropdown avatarDropdown;
    public Image avatarPreview;
    public Button saveButton;

    private List<Card> coachCards;

    private void Start()
    {
        nicknameInputField.characterLimit = 3;

        if (PlayerPrefs.HasKey("PlayerFullName"))
            nameInputField.text = PlayerPrefs.GetString("PlayerFullName");

        if (PlayerPrefs.HasKey("PlayerNickname"))
            nicknameInputField.text = PlayerPrefs.GetString("PlayerNickname");

        // Sadece Coach kartlarýný filtrele
        coachCards = CardDatabase.cardList
            .Where(c => c.cardType == CardType.Coach)
            .ToList();

        // Dropdown seçeneklerini coach isimleriyle doldur
        avatarDropdown.ClearOptions();
        avatarDropdown.AddOptions(coachCards.Select(c => c.cardName).ToList());

        if (PlayerPrefs.HasKey("SelectedAvatarIndex"))
        {
            int index = PlayerPrefs.GetInt("SelectedAvatarIndex");
            if (index >= 0 && index < coachCards.Count)
            {
                avatarDropdown.value = index;
                UpdateAvatarPreview(index);
            }
        }
        else
        {
            UpdateAvatarPreview(0);
        }

        avatarDropdown.onValueChanged.AddListener(UpdateAvatarPreview);
        saveButton.onClick.AddListener(SaveSettings);
    }

    private void UpdateAvatarPreview(int index)
    {
        if (index >= 0 && index < coachCards.Count)
        {
            Sprite sprite = Resources.Load<Sprite>(coachCards[index].imagePath);
            if (sprite != null)
                avatarPreview.sprite = sprite;
            else
                Debug.LogWarning("Avatar sprite bulunamadý: " + coachCards[index].imagePath);
        }
    }

    private void SaveSettings()
    {
        string fullName = nameInputField.text.Trim();
        string nickname = nicknameInputField.text.Trim();

        if (nickname.Length != 3)
        {
            Debug.LogWarning("Kýsa isim (nickname) tam olarak 3 karakter olmalý!");
            return;
        }

        int selectedAvatarIndex = avatarDropdown.value;

        PlayerPrefs.SetString("PlayerFullName", fullName);
        PlayerPrefs.SetString("PlayerNickname", nickname);
        PlayerPrefs.SetInt("SelectedAvatarIndex", selectedAvatarIndex);
        PlayerPrefs.Save();

        // Seçilen coach kartýný al
        Card selectedCoach = coachCards[selectedAvatarIndex];
        Sprite avatarSprite = Resources.Load<Sprite>(selectedCoach.imagePath);

        // PlayerDataManager'a gönder
        PlayerDataManager.Instance.SetPlayerData(nickname, avatarSprite);
        SceneManager.LoadScene("MenuScene");
    }

}
