using UnityEngine;

public class ViewTiltHandle : MonoBehaviour
{
    public Transform handle;
    public PlayerWalk controller;
    public PlayerEvironmentDetection detector;
    public float smoothing = 0.2f;
    public Vector3 upTiltEuler = new(15f, 0, 0);
    public Vector3 rightTiltEuler = new(0, 0, 15); 
    private Vector3 targetPoint;
    private Vector3 velocity = new(); 
    private Vector3 upDebug = new();
    private Vector3 rDebug = new();
    private Vector3 lDebug = new();

    private void Awake()
    {
        if(!controller) controller = GetComponentInParent<PlayerWalk>();
        if(!detector) detector = GetComponentInParent<PlayerEvironmentDetection>();
        if (!handle) handle = transform;
    }

    private void Update()
    {
        targetPoint = Vector3.SmoothDamp(targetPoint, GetDesired(), ref velocity, smoothing);
        transform.localEulerAngles = targetPoint;

        upDebug = (detector.CheckForwardOnly() ? upTiltEuler : Vector3.zero);
        rDebug = (detector.CheckWallsOnly() && detector.rightHit ? -rightTiltEuler : Vector3.zero);
        lDebug = (detector.CheckWallsOnly() && detector.leftHit ? rightTiltEuler : Vector3.zero);
    }
    Vector3 GetDesired()
    {
        if (controller.isOriginGrounded) return Vector3.zero;

        else if (controller.IsSprinting()) return upDebug + rDebug + lDebug;

        else return Vector3.zero;
    }
}
