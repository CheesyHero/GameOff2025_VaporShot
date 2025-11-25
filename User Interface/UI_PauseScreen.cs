using UnityEngine;

public class UI_PauseScreen : MonoBehaviour
{
    public GameObject handle;

    private void Start()
    {
        Time.timeScale = 1f;
        if (handle) handle.SetActive(false);
    }
    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
    private void Update()
    {
        if (PlayerInputController.Player.cancelAction.WasPressedThisFrame())
            ToggleHandle(); 
    }

    public void ToggleHandle()
    {
        if (!handle) return;

        handle.SetActive(!handle.activeInHierarchy);
        bool current = handle.activeInHierarchy;
        // current to TRUE is PAUSED

        Time.timeScale = current ? 0f : 1f;
        Cursor.lockState = current ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = current;
    }
}
