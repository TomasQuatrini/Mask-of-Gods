using System;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using UnityEngine.UI;

public class NetworkController : MonoBehaviour, INetworkRunnerCallbacks
{
    [Header("UI Elements")]
    [SerializeField] private GameObject _lobbyPanel;
    [SerializeField] private Button _createRoomButton;
    [SerializeField] private Button _joinRoomButton;

    [Header("Network Components")]
    [SerializeField] private NetworkRunner _networkRunner;
    [SerializeField] private NetworkSceneManagerDefault _networkSceneManagerDefault;
    [SerializeField] private NetworkObject _playerPrefab;

    private Dictionary<PlayerRef, NetworkObject> _spawnedPlayers = new Dictionary<PlayerRef, NetworkObject>();

    private Vector3 _spawmPosition;


    private void Awake()
    {
        if (_networkRunner != null)
        {
            _networkRunner.AddCallbacks(this);
        }
        else
        {
            Debug.LogError("NetworkRunner is not assigned!");
        }        
    }
    private void Start()
    {
        _createRoomButton.onClick.AddListener(CreateRoom);
        _joinRoomButton.onClick.AddListener(JoinRoom);
        if (UniversalSpawn.Instance != null)
            _spawmPosition = UniversalSpawn.Instance.Position();
        else
            Debug.LogError("UniversalSpawn instance not found!");
    }

    private async void CreateRoom()
    {
        var gameArg = new StartGameArgs()
        {
            GameMode = GameMode.Host,
            SessionName = "Room_01",
            SceneManager = _networkSceneManagerDefault
        };

        var result = await _networkRunner.StartGame(gameArg);

        if (!result.Ok)
        {
            Debug.LogError($"Failed to create room: {result.ShutdownReason}");
            Debug.LogError($"Error: {result.ErrorMessage}");
        }
        _networkRunner.ProvideInput = true;
    }

    private async void JoinRoom()
    {
        var gameArg = new StartGameArgs()
        {
            GameMode = GameMode.Client,
            SessionName = "Room_01",
            SceneManager = _networkSceneManagerDefault
        };

        var result = await _networkRunner.StartGame(gameArg);

        if (!result.Ok)
        {
            Debug.LogError(result.ShutdownReason);
        }
        _networkRunner.ProvideInput = true;
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("OnPlayerJoined");
        _lobbyPanel.SetActive(false);

        if (!_networkRunner.IsServer) return;

        var PlayerSpawned = _networkRunner.Spawn(_playerPrefab, _spawmPosition, Quaternion.identity, player);
        _spawnedPlayers.Add(player, PlayerSpawned);
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (!_networkRunner.IsServer) return;
        if (_spawnedPlayers.Remove(player, out NetworkObject networkObject))
        {
            _networkRunner.Despawn(networkObject);
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        var ip = InputPlayer.Instance;
        if (ip == null) return;

        var inputPlayer = new PlayerNetworkInput
        {
            Move = ip.CurrentMove,
            Run = ip.IsRunning,
            Jump = ip.IsJumping
        };

        input.Set(inputPlayer);
    }

    //-----------------------------------------------------------------------------------//
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    void INetworkRunnerCallbacks.OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    void INetworkRunnerCallbacks.OnConnectedToServer(NetworkRunner runner)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }
}