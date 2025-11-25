using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_ChangeSceneButton : MonoBehaviour
{
    public void BUTTON_ChangeeScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void BUTTON_Quit()
    {
        Application.Quit();
    }
}
