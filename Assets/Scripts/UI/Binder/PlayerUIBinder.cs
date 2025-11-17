using UnityEngine;

public class PlayerUIBinder : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    private PlayerStaminaSM _playerStaminaSM;
    private PlayerContext _ctx;

    private void Awake()
    {
        _ctx = GetComponentInParent<PlayerContext>();
        _playerHealth = _ctx.Health;
        _playerStaminaSM = _ctx.Stamina;
    }

    private void Start()
    {
        if (HUDController.Instance == null)
        {
            Debug.LogWarning("HUDController instance not found. UI binding skipped.", this);
            return;
        }
        if (_playerHealth == null && _playerStaminaSM == null) return;
        HUDController.Instance.BindPlayer(_playerHealth, _playerStaminaSM);
    }
}
