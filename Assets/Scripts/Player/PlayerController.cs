//Hugo Lindroth 2020

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region Fields

    //public values
    public int HP
    {
        get
        {
            return hp;
        }
    }

    //events
    public UnityEvent OnDeath;
    public DamageEvent OnDamage;

    public class DamageEvent : UnityEvent<int>
    {
    }


    //serialized values
    [SerializeField, Range(0, 1000)] float movementSpeed = 10f;
    [SerializeField, Range(1, 100)] int hp;

    //references
    Rigidbody2D body;
    public Weapon currentWeapon;
    [SerializeField] private WeaponsManager weaponsManager;
    [SerializeField] Shield shield;
    [SerializeField] private float swapCoolDownTimer = 0.5f;

    //private fields
    Vector2 movementInput = Vector2.zero;
    Vector2 mouseInput = Vector2.zero;
    private bool firing = false;
    private bool swapping = false;
    private int swapNextOrPrevious = 0;

    #endregion Fields

    //Public non-input methods
    public void TakeDamage(int damage)
    {
        if (!shield.active && hp > 0)
        {
            OnDamage.Invoke(damage);
            hp -= damage;
            if (hp < 1)
            {
                OnDeath.Invoke();
            }
        }
    }

    //Methods native to Unity
    #region UnityMethods
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        body = GetComponent<Rigidbody2D>();
        weaponsManager = GetComponentInChildren<WeaponsManager>();
        weaponsManager.shootPoint = this.transform;
        currentWeapon = weaponsManager.SwapWeapon(swapNextOrPrevious);
        OnDamage = new DamageEvent();
    }

    private void Update() {
        if (firing && currentWeapon != null) {
            currentWeapon.Fire();
        }

        if (swapping) {
            
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

    public void GetShieldInput(InputAction.CallbackContext ctx)
    {
        shield.ActivateShield();
    }

    public void GetWeaponSwapInput(InputAction.CallbackContext ctx) {
        swapNextOrPrevious = (int)ctx.ReadValue<Vector2>().y;
        if (swapNextOrPrevious != 0 && !swapping) {
            swapping = true;
            currentWeapon = weaponsManager.SwapWeapon(swapNextOrPrevious);
            StartCoroutine(SwapCoolDown());
        }
    }
    
    //timer mehods
    IEnumerator SwapCoolDown() {
        yield return new WaitForSeconds(swapCoolDownTimer);
        swapping = false;
    }

    #endregion Input
}
