using System;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

public class NetworkConnectionManager : MonoBehaviour, INetworkRunnerCallbacks
{
    [Header("Network Components")]
    [SerializeField] private NetworkRunner _runner;
    [SerializeField] private NetworkSceneManagerDefault _sceneManager;
    [SerializeField] private string _sessionName = "Room_01";

    public event Action<PlayerRef> PlayerJoined;
    public event Action<PlayerRef> PlayerLeft;

    public NetworkRunner Runner => _runner;

    private void Awake()
    {
        if (_runner == null)
        {
            _runner = GetComponent<NetworkRunner>();
        }

        if (_runner == null)
        {
            Debug.LogError("[NetworkConnectionManager] NetworkRunner no asignado.");
            return;
        }

        _runner.AddCallbacks(this);
    }

    public async void CreateRoom()
    {
        if (_runner == null) return;

        var args = new StartGameArgs
        {
            GameMode = GameMode.Host,
            SessionName = _sessionName,
            SceneManager = _sceneManager
        };

        var result = await _runner.StartGame(args);

        if (!result.Ok)
        {
            Debug.LogError($"[NetworkConnectionManager] Error al crear sala: {result.ShutdownReason}");
            Debug.LogError(result.ErrorMessage);
            return;
        }

        _runner.ProvideInput = true;
    }
        
    public async void JoinRoom()
    {
        if (_runner == null) return;

        var args = new StartGameArgs
        {
            GameMode = GameMode.Client,
            SessionName = _sessionName,
            SceneManager = _sceneManager
        };

        var result = await _runner.StartGame(args);

        if (!result.Ok)
        {
            Debug.LogError($"[NetworkConnectionManager] Error al unir a sala: {result.ShutdownReason}");
            Debug.LogError(result.ErrorMessage);
            return;
        }

        _runner.ProvideInput = true;
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"[NetworkConnectionManager] OnPlayerJoined: {player}");
        PlayerJoined?.Invoke(player);
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"[NetworkConnectionManager] OnPlayerLeft: {player}");
        PlayerLeft?.Invoke(player);
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        var ip = InputPlayer.Instance;
        if (ip == null) return;

        var inputPlayer = new PlayerNetworkInput
        {
            Move = ip.CurrentMove,
            IsRun = ip.IsRunning,
            IsJump = ip.ConsumeIsJumping(),
            IsPickup = ip.ConsumeWantsToPickup()
        };
        //inputPlayer.buttons.Set(PlayerNetworkInput.RUN, ip.IsRunning);
        //inputPlayer.buttons.Set(PlayerNetworkInput.JUMP, ip.ConsumeIsJumping());
        //inputPlayer.buttons.Set(PlayerNetworkInput.PICKUP, ip.ConsumeWantsToPickup());
        input.Set(inputPlayer);
    }
        
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnSessionListUpdated(NetworkRunner runner, System.Collections.Generic.List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, System.Collections.Generic.Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { } 
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
}