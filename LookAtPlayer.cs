using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{ 
    public Transform target;
    public float speed = 5f;

    private void Start()
    {
        target = PlayerWalk.player.transform;
    }

    private void Update()
    {
        if (!target) return;
         
        Vector3 d = target.position - transform.position;
         
        if (d.sqrMagnitude > 0.0001f)
        {
            Quaternion tRot = Quaternion.LookRotation(d); 
            transform.rotation = 
                Quaternion.RotateTowards(transform.rotation, tRot, speed * Time.deltaTime);
        }
    }
}
