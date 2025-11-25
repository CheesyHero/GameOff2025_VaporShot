using UnityEngine;

public class PlayerWallRun : MonoBehaviour
{
    public float wallJumpStrength = 16f;
    public float verticalJumpStrength = 12f;
    public float gravityShift = 2f; 
    public bool active;
    PlayerWalk parent;
    PlayerEvironmentDetection PEDetect; 

    private void Awake()
    { 
        parent = GetComponent<PlayerWalk>();
        PEDetect = GetComponentInChildren<PlayerEvironmentDetection>();

        parent.groundCheckOverride += GroundCheck;
        parent.jumpOverride += Jump;
    } 
    private void Update()
    { 
        active = PEDetect.CheckWallsOnly() && parent.IsSprinting() && !parent.isOriginGrounded;

        if (active) 
            parent.ApplyForceOverTime(transform.up * gravityShift); 

        CheckEntry();
    }
    bool lastCheck = false;
    private void LateUpdate()
    {
        lastCheck = active;
    } 
    private void CheckEntry()
    {
        if(active == true && lastCheck == false)
        {
            EnterWallClimb();
            lastCheck = true;
        }
    }
    private float lastWallExit = 0;
    public float wallTimeBuffer = 0.25f;
    private void EnterWallClimb()
    {
        if (Time.time - lastWallExit < wallTimeBuffer)
            return;

        // IF the player
        else if ((PEDetect.leftHit && usedLeft) || (PEDetect.rightHit && usedRight))
        {
            parent.ResetJumps(); // Gain regular jump
        }

        else
        {
            parent.ResetJumps(-1); // Gain one extra jump
            Debug.Log("Reset jump");
        }

        if (PEDetect.rightHit)
            usedLeft = false;

        if (PEDetect.leftHit)
            usedRight = false;
    }

    private bool Jump()
    {
        if (active) // In the air / on a wall
        { 
            Push();
            return true;
        }
        else return false; 
    }
    [SerializeField]
    bool usedLeft = false;
    [SerializeField]
    bool usedRight = false;
    private void Push()
    {
        parent.ApplyImpulse(GetPush(), false);
        //parent.StartJumpCooldown();
    }
    private Vector3 GetPush()
    {
        if (parent.IsSprinting() && !parent.isOriginGrounded)
        {
            if (PEDetect.rightHit && !PEDetect.leftHit && !usedRight)
            {
                usedRight = true;
                usedLeft = false;
                return -transform.right * wallJumpStrength;
            }
            else if (PEDetect.leftHit && !PEDetect.rightHit && !usedLeft)
            {
                usedLeft = true;
                usedRight = false;
                return transform.right * wallJumpStrength;
            }

            lastWallExit = Time.time;
        } 

        return Vector3.zero;
    }

    private void GroundCheck(ref bool current)
    {
        if (active)
        {
            if (PEDetect.leftHit && !usedLeft)
                current = true;
            else if (PEDetect.rightHit && !usedRight)
                current = true;
        }
        if(parent.isVirtuallyGrounded)
        {
            if (!lastCheck)
                parent.ResetJumps();
        } 
        if (parent.isOriginGrounded)
        {
            usedLeft = false;
            usedRight = false;
        }
    }
}
