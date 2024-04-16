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
    public Button startButton;  // This is the Start button which replaces the Next button on the last video

    private int currentVideoIndex = 0;

    void Start()
    {
        // Initially play the first video if not already playing
        videoPlayer.clip = videoClips[currentVideoIndex];
        videoPlayer.Play();

        // Setting up the button event listeners
        nextVideoButton.onClick.AddListener(PlayNextVideo);
        startButton.onClick.AddListener(LoadNextScene);

        // Initially hide the start button
        startButton.gameObject.SetActive(false);
    }

    public void PlayNextVideo()
    {
        // Increment the video index
        currentVideoIndex++;

        if (currentVideoIndex < videoClips.Length)
        {
            videoPlayer.clip = videoClips[currentVideoIndex];
            videoPlayer.Play();
        }

        // Check if the current video is the last one
        if (currentVideoIndex == videoClips.Length - 1)
        {
            // This is the last video, so switch buttons
            nextVideoButton.gameObject.SetActive(false);
            startButton.gameObject.SetActive(true);
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("Level 1");
    }
}
