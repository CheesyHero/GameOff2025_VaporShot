using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    public float gravityShift = 2f;
    public float decayRate = 6f;
    public float rechargeate = 6f;
    public bool active;
    [SerializeField]
    private float sticky = 2f;
    PlayerWalk parent;
    PlayerEvironmentDetection PEDetect;
    CharacterController controller;
     
    bool lastActive = false;
    bool hasRefreshedJumpThisEntry = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        parent = GetComponent<PlayerWalk>();
        PEDetect = GetComponentInChildren<PlayerEvironmentDetection>();

        parent.groundCheckOverride += GroundCheck;
    }
    private void Start()
    {
        sticky = gravityShift;
    }
    private void Update()
    {
        active = PEDetect.CheckForwardOnly() && parent.IsSprinting() && !controller.isGrounded;

        // Detect transition into climb state
        if (active && !lastActive)
        {
            // Refresh jumps once on entry
            if (!hasRefreshedJumpThisEntry)
            {
                parent.ResetJumps();
                hasRefreshedJumpThisEntry = true;
            }
        }
         
        if (!active && lastActive) 
            hasRefreshedJumpThisEntry = false;    

        if (active)
        {
            parent.ApplyForceOverTime(transform.up * sticky);
            if (sticky > 0) sticky -= decayRate * Time.deltaTime;
        }
        else if (sticky < gravityShift)
            sticky += rechargeate * Time.deltaTime;
    }
    private void LateUpdate()
    {
        lastActive = active;
    }

    private void GroundCheck(ref bool current)
    {
        if (active) current = true;
        if (controller.isGrounded)
        {
            sticky = gravityShift;
            hasRefreshedJumpThisEntry = false;
        }
    }
}
