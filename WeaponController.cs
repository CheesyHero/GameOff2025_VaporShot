using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    public PlayerWalk parent;
    public PlayerInputController input;
    public Weapon weapon;
    public Vector3 hitPoint = new();

    private void Update()
    {
        if (parent.Health.defeated) return;

        SetHitPointToRay();

        if (input.FireHeld)
            TryShoot();
        if (input.reloadAction.WasPressedThisFrame())
            TryReload();
    }
    private void SetHitPointToRay()
    {
        Ray ray = new(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            hitPoint = hit.point;
        }
        else hitPoint = Vector3.zero;
    }

    public void TryShoot()
    {
        if (weapon) weapon.Shoot(hitPoint); 
    }
    public void TryReload()
    {
        if (weapon) weapon.Reload();
    }
}
