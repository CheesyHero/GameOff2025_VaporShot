using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public Weapon weapon;

    public bool active = true;
    public bool fire = false;
    [SerializeField]
    protected float timer = 2f;
    public float shootTimeMin = 1f;
    public float shootTimeMax = 3f;

    private void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
        else
        {
            fire = !fire;
            timer = Random.Range(shootTimeMin, shootTimeMax);
        }

        FireWeapon();
    }

    public void FireWeapon()
    {
        if (active && fire) TryShoot();
    }

    public void TryShoot()
    {
        if(weapon) weapon.Shoot();
    }

    public void TryReload()
    {
        if(weapon) weapon.Reload();
    }
}
