using UnityEngine; 
using System.Collections.Generic;

[SelectionBase]
public class Enemy : MonoBehaviour
{
    readonly protected static List<Enemy> instances = new();
    public static Enemy[] AllEnemies 
    { get { return instances.ToArray(); } }
    public static int Count
    { get { return instances.Count; } }

    public CharacterVitals Health { get; private set; }

    private void Awake()
    {
        Health = GetComponent<CharacterVitals>();
        Health.OnDeath += Defeat;

        Initiate();
    }
    private void OnDestroy()
    {
        Exit();
    }
    public virtual void Initiate()
    {
        instances.Add(this);
    }
    public virtual void Exit()
    {
        instances.Remove(this);
    } 
    public virtual void Defeat()
    {
        Destroy(gameObject);
    }
}
