using UnityEngine;
using TMPro;

public class AmmoCount : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (PlayerWalk.player != null)
            SetAmmo(PlayerWalk.player.Weapons.weapon);
    }

    public void SetAmmo(Weapon weapon)
    {
        if (!text || !weapon) return;

        if (!weapon.IsReloading) text.text = weapon.CurrentAmmo + " / " + weapon.maxAmmo;
        else text.text = "RELOAD";
    }
}
