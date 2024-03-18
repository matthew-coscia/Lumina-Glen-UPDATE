using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Michsky.UI.ModernUIPack
{
    public class ProgressBar : MonoBehaviour
    {
        // Content
        [Range(0, 100)] public float currentPercent;
        [Range(0, 100)] public int speed;

        // Resources
        public Image loadingBar;
        public TextMeshProUGUI textPercent;

        // Settings
        public bool isOn;
        public bool restart;
        public bool invert;

        void Start()
        {
            if (isOn == false)
            {
                loadingBar.fillAmount = currentPercent / 100;
                textPercent.text = ((int)currentPercent).ToString("F0") + "%";
            }
        }

        void Update()
        {
            if (isOn == true)
            {
                loadingBar.fillAmount = currentPercent / 100;
                textPercent.text = ((int)currentPercent).ToString("F0") + "%";
            }
        }
    }
}