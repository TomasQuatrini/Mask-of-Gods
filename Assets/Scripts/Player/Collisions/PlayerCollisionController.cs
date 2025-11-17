using System;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{    
    private PlayerContext _ctx;
    private Collider _playerCollider;
    [SerializeField] private LayerMask groundLayer;

    public event Action PlayerCollidedWithGround;

    private void Awake()
    {
        _ctx = GetComponentInParent<PlayerContext>();
        _playerCollider = _ctx.Collider;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            PlayerCollidedWithGround?.Invoke();
        }
    }
}
