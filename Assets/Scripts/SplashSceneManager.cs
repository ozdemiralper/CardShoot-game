using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SplashSceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // VideoPlayer component referans�
    public static PlayerCardCollection playerCards;

    void Start()
    {
        CardDatabase.Initialize();
        playerCards = PlayerCardCollection.Load();

        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer componenti atanmad�!");
            LoadMenu();  // Video yoksa direkt ge�
            return;
        }

        // Video bitti�inde LoadMenu �a�r�lacak
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
