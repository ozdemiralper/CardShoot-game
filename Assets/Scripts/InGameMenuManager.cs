using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class InGameMenuManager : MonoBehaviour
{
    [Header("Ana Menü Paneli")]
    public GameObject menuPanel;
    public Button hamburgerButton;
    public Button btnMenu;
    public Button btnSettings;
    public Button btnQuit;
    public Button btnClose1;
    public Button btnClose2;

    [Header("Oyun Ýçi Ayarlar Paneli")]
    public GameObject settingsPanel;
    public TMP_InputField nicknameInput;
    public TMP_Dropdown avatarDropdown;
    public Image avatarPreview;
    public Button saveButton;

    private List<Card> coachCards;

    private void Start()
    {
        // Paneller kapalý baþlasýn
        menuPanel.SetActive(false);
        settingsPanel.SetActive(false);

        // Ana Menü butonlarý
        hamburgerButton.onClick.AddListener(ToggleMenu);
        btnClose1.onClick.AddListener(CloseMenu);
        btnClose2.onClick.AddListener(CloseMenu);
        btnMenu.onClick.AddListener(ReturnToMainMenu);
        btnSettings.onClick.AddListener(OpenSettings);
        btnQuit.onClick.AddListener(QuitGame);

        // Ayar paneli save butonu
        saveButton.onClick.AddListener(SaveSettings);

        // Coach kartlarýný yükle
        coachCards = CardDatabase.cardList
            .Where(c => c.cardType == CardType.Coach)
            .ToList();

        // Dropdown’ý doldur
        avatarDropdown.ClearOptions();
        avatarDropdown.AddOptions(coachCards.Select(c => c.cardName).ToList());

        // Avatar deðiþtikçe önizleme güncellensin
        avatarDropdown.onValueChanged.AddListener(UpdateAvatarPreview);
    }

    private void ToggleMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
    }

    private void CloseMenu()
    {
        menuPanel.SetActive(false);
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    private void OpenSettings()
    {
        // Mevcut veriyi yükle
        nicknameInput.text = PlayerDataManager.Instance.nickname;

        // Avatar index PlayerPrefs’ten ya da default
        int index = PlayerPrefs.GetInt("SelectedAvatarIndex", 0);
        avatarDropdown.value = index;
        UpdateAvatarPreview(index);

        // Ayarlar panelini aç
        settingsPanel.SetActive(true);
    }

    private void SaveSettings()
    {
        string newNickname = nicknameInput.text.Trim();
        if (newNickname.Length != 3)
        {
            Debug.LogWarning("Nickname tam olarak 3 karakter olmalý!");
            return;
        }

        int selectedAvatarIndex = avatarDropdown.value;
        Sprite avatarSprite = Resources.Load<Sprite>(coachCards[selectedAvatarIndex].imagePath);

        // PlayerDataManager’a yaz
        PlayerDataManager.Instance.SetPlayerData(newNickname, avatarSprite);

        // PlayerPrefs’e kaydet
        PlayerPrefs.SetString("PlayerNickname", newNickname);
        PlayerPrefs.SetInt("SelectedAvatarIndex", selectedAvatarIndex);
        PlayerPrefs.Save();

        // UI'yi güncelle
        PlayerUIController uiManager = FindObjectOfType<PlayerUIController>();
        if (uiManager != null)
        {
            uiManager.UpdateUI();
        }

        // Paneli kapat
        settingsPanel.SetActive(false);
    }


    private void UpdateAvatarPreview(int index)
    {
        if (index >= 0 && index < coachCards.Count)
        {
            Sprite sprite = Resources.Load<Sprite>(coachCards[index].imagePath);
            if (sprite != null)
                avatarPreview.sprite = sprite;
        }
    }

    private void QuitGame()
    {
        Debug.Log("Oyun kapatýlýyor...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
