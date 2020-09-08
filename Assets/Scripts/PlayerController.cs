//Hugo Lindroth 2020
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region Fields

    //public values


    //serialized values
    [SerializeField, Range(0, 1000)] float movementSpeed = 10f;

    //references
    Rigidbody2D body;
    Weapon currentWeapon;
    [SerializeField] private WeaponsManager weaponsManager;

    //private fields
    Vector2 movementInput = Vector2.zero;
    Vector2 mouseInput = Vector2.zero;
    private bool firing = false;

    #endregion Fields

    //Methods native to Unity
    #region UnityMethods
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        body = GetComponent<Rigidbody2D>();
        weaponsManager = GetComponentInChildren<WeaponsManager>();
        weaponsManager.shootPoint = this.transform;
        currentWeapon = weaponsManager.GetNextWeapon();
    }

    private void Update() {
        if (firing) {
            currentWeapon.Fire();
        }
    }

    private void FixedUpdate()
    {
        if(mouseInput != Vector2.zero)
        {
            body.velocity = mouseInput * (movementSpeed * Time.deltaTime);
        }
        else
        {
            body.velocity = movementInput * (movementSpeed * Time.deltaTime);
        }
        mouseInput = Vector2.zero;
    }

    #endregion UnityMethods

    //Methods for Input System
    #region Input
    public void GetMovementInput(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    public void GetMouseMovementInput(InputAction.CallbackContext ctx)
    {
        mouseInput += ctx.ReadValue<Vector2>();
    }

    public void GetShootInput(InputAction.CallbackContext ctx) {
        firing = ctx.ReadValueAsButton();
    }

    #endregion Input
}
