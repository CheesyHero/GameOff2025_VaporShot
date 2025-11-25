using UnityEngine;

public class PlayerEvironmentDetection : MonoBehaviour
{
    public bool frontHit, rightHit, leftHit;
    [Header("Required Components")]
    [SerializeField]
    private WallCheckParent Front;
    [SerializeField]
    private WallCheckParent Right, Left;

    public bool CheckForwardOnly()
    {
        return frontHit && !rightHit && !leftHit;
    }

    public bool CheckWallsOnly()
    {
        return !frontHit && (rightHit || leftHit);
    }

    private void Update()
    {
        frontHit = Front ? Front.GetHit() : false;
        rightHit = Right ? Right.GetHit() : false;
        leftHit = Left ? Left.GetHit() : false;
    }
}
