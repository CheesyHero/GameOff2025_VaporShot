using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerWalk : MonoBehaviour
{
    public static PlayerWalk player;
    public CharacterVitals Health { get; private set; }

    public CharacterController Controller { get; private set; }
    public PlayerInputController Input { get; private set; }
    public WeaponController Weapons { get; private set; }

    public float walkSpeed = 6f;
    public float sprintSpeed = 10f;
    public float acceleration = 0.125f;
    public float air_accel = 0.5f;
    [Space(2)]
    public LayerMask groundLayer;
    public int jumps = 2;
    public float jumpStrength = 6f;
    [Space(2)]
    public float gravity = 6f;
    public float floorRadiusCheck = 0.5f;
    public bool isOriginGrounded;
    public bool isVirtuallyGrounded = true;

    public delegate bool JumpOverride();
    public JumpOverride jumpOverride;
    public delegate void GroundCheckOverride(ref bool current);
    public GroundCheckOverride groundCheckOverride;


    private void Awake()
    { 
        Controller = GetComponent<CharacterController>(); 
        Input = GetComponent<PlayerInputController>();
        Weapons = GetComponentInChildren<WeaponController>();
        Health = GetComponent<CharacterVitals>();

        if (player != null && player != this)
            Destroy(gameObject);
        else player = this;

        Health.OnDeath += EndGame;
    }

    private void Update()
    {
        if (Health.defeated) return;

        OutOfBounds();
        CheckGrounded();
        HeadCollision();

        Jump();
        Walk();
        FallPhysics();
    }
    public void EndGame()
    {
        GameManager.GameOver();
    }

    Vector3 momentum = new();
    Vector3 cur_vel_momentum = new();
    Vector3 dir = new(); 
    protected void Walk()
    { 
        dir = transform.right * Input.Move.x + transform.forward * Input.Move.y;
        momentum = Vector3.SmoothDamp(momentum, dir.normalized * (IsSprinting() ? sprintSpeed : walkSpeed), 
            ref cur_vel_momentum, isVirtuallyGrounded ? acceleration : air_accel);

        Controller.Move(Time.deltaTime * momentum);
    }
    public bool IsSprinting()
    {
        return Input.SprintHeld && Input.Move.y >= 0.25f;
    }

    Vector3 currentGravity = new();
    public Vector3 GetMomentum()
    {
        if (dir.magnitude > 0.1f)
        {
            Vector3 c = Controller.velocity;
            c.y *= 0.5f;
            return c;
        }

        else return transform.forward;
    }
    public float GetCurrentGForce {  get { return currentGravity.y; } }
    float j_time = 0;
    int j_counts = 0;
    public bool CanJump { get { return j_counts > 0 && j_time < 0; } }
    protected void Jump()
    {
        if (j_time > 0) j_time -= Time.deltaTime;

        if (j_counts < jumps && Input.jumpAction.WasPressedThisFrame())
        {  
            TriggerJump();
            jumpOverride?.Invoke();
        } 
    }
    public float jump_cooldown = 0.35f;
    public void StartJumpCooldown()
    {
        j_time = jump_cooldown;
        j_counts++;
    }
    public void TriggerJump(float mod = 1f)
    {
        KillGravity(jumpStrength * mod);
        StartJumpCooldown();
    }
    public void KillGravity(float force = 0)
    {
        currentGravity.y = force;
    }
    protected void FallPhysics()
    {
        CalculateYGravity();
        CalculateXZForce();

        Controller.Move(Time.deltaTime * currentGravity);
    }
    protected void CalculateYGravity()
    {
        if (!isVirtuallyGrounded) currentGravity.y -= gravity * gravity * Time.deltaTime;

        else if (isVirtuallyGrounded && !isOriginGrounded) 
            currentGravity.y -= 0.5f * gravity * gravity * Time.deltaTime; 

        else if (j_time <= 0 && isOriginGrounded) 
            currentGravity.y = -1; 
    }
    public void ResetJumps(int offset = 0)
    {
        j_counts = 0 + offset;
    }
    protected void CalculateXZForce()
    {
        float mult = isVirtuallyGrounded ? 10f : 1f;

        CurveValue(ref currentGravity.x, mult); 
        CurveValue(ref currentGravity.z, mult);
    }
    private void CurveValue(ref float val, float mult = 1f)
    {
        if (Mathf.Abs(val) > 0.1f)
            val -= val * Time.deltaTime * mult;

        else val = 0f;
    }

    protected void CheckGrounded()
    {
        isOriginGrounded = GroundOverlapSphere();
        isVirtuallyGrounded = Controller.isGrounded || isOriginGrounded; 
        groundCheckOverride?.Invoke(ref isVirtuallyGrounded);

        if (isOriginGrounded && j_time <= 0) ResetJumps();
    }
    protected bool GroundOverlapSphere()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, floorRadiusCheck);
        foreach(var c in hits)
        {
            if (c.gameObject.CompareTag("World")) return true;
        }

        return false;
    }

    public float outOfBoundsY = -10f;
    public void OutOfBounds()
    {
        if (transform.position.y < outOfBoundsY)
        {
            Controller.enabled = false;
            transform.position = Vector3.up * 5;
            Controller.enabled = true;
        }
    }

    public void ApplyImpulse(Vector3 force, bool kill)
    {
        if (kill) KillGravity(force.y);
        else currentGravity.y += force.y;

        force.y = 0;
        currentGravity += force;
    } 
    public void ApplyForceOverTime(Vector3 force)
    {
        Controller.Move(force * Time.deltaTime);
    }

    public void HeadCollision()
    {
        if (currentGravity.y <= 0) return;

        Ray ray = new(transform.position + Vector3.up * 2, Vector3.up);
        if(Physics.Raycast(ray, out RaycastHit hit, 0.2f))
        {
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("World")) // Check if transform is contained in the "World" layer
            {
                currentGravity.y -= Time.deltaTime * gravity * gravity;
            }
        }
    }
}
