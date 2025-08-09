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
    public Button myDeckButton;  // Yeni eklenen

    [Header("Play Alt Men� Paneli")]
    public GameObject playSubMenuPanel;
    public Button singlePlayerButton;
    public Button multiplayerButton;

    // Fonksiyonlar public oldu, Inspector�dan atanabilir

    public void OnPlayClicked()
    {
        bool isActive = playSubMenuPanel.activeSelf;
        playSubMenuPanel.SetActive(!isActive);
    }

    public void OnOptionsClicked()
    {
        SceneManager.LoadScene("OptionsScene");
    }

    public void OnCreditsClicked()
    {
        Debug.Log("Credits se�ildi");
    }

    public void OnHelpClicked()
    {
        Debug.Log("Help se�ildi");
    }

    public void OnQuitClicked()
    {
        Debug.Log("Oyun kapat�l�yor...");
        Application.Quit();
    }

    public void OnSinglePlayerClicked()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnMultiplayerClicked()
    {
        Debug.Log("Multiplayer se�ildi");
        SceneManager.LoadScene("MultiplayerScene");
    }

    public void OnMyDeckClicked()
    {
        SceneManager.LoadScene("MyDeckScene");
    }
}
