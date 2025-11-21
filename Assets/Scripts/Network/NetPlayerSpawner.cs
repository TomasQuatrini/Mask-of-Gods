using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private NetworkConnectionManager _connection;
    [SerializeField] private NetworkObject _playerPrefab;

    [Header("Spawn Config")]
    [SerializeField] private float _horizontalOffset = 2f;

    private readonly Dictionary<PlayerRef, NetworkObject> _spawnedPlayers = new();

    private Vector3 _baseSpawnPosition;

    private void Start()
    {
        if (_connection == null)
        {
        }

        if (UniversalSpawn.Instance != null)
        {
            _baseSpawnPosition = UniversalSpawn.Instance.Position();
        }
        else
        {
            Debug.LogError("[PlayerSpawner] UniversalSpawn.Instance es null. Usando (0,0,0).");
            _baseSpawnPosition = Vector3.zero;
        }
    }

    private void OnEnable()
    {
        if (_connection != null)
        {
            _connection.PlayerJoined += HandlePlayerJoined;
            _connection.PlayerLeft += HandlePlayerLeft;
        }
    }

    private void OnDisable()
    {
        if (_connection != null)
        {
            _connection.PlayerJoined -= HandlePlayerJoined;
            _connection.PlayerLeft -= HandlePlayerLeft;
        }
    }

    private void HandlePlayerJoined(PlayerRef player)
    {
        var runner = _connection.Runner;
        if (runner == null)
        {
            Debug.LogError("[PlayerSpawner] Runner es null.");
            return;
        }

        // Solo el servidor spawnea jugadores
        if (!runner.IsServer) return;

        int index = _spawnedPlayers.Count; // 0 para el primero (host), 1 para el segundo, etc.

        var offset = new Vector3(index * _horizontalOffset, 0f, 0f);
        var spawnPosition = _baseSpawnPosition + offset;

        var playerObj = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
        _spawnedPlayers[player] = playerObj;

        Debug.Log($"[PlayerSpawner] Spawned player {player} en {spawnPosition}");
    }

    private void HandlePlayerLeft(PlayerRef player)
    {
        var runner = _connection.Runner;
        if (runner == null)
            return;

        if (!runner.IsServer) return;

        if (_spawnedPlayers.TryGetValue(player, out var obj))
        {
            runner.Despawn(obj);
            _spawnedPlayers.Remove(player);
        }
    }
}