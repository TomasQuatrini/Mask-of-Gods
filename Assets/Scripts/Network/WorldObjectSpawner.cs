using Fusion;
using UnityEngine;

public class WorldObjectSpawner : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private NetworkConnectionManager _connection;
    [SerializeField] private NetworkObject _staminaSpawnerPrefab;
    [SerializeField] private NetworkObject _healthSpawnerPrefab;

    private Vector3 _staminaSpawnerPosition;
    private Vector3 _healthSpawnerPosition;

    private bool _worldSpawned;

    private void Start()
    {
        if (_connection == null)
        {
        }

        if (UniversalSpawn.Instance != null)
        {
            _staminaSpawnerPosition = UniversalSpawn.Instance.StaminaSpawn();
            _healthSpawnerPosition = UniversalSpawn.Instance.HealthSpawn();
        }
        else
        {
            Debug.LogError("[WorldObjectSpawner] UniversalSpawn.Instance es null. Usando (0,0,0).");
            _staminaSpawnerPosition = Vector3.zero;
            _healthSpawnerPosition = Vector3.zero;
        }
    }

    private void OnEnable()
    {
        if (_connection != null)
        {
            _connection.PlayerJoined += HandlePlayerJoined;
        }
    }

    private void OnDisable()
    {
        if (_connection != null)
        {
            _connection.PlayerJoined -= HandlePlayerJoined;
        }
    }

    private void HandlePlayerJoined(PlayerRef player)
    {
        var runner = _connection.Runner;
        if (runner == null)
        {
            Debug.LogError("[WorldObjectSpawner] Runner es null.");
            return;
        }

        // Solo el servidor crea los spawners del mundo
        if (!runner.IsServer) return;

        // Solo una vez
        if (_worldSpawned) return;
        _worldSpawned = true;

        runner.Spawn(_staminaSpawnerPrefab, _staminaSpawnerPosition, Quaternion.identity);
        runner.Spawn(_healthSpawnerPrefab, _healthSpawnerPosition, Quaternion.identity);

        Debug.Log("[WorldObjectSpawner] Spawners de mundo creados.");
    }
}