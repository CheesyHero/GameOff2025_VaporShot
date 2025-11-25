using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static readonly List<EnemySpawner> AllSpawners = new();

    public Enemy[] prefabs;
    public int maxSpawns = 3;
    public List<Enemy> spawns = new();
    public bool CanSpawn { get { return spawns.Count < maxSpawns; } }

    private void Awake()
    {
        AllSpawners.Add(this);
    }
    private void OnDestroy()
    {
        AllSpawners.Remove(this);
    }
    public static bool SpawnEnemy()
    {
        Debug.Log("Attempting to spawn enemy...");

        if(AllSpawners.Count == 0) return false;

        List<EnemySpawner> valid = new();
        foreach (var item in AllSpawners.ToArray()) 
            if(item.CanSpawn) valid.Add(item);

        if (valid.Count > 0)
            return valid[Random.Range(0, valid.Count)].Spawn();
        else return false;
    }

    public bool Spawn() // Returns success or failure to spawn
    {
        Debug.Log(name + " is attempting to spawn enemy from array with size: " + prefabs.Length);

        if (prefabs.Length <= 0 || !CanSpawn) return false;

        var i = Instantiate(GetSpawnPick(), transform.position, transform.rotation, transform);
        Debug.Log(name + " created instance : " + i.name);
        spawns.Add(i);
        i.Health.OnDeath += RemoveFromList;

        return true;
    }
    public Enemy GetSpawnPick()
    {
        if (prefabs.Length <= 0) return null;

        return prefabs[Random.Range(0, prefabs.Length)];
    }
    public void RemoveFromList()
    {
        foreach(var i in spawns.ToArray())
        {
            if(i.Health.defeated)
                spawns.Remove(i);
        }
    }
}
