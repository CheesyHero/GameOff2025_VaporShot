using UnityEngine;

public class UI_HealthDisplay : MonoBehaviour
{
    [SerializeField]
    protected UI_TextMeshSetter healthText;
    [SerializeField]
    protected UI_FillBar fillBar;

    private void Update()
    {
        SetHealth(PlayerWalk.player.Health);
    }

    public void SetHealth(CharacterVitals input)
    {
        if (input == null) return;

        healthText.SetText("HP: " + input.health.ToString("0") + " / " + input.max_health.ToString("0"));
        fillBar.SetFillAmount(input.GetHealthPercent());
    }
}
