using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.Services.CloudSave;


public class SaveDataPlayGame : MonoBehaviour
{
    [SerializeField] private PlayerContext _playerContext;
    public void SavePlayerData()
    {
        var playerPosition = new PlayerPosition(_playerContext.transform.position);
        var currentPlayerHealth = _playerContext.Health.HealthResource.Current;
        var currentPlayerStamina = _playerContext.Stamina.StaminaResource.Current;
        var saveData = new SaveDataPlayer(playerPosition, currentPlayerHealth, currentPlayerStamina);
    }
}

[System.Serializable]
public class SaveDataPlayer
{
    private PlayerPosition _playerPosition;
    private float _currentPlayerHealth;
    private float _currentPlayerStamina;

    public PlayerPosition PlayerPosition => _playerPosition;
    public float CurrentPlayerHealth => _currentPlayerHealth;
    public float CurrentPlayerStamina => _currentPlayerStamina;

    public SaveDataPlayer(PlayerPosition playerPosition, float currentPlayerHealth, float currentPlayerStamina)
    {
        _playerPosition = playerPosition;
        _currentPlayerHealth = currentPlayerHealth;
        _currentPlayerStamina = currentPlayerStamina;
    }
}

[System.Serializable]
public struct PlayerPosition
{
    public float x;
    public float y;
    public float z;

    public Vector3 Vector3 => new Vector3(x, y, z);
    public PlayerPosition(Vector3 vector3)
    {
        x = vector3.x;
        y = vector3.y;
        z = vector3.z;
    }
}