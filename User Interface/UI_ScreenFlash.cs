using UnityEngine;

public class UI_ScreenFlash : UI_ImageSetter
{
    public float durationWeight = 10f;
    public Color damageColor = Color.red;
    public Color endColor = Color.clear;
     
    private void Start()
    {
        var player = PlayerWalk.player;
        if (player) player.Health.OnHit += Flash;

        image.color = Color.clear;
    }

    public void Flash(float damage)
    {
        if (damage <= 0) return;

        AnimateColor(damageColor, endColor, damage / 100f * durationWeight);
    }
}
