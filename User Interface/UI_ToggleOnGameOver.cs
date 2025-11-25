using UnityEngine;

public class UI_ToggleOnGameOver : MonoBehaviour
{
    public bool defaultSetting = true;

    private void Awake()
    {
        GameManager.OnGameOver += ToggleDisplay;
    }
    private void Start()
    {
        gameObject.SetActive(defaultSetting);
    }
    private void OnDestroy()
    {
        GameManager.OnGameOver -= ToggleDisplay;
    }

    public void ToggleDisplay()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
