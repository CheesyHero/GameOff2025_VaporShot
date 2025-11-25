using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    public Transform orientation;
    public float sensX;
    public float sensY; 
    float xRot;
    float yRot; 
    private PlayerControls controls;
    private Vector2 lookInput;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    private void OnEnable() => controls.Player.Enable();
    private void OnDisable() => controls.Player.Disable();

    void Start()
    {  
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
     
    void Update()
    {
        if (PlayerWalk.player.Health.defeated) return;

        Vector2 look = lookInput;

        float mouseY = look.y * Time.deltaTime * sensX;
        float mouseX = look.x * Time.deltaTime * sensX;

        yRot += mouseX;
        xRot -= mouseY; 
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        orientation.rotation = Quaternion.Euler(0, yRot, 0);
    }

}
