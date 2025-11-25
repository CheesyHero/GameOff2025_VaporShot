using UnityEngine;
using TMPro;

public class EnemiesRemaining : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        SetCount();
    }

    public void SetCount()
    {
        if (!text) return;

        int count = Enemy.Count;
        int total = GameManager.EnemyCount;
        text.text = count > 0 ? "Enemies Remaining:\n" + count + " (" + total + ")": "No Enemies";
    }
}
