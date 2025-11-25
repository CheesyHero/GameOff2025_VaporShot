using UnityEngine;
using UnityEngine.UI;

public class DisplayModeDropdown : MonoBehaviour
{
    public Dropdown displayDropdown;

    void Start()
    {
        displayDropdown.ClearOptions();

        var modes = new System.Collections.Generic.List<string>()
        {
            "Windowed",
            "Borderless Window",
            "Fullscreen"
        };

        displayDropdown.AddOptions(modes);
        displayDropdown.value = (int)Screen.fullScreenMode;
        displayDropdown.RefreshShownValue();

        displayDropdown.onValueChanged.AddListener(SetDisplayMode);
    }

    public void SetDisplayMode(int index)
    {
        switch (index)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;

            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow; // borderless
                break;

            case 2:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
        }
    }
}
