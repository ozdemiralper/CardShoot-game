using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    public float splashDuration = 10f;

    void Start()
    {
        Invoke("LoadMenu", splashDuration);
    }

    void LoadMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
