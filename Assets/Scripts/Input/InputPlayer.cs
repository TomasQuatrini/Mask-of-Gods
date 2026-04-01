using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    public static InputPlayer Instance { get; private set; }

    [Header("propiedades para networking")]
    public Vector3 CurrentMove { get; private set; }
    public Vector2 CurrentDelta { get; private set; }
    public bool IsRunning { get; private set; }
    public bool IsJumping { get; private set; }
    public bool WantsToPickup { get; private set; }

    public bool TakeDamaged { get; private set; } //provisorio
    public bool ConsumePotion_Stamina { get; private set; }
    public bool ConsumePotion_Health { get; private set; }


    [SerializeField] private KeysMove _keys;

    public event Action<Vector3> OnMove;
    public event Action<Vector2> OnDelta;
    public event Action<bool> OnRun;
    public event Action OnJump;
    public event Action OnAttack;
    public event Action OnSpecial1;
    public event Action ConsumeHealthPotion;
    public event Action ConsumeStaminaPotion;

    private Vector3 _moveInput;

    [Header("Latches for networking")]
    private bool _wantsToPickupLatch;
    private bool _isJumpingLatch;
    private bool _takeDamagedLatch;
    private bool _consumePotionStaminaLatch;
    private bool _consumePotionHealthLatch;

    private Vector2 _axisCamera;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    #region Properties with Latches
    public bool ConsumeWantsToPickup()
    {
        if (_wantsToPickupLatch)
        {
            _wantsToPickupLatch = false;
            return true;
        }
        return false;
    }

    public bool ConsumeIsJumping()
    {
        if (_isJumpingLatch)
        {
            _isJumpingLatch = false;
            return true;
        }
        return false;
    }

    public bool ConsumeTakeDamaged()
    {
        if (_takeDamagedLatch)
        {
            _takeDamagedLatch = false;
            return true;
        }
        return false;
    }

    public bool ConsumePotionStamina()
    {
        if (_consumePotionStaminaLatch)
        {
            _consumePotionStaminaLatch = false;
            return true;
        }
        return false;
    }

    public bool ConsumePotionHealth()
    {
        if (_consumePotionHealthLatch)
        {
            _consumePotionHealthLatch = false;
            return true;
        }
        return false;        
    }
    #endregion

    void Update()
    {
        GetMoving();
        GetDelta();       
        GetRunning();
        GetJumping();
        GetPickup();
        GetTakeDamaged();
        GetConsumePotionHealth();
        GetConsumePotionStamina();
        SetLatches();
        GetAttacks();
    }

    private void SetLatches()
    {
        if (WantsToPickup)
        {
            _wantsToPickupLatch = true;
            WantsToPickup = false;
        }
        if (IsJumping)
        {
            _isJumpingLatch = true;
            IsJumping = false;
        }
        if (ConsumePotion_Health)
        {
            _consumePotionHealthLatch = true;
            ConsumePotion_Health = false;
        }
        if (ConsumePotion_Stamina)
        {
            _consumePotionStaminaLatch = true;
            ConsumePotion_Stamina = false;
        }
        if (TakeDamaged)
        {
            _takeDamagedLatch = true;
            TakeDamaged = false;
        }
    }



    #region GetInputs

    private Vector3 GetWorldMoveFromCamera(Vector3 localMove)
    {
        if (CameraFollow.Instance == null || localMove == Vector3.zero)
        {
            return localMove;
        }
        var camera = CameraFollow.Instance.transform;
        Vector3 camForward = camera.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = camera.right;
        camRight.y = 0f;
        camRight.Normalize();

        Vector3 worldMove = camForward * localMove.z + camRight * localMove.x;
        if (worldMove.sqrMagnitude > 1f)
        {
            worldMove.Normalize();
        }
        return worldMove;
    }

    private void GetDelta()
    {
        _axisCamera = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        CurrentDelta = _axisCamera;
        OnDelta?.Invoke(CurrentDelta);
    }
    private void GetMoving()
    {
        _moveInput = Vector3.zero;
        if (Input.GetKey(_keys.up))
            _moveInput.z += 1;
        if (Input.GetKey(_keys.down))
            _moveInput.z -= 1;
        if (Input.GetKey(_keys.left))
            _moveInput.x -= 1;
        if (Input.GetKey(_keys.right))
            _moveInput.x += 1;

        if (_moveInput.sqrMagnitude > 1f)
        { 
            _moveInput.Normalize();
        }

        Vector3 worldMove = GetWorldMoveFromCamera(_moveInput);

        CurrentMove = worldMove;

        OnMove?.Invoke(worldMove);
    }
    private void GetRunning()
    {
        bool pressed = Input.GetKey(_keys.run);
        IsRunning = pressed;
        OnRun?.Invoke(pressed);
    }
    private void GetJumping()
    {
        if (Input.GetKeyDown(_keys.jump))
        {
            OnJump?.Invoke();
            IsJumping = true;
        }
        else
        {
            IsJumping = false;
        }
    }

    private void GetPickup()
    {
        if (Input.GetKeyDown(_keys.pickup))
        {
            WantsToPickup = true;
        }
        else
        {
            WantsToPickup = false;
        }
    }

    private void GetTakeDamaged()
    {
        if(Input.GetKeyDown(_keys.takedamage))
        {
            TakeDamaged = true;
        }
        else
        {
            TakeDamaged = false;
        }
    }

    private void GetConsumePotionStamina()
    {
        if (Input.GetKeyDown(_keys.consumeS))
        {
            ConsumePotion_Stamina = true;
            ConsumeStaminaPotion?.Invoke();
        }
        else
        {
            ConsumePotion_Stamina = false;
        }
    }

    private void GetConsumePotionHealth()
    {
        if (Input.GetKeyDown(_keys.consumeH))
        {
            ConsumePotion_Health = true;
            ConsumeHealthPotion?.Invoke();
        }
        else
        {
            ConsumePotion_Health = false;
        }
    }  

    private void GetAttacks()
    {
        if (Input.GetKeyDown(_keys.attack))
        {
            OnAttack?.Invoke();
        }
        if (Input.GetKeyDown(_keys.specialAttack1))
        {
            OnSpecial1?.Invoke();
        }
    }

    #endregion
}
