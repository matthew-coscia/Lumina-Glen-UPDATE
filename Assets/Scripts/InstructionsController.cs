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

    private int currentVideoIndex = 0;


    void Start()
    {

        // Initially play the first video if not already playing
        if (!videoPlayer.isPlaying)
        {
            videoPlayer.clip = videoClips[currentVideoIndex];
            videoPlayer.Play();
        }

        // Setting up the button event listener
        nextVideoButton.onClick.AddListener(PlayNextVideo);
    }

    public void PlayNextVideo()
    {
        if (currentVideoIndex < videoClips.Length - 1)
        {
            currentVideoIndex++;
            videoPlayer.clip = videoClips[currentVideoIndex];
            videoPlayer.Play();
        }
        else
        {
             SceneManager.LoadScene("Level 1");
            
        }
    }

}
