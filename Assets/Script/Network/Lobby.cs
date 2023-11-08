using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEditor.MemoryProfiler;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using Fusion.Sockets;

public class Lobby : NetworkBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private TMP_InputField _room;
    [SerializeField] private NetworkRunner _runner;
    [SerializeField] private NetworkScene _scene;
    [SerializeField] private GameObject playerprefab;

    private Action<NetworkRunner, PlayerRef> _spawnPlayerCallback;
    private Action<NetworkRunner, ConnectionStatus, string> _connectionCallback;
    private ConnectionStatus _status;

    public static Lobby current;

    public enum ConnectionStatus
    {
        Disconnected,
        Connecting,
        Failed,
        Connected,
        Loading,
        Loaded
    }

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        OnConnectUpdate(null, ConnectionStatus.Disconnected, "");
    }

    public void EnterRoom()
    { 
        Launch(GameMode.Shared, _room.text, _scene,  OnConnectUpdate, OnSpawnPlayer);
    }

    public async void Launch(GameMode mode, 
            string room,
            INetworkSceneManager sceneLoader,
            Action<NetworkRunner, ConnectionStatus, string> onConnect,
            Action<NetworkRunner, PlayerRef> onSpawnPlayer
        )
    {

        _spawnPlayerCallback = onSpawnPlayer;

        SetConnectionStatus(ConnectionStatus.Connecting, "");

        DontDestroyOnLoad(gameObject);

        if (_runner == null)
            _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.name = name;
        _runner.ProvideInput = mode != GameMode.Server;

        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = room,
            SceneManager = sceneLoader
        });
 
    }

    private void OnSpawnPlayer(NetworkRunner runner, PlayerRef playerref)
    {
        runner.Spawn(playerprefab, Vector3.zero, Quaternion.identity, playerref);
    }

    private void OnConnectUpdate(NetworkRunner runner, ConnectionStatus status, string reason)
    {
        if (!this)
            return;


        Debug.Log(status);

        if (status != _status)
        {
            switch (status)
            {
                case ConnectionStatus.Disconnected:
                    Debug.Log(status);
                    break;
                case ConnectionStatus.Failed:
                    Debug.Log(status);
                    break;
            }
        }

        _status = status;

    }

    public void SetConnectionStatus(ConnectionStatus status, string message)
    {
        _status = status;
        if (_connectionCallback != null)
            _connectionCallback(_runner, status, message);
    }

    private void InstantiatePlayer(NetworkRunner runner, PlayerRef playerref)
    {
        _spawnPlayerCallback(runner, playerref);
       
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("Connected");

        if (runner.GameMode == GameMode.Shared)
        {
            InstantiatePlayer(runner, runner.LocalPlayer);
        }
        SetConnectionStatus(ConnectionStatus.Connected, "");
        //SceneManager.LoadScene(1);
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            Debug.Log("");
            InstantiatePlayer(runner, player);
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        SetConnectionStatus(ConnectionStatus.Disconnected, "");
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
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

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        
    }
}
