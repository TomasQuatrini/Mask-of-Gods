using System;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    public static InputPlayer Instance { get; private set; }

    [Header("propiedades para networking")]
    public Vector3 CurrentMove { get; private set; }
    public bool IsRunning { get; private set; }
    public bool IsJumping { get; private set; }

    [SerializeField] private KeysMove _keys;    

    public event Action<Vector3> OnMove;   
    public event Action<bool> OnRun;       
    public event Action OnJump;
    public event Action OnAttack;
    public event Action OnSpecial1;

    private Vector3 _moveInput;
    private bool _isRunning;
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    void Update()
    {
        _moveInput = Vector3.zero;

        // Movimiento
        if (Input.GetKey(_keys.up))
            _moveInput.z += 1;
        if (Input.GetKey(_keys.down))
            _moveInput.z -= 1;
        if (Input.GetKey(_keys.left))
            _moveInput.x -= 1;
        if (Input.GetKey(_keys.right))
            _moveInput.x += 1;

        CurrentMove = _moveInput;
        _isRunning = Input.GetKey(_keys.run);
        IsRunning = _isRunning;

        OnRun?.Invoke(_isRunning);
        OnMove?.Invoke(_moveInput);       

        if (Input.GetKeyDown(_keys.attack))
        { 
            Debug.Log("LLamando Ataque");
            OnAttack?.Invoke(); 
        }
        if (Input.GetKeyDown(_keys.specialAttack1))
        {
            Debug.Log("LLamando Ataque Especial");
            OnSpecial1?.Invoke();
        }
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
}

