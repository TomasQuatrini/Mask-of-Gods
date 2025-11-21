using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _lobbyPanel;
    [SerializeField] private Button _createRoomButton;
    [SerializeField] private Button _joinRoomButton;

    [Header("Refs")]
    [SerializeField] private NetworkConnectionManager _connection;

    private void Awake()
    {
        if (_connection == null)
        {
        }
    }

    private void Start()
    {
        if (_connection == null)
        {
            Debug.LogError("[NetworkUIManager] NetworkConnectionManager no asignado.");
            return;
        }

        _createRoomButton.onClick.AddListener(_connection.CreateRoom);
        _joinRoomButton.onClick.AddListener(_connection.JoinRoom);

        _connection.PlayerJoined += OnAnyPlayerJoined;
    }

    private void OnAnyPlayerJoined(PlayerRef player)
    {
        if (_lobbyPanel != null)
            _lobbyPanel.SetActive(false);
    }

    private void OnDestroy()
    {
        if (_connection != null)
        {
            _connection.PlayerJoined -= OnAnyPlayerJoined;
        }

        if (_createRoomButton != null)
            _createRoomButton.onClick.RemoveAllListeners();

        if (_joinRoomButton != null)
            _joinRoomButton.onClick.RemoveAllListeners();
    }
}