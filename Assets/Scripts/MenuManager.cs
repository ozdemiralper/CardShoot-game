using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Ana Men� Butonlar�")]
    public Button playButton;
    public Button optionsButton;
    public Button creditsButton;
    public Button helpButton;
    public Button quitButton;

    [Header("Play Alt Men� Paneli")]
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
        Debug.Log("Credits se�ildi");
    }

    void OnHelpClicked()
    {
        Debug.Log("Help se�ildi");
    }

    void OnQuitClicked()
    {
        Debug.Log("Oyun kapat�l�yor...");
        Application.Quit();
    }

    void OnSinglePlayerClicked()
    {
        SceneManager.LoadScene("GameScene");
    }

    void OnMultiplayerClicked()
    {
        Debug.Log("Multiplayer se�ildi");
        SceneManager.LoadScene("MultiplayerScene");
    }
}
