using System.Collections;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour, IEnemyAttack
{
    [SerializeField] private GameObject _hitbox;
    [SerializeField] public MeleeAttackData Data;


    [SerializeField] private MeshRenderer _renderer;

    private bool _hitboxActive;
    private float _nextAttackTime;

    private void Awake()
    {
        _renderer = GetComponentInChildren<MeshRenderer>();
        if (_hitbox == null)
        {
            Debug.LogError("[EnemyMeleeAttack] Falta asignar hitbox", this);            
        }
        else
        {
            _hitbox.SetActive(true);
            _renderer.enabled = false;
        }
    }

    public void Attack()
    {        
        if (Time.time < _nextAttackTime || _hitboxActive || _hitbox == null)
            return;

        _nextAttackTime = Time.time + Data.attackCooldown;
        StartCoroutine(ActivateHitbox());
    }

    private IEnumerator ActivateHitbox()
    {
        _hitboxActive = true;
        _hitbox.SetActive(true);
        _renderer.enabled = true;

        yield return new WaitForSeconds(Data.activeTime);

        _hitbox.SetActive(false);
        _hitboxActive = false;
        _renderer.enabled = false;
    }    
}