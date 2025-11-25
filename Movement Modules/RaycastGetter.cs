using UnityEngine;

public class RaycastGetter : MonoBehaviour
{
    [SerializeField]
    private bool h = false;
    public bool Hit { get { return h; } }

    public LayerMask layers;
    public float range = 1f; 

    private void Update()
    {
        h = Fire();
    }

    public bool Fire()
    {
        Ray ray = new(transform.position, transform.forward);
        if(Physics.Raycast(ray, out RaycastHit hit, range, layers))
        {
            DrawRay(Color.cyan);
            return (layers.value & (1 << hit.transform.gameObject.layer)) != 0;
        }

        DrawRay(Color.red);
        return false;
    } 

    public void DrawRay(Color c)
    {
        Debug.DrawRay(transform.position, transform.forward * range, c);
    }
}
