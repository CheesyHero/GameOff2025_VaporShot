using UnityEngine;
using UnityEngine.Events;

public class CharacterVitals : MonoBehaviour
{ 
    public bool defeated = false;
    public float max_health = 100f;
    public float health;
    public float max_armor = 100f;
    public float armor;

    public UnityAction<float> OnHit;
    public UnityAction OnDeath;

    private void Awake()
    {
        health = max_health;
    }

    public void TakeDamage(float damage)
    {
        if (defeated) return;

        HitArmor(ref damage);
        HitHealth(damage);
        CheckDeath();
    }
    private void HitArmor(ref float damage)
    {
        if (armor > 0)
        {
            damage = Mathf.Max(damage - 5, damage / 2);
            armor -= damage;

            if (armor < 0)
            {
                damage = Mathf.Abs(armor);
                armor = 0;
            }
            else damage = 0;

            if(armor < 0) armor = 0;
        }
    }
    private void HitHealth(float damage)
    { 
        if (damage > 0)
        {
            health -= damage;
            OnHit?.Invoke(damage);
        }

        if (health < 0) health = 0;
    }
    public void GainHealth(float amount)
    {
        health += amount;
        if (health > max_health)
            health = max_health;
    }
    private void CheckDeath()
    { 
        if (health <= 0 && !defeated)
        {
            defeated = true;
            OnDeath?.Invoke();
        }
    }

    public float GetHealthPercent()
    {
        return health / max_health;
    }
}
