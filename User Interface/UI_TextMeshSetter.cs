using UnityEngine;
using TMPro;

public class UI_TextMeshSetter : MonoBehaviour
{
    [SerializeField]
    protected TextMeshProUGUI textMesh;

    private void Awake()
    {
        if (!textMesh) textMesh = GetComponent<TextMeshProUGUI>();
    }
    public void SetText(string input)
    {
        if (textMesh) textMesh.text = input;
    }
}
