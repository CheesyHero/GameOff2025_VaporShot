using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Projectile projectile;
    public Transform shootPoint;
    public ParticleSystem muzzleFlash;
    public AudioSampler sounds;
    [Header("Main")]
    public float rateOfFire = 10f;
    public int projectileCount = 1;
    public float projectileRange = 10f;
    public float projectileSpeed = 10f;
    public float accuracyMod = 15f;
    public float damage = 10f;
    public int maxAmmo = 24; 
    public int CurrentAmmo { get; private set; }
    public bool canShoot;
    [Space(2)]
    public float reloadTime = 2f;
    private float cooldown = 0;

    private void Start()
    {
        CurrentAmmo = maxAmmo;
    }
    private void Update()
    {
        canShoot = cooldown <= 0;

        if (cooldown > 0) cooldown -= Time.deltaTime;
    }

    public void Shoot(Vector3 hitPoint = new())
    {
        if (CurrentAmmo > 0 && !IsReloading)
            Fire(hitPoint);

        else Reload();
    }
    public void Reload()
    {
        if (CurrentAmmo < maxAmmo && !IsReloading)
            routine = StartCoroutine(ReloadingTime());
        else return;
    }
    Coroutine routine;
    public bool IsReloading { get { return routine != null; } }
    IEnumerator ReloadingTime()
    {
        TryReloadStartSound();

        float t = reloadTime;
        while (t > 0)
        {
            t -= Time.deltaTime;

            yield return null;
        }

        TryReloadEndSound();
        CurrentAmmo = maxAmmo;
        routine = null;
    }
    private void Fire(Vector3 hitPoint = new())
    {
        if (!canShoot) return;

        //Debug.Log("BANG");

        TryFireSound();
        PlayMuzzleFlashEffect();

        for (int i = 0; i < projectileCount; i++)
            CreateProjectile(hitPoint);

        CurrentAmmo--;

        if (rateOfFire <= 0) rateOfFire = 1f;
        cooldown = 1f / rateOfFire;
    }
    private void CreateProjectile(Vector3 hitPoint = new())
    {
        if (!projectile || !shootPoint) return;

        Projectile p = Instantiate(projectile, shootPoint.position, shootPoint.rotation);
        p.Orient(this, hitPoint);
    }
    public void PlayMuzzleFlashEffect()
    {
        if (muzzleFlash) muzzleFlash.Play();
    }
    public void TryFireSound()
    {
        if (sounds) 
            sounds.Play(0); 
    }
    public void TryReloadStartSound()
    {
        if (sounds) 
            sounds.Play(1); 
    } 
    public void TryReloadEndSound()
    {
        if (sounds) 
            sounds.Play(2); 
    }
}
