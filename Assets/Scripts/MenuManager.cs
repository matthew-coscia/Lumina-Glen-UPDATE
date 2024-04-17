using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel; 
    public Slider sensitivitySlider; 
    public Slider volumeSlider; 
    public float mouseSensitivityMulti;
    public GameObject crosshair;
    public bool isMenuActive;

    void Start()
    {
        menuPanel.SetActive(false);
        ToggleCursorState(false);
        isMenuActive = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMenuActive = !menuPanel.activeSelf;
            menuPanel.SetActive(!menuPanel.activeSelf);
            ToggleCursorState(isMenuActive);
            AdjustSensitivity();
            AdjustVolume();

            Time.timeScale = menuPanel.activeSelf ? 0 : 1;
        }
    }

    void ToggleCursorState(bool isMenuActive)
    {
        if (isMenuActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            crosshair.SetActive(false);
        }
        else
        {
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
}
