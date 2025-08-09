using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Ana Menü Butonlarý")]
    public Button playButton;
    public Button optionsButton;
    public Button creditsButton;
    public Button helpButton;
    public Button quitButton;

    [Header("Play Alt Menü Paneli")]
    public GameObject playSubMenuPanel;
    public Button singlePlayerButton;
    public Button multiplayerButton;

    void OnPlayClicked()
    {
        bool isActive = playSubMenuPanel.activeSelf;
        playSubMenuPanel.SetActive(!isActive);
    }


    void OnOptionsClicked()
    {
        SceneManager.LoadScene("OptionsScene");
    }

    void OnCreditsClicked()
    {
        Debug.Log("Credits seçildi");
    }

    void OnHelpClicked()
    {
        Debug.Log("Help seçildi");
    }

    void OnQuitClicked()
    {
        Debug.Log("Oyun kapatýlýyor...");
        Application.Quit();
    }

    void OnSinglePlayerClicked()
    {
        SceneManager.LoadScene("GameScene");
    }

    void OnMultiplayerClicked()
    {
        Debug.Log("Multiplayer seçildi");
        SceneManager.LoadScene("MultiplayerScene");
    }
}
