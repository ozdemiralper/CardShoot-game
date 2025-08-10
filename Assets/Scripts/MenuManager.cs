using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Ana Menü Butonlarý")]
    public Button playButton;
    public Button optionsButton;
    public Button cardsButton;
    public Button creditsButton;
    public Button helpButton;
    public Button quitButton;

    [Header("Play Alt Menü Paneli")]
    public GameObject playSubMenuPanel;
    public Button singlePlayerButton;
    public Button multiplayerButton;

    [Header("Cards Alt Menü Paneli")]
    public GameObject cardsSubMenuPanel;
    public Button MyCards;
    public Button AllCards;

    public void OnPlayClicked()
    {
        bool isActive = playSubMenuPanel.activeSelf;
        playSubMenuPanel.SetActive(!isActive);
    }

    public void OnCardsClicked()
    {
        bool isActive = cardsSubMenuPanel.activeSelf;
        cardsSubMenuPanel.SetActive(!isActive);
    }
    public void OnOptionsClicked()
    {
        SceneManager.LoadScene("OptionsScene");
    }

    public void OnCreditsClicked()
    {
        Debug.Log("Credits seçildi");
    }

    public void OnHelpClicked()
    {
        Debug.Log("Help seçildi");
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }

    public void OnSinglePlayerClicked()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnMultiplayerClicked()
    {
        Debug.Log("Multiplayer seçildi");
        SceneManager.LoadScene("MultiplayerScene");
    }

    public void OnMyCardsClicked()
    {
        SceneManager.LoadScene("MyCardsScene");
    }

    public void OnAllCardsClicked()
    {
        SceneManager.LoadScene("AllCardsScene");
    }
}