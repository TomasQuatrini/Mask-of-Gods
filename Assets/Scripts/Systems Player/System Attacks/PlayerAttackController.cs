using System;
using System.Collections;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private GameObject _activeWeapon;
    private GameObject _activeFirstMask;
    //private Rigidbody _weaponRb;
    //private PlayerCollisionController _playerCollisionController;
    private PlayerAttackCommand _attackCommand;
    private AresMaskCommand _aresMaskCommand;
    [SerializeField]  private AttackSetting _setting;
    [SerializeField] private MaskSetting _AresSetting;
    [SerializeField] private InputPlayer _inputPlayer;
    private Coroutine _attackCooldownCoroutine;
    private Coroutine _specialAttackCoroutine;

    private bool _isAttackingAres = false;

    private void Awake()
    {
        _activeWeapon = GameObject.FindWithTag("PlayerWepon"); //hacer con try
        _activeFirstMask = GameObject.FindWithTag("PlayerMask");
        //_playerCollisionController = GetComponent<PlayerCollisionController>();
    }
    private void Start()
    {
        _attackCommand = new PlayerAttackCommand(_setting, _activeWeapon, this);
        //if (_activeWeapon is not null) { _weaponRb = _activeWeapon.GetComponent<Rigidbody>(); }
        _inputPlayer.OnAttack += HandleBasicAttack;
        _inputPlayer.OnSpecial1 += HandleSpecialAttack1;
        _aresMaskCommand = new AresMaskCommand(_AresSetting, _activeFirstMask, this);

    }

    private void HandleBasicAttack()
    {
        if (_attackCooldownCoroutine is null)
        {
            _attackCooldownCoroutine = StartCoroutine(AttackCooldown(_setting.attackCooldown));
        }
        else return;
        if (_attackCommand == null)
            _attackCommand = new PlayerAttackCommand(_setting, _activeWeapon, this);

        StartCoroutine(_attackCommand.AttackCoroutine());
    }
    private IEnumerator AttackCooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _attackCooldownCoroutine = null;
        _specialAttackCoroutine = null;
    }

    private void HandleSpecialAttack1()
    {
        Debug.Log("Llamando Ataque Ares");
        if (_specialAttackCoroutine is null)
        {
            _specialAttackCoroutine = StartCoroutine(AttackCooldown(_AresSetting.cooldown));
        }
        else return;
        if (_aresMaskCommand == null)
            _aresMaskCommand = new AresMaskCommand(_AresSetting, _activeFirstMask, this);

        StartCoroutine(_aresMaskCommand.AttackCoroutine());
    }
    private void OnDrawGizmos()
    {
        if (!_isAttackingAres) return;

        var collider = _activeFirstMask.GetComponent<BoxCollider>();
        if (collider == null) return;

        Gizmos.color = new Color(1f, 0f, 0f, 0.35f); // rojo semitransparente
        Matrix4x4 matrix = Matrix4x4.TRS(collider.transform.position, collider.transform.rotation, collider.transform.lossyScale);
        Gizmos.matrix = matrix;
        Gizmos.DrawCube(collider.center, collider.size);
    }
    public void SetAttacking(bool state)
    {
        _isAttackingAres = state;
    }
}
