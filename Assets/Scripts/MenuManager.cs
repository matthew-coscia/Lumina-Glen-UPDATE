using UnityEngine;
using UnityEngine.UI; // Required for interacting with Sliders and Buttons

public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel; // Assign your menu panel to this in the inspector
    public Slider sensitivitySlider; // Assign your sensitivity slider to this
    public Slider volumeSlider; // Assign your volume slider to this
    public float mouseSensitivityMulti;
    public GameObject crosshair;
    public bool isMenuActive;

    void Start()
    {
        menuPanel.SetActive(false); // Hide the menu at start
        ToggleCursorState(false);
        isMenuActive = false;
    }

    void Update()
    {
        // Toggle the menu when Escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMenuActive = !menuPanel.activeSelf;
            menuPanel.SetActive(!menuPanel.activeSelf);
            ToggleCursorState(isMenuActive);
            AdjustSensitivity();
            AdjustVolume();

            // Optionally, pause the game if the menu is active
            Time.timeScale = menuPanel.activeSelf ? 0 : 1;
        }
    }

    void ToggleCursorState(bool isMenuActive)
    {
        if (isMenuActive)
        {
            // Show the cursor and unlock it so the player can interact with the menu
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            crosshair.SetActive(false);
        }
        else
        {
            // Hide the cursor and lock it for normal gameplay
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            crosshair.SetActive(true);
        }
    }

    public void AdjustSensitivity()
    {
        mouseSensitivityMulti = sensitivitySlider.value;
    }

    public void AdjustVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }

    // Call these methods on slider value change or button click
}
