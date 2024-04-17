using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InstructionsController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public VideoClip[] videoClips;
    public Button nextVideoButton;
    public Button startButton; 

    private int currentVideoIndex = 0;

    void Start()
    {
        videoPlayer.clip = videoClips[currentVideoIndex];
        videoPlayer.Play();

        nextVideoButton.onClick.AddListener(PlayNextVideo);
        startButton.onClick.AddListener(LoadNextScene);

        startButton.gameObject.SetActive(false);
    }

    public void PlayNextVideo()
    {
        currentVideoIndex++;

        if (currentVideoIndex < videoClips.Length)
        {
            videoPlayer.clip = videoClips[currentVideoIndex];
            videoPlayer.Play();
        }

        if (currentVideoIndex == videoClips.Length - 1)
        {
            nextVideoButton.gameObject.SetActive(false);
            startButton.gameObject.SetActive(true);
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("Level 1");
    }
}
