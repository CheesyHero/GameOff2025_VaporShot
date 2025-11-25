using UnityEngine;

public class WallCheckParent : MonoBehaviour
{
    public bool hit;
    private RaycastGetter[] children;

    private void Awake()
    {
        children = GetComponentsInChildren<RaycastGetter>();
    }
    private void Update()
    {
        hit = GetHit();
    }

    public bool GetHit()
    {
        foreach (var child in children) 
            if (child.Hit) return true; 

        return false;
    }

}
