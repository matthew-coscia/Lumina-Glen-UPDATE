using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{
    public VideoPlayer videoPlayer; 

    void Start()
    {
        videoPlayer.loopPointReached += LoadMainGameScene;
        if (!videoPlayer.isPlaying)
        {
            videoPlayer.Play();
        }
    }

    void LoadMainGameScene(VideoPlayer vp)
    {
        SceneManager.LoadScene("Instructions"); 
    }
}