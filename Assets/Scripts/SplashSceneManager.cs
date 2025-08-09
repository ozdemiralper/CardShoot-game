using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SplashSceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // VideoPlayer component referansý
    public static PlayerCardCollection playerCards;

    void Start()
    {
        CardDatabase.Initialize();
        playerCards = PlayerCardCollection.Load();

        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer componenti atanmadý!");
            LoadMenu();  // Video yoksa direkt geç
            return;
        }

        // Video bittiðinde LoadMenu çaðrýlacak
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        LoadMenu();
    }

    void LoadMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
