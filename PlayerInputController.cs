using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public static PlayerInputController Player { get; private set; }
    private PlayerControls input;

    public InputAction jumpAction;
    public InputAction shootAction;
    public InputAction reloadAction;
    public InputAction cancelAction;
    public Vector2 Move;
    public Vector2 Look;
    public bool JumpPressed;
    public bool FireHeld;
    public bool SprintHeld;

    void Awake()
    {
        input = new PlayerControls();

        input.Player.Move.performed += ctx => Move = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += ctx => Move = Vector2.zero;

        input.Player.Look.performed += ctx => Look = ctx.ReadValue<Vector2>();
        input.Player.Look.canceled += ctx => Look = Vector2.zero;

        input.Player.Jump.performed += ctx => JumpPressed = true;
        input.Player.Jump.canceled += ctx => JumpPressed = false;

        input.Player.Sprint.performed += ctx => SprintHeld = true;
        input.Player.Sprint.canceled += ctx => SprintHeld = false;

        input.Player.Attack.performed += ctx => FireHeld = true;
        input.Player.Attack.canceled += ctx => FireHeld = false; 

        jumpAction = input.FindAction("Jump");
        shootAction = input.FindAction("Attack");
        reloadAction = input.FindAction("Reload");
        cancelAction = input.FindAction("Escape");

        Player = this;
    }

    void OnEnable() => input.Player.Enable();
    void OnDisable() => input.Player.Disable();
}
