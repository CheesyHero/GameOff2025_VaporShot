using UnityEngine;

public class BalloonDummy : Enemy
{
    [Header("Balloon Attributes")]
    public GameObject deathEffect;

    public override void Defeat()
    {
        if (deathEffect) Instantiate(deathEffect, transform.position, transform.rotation);

        base.Defeat();
    }
}
