using System;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    private Collider _playerCollider;
    [SerializeField] private LayerMask groundLayer;

    public event Action PlayerCollidedWithGround;

    private void Awake()
    {
        _playerCollider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            PlayerCollidedWithGround?.Invoke();
        }
    }
}
