using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEditor.MemoryProfiler;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class Lobby : MonoBehaviour
{
    [SerializeField] private TMP_InputField _room;

    [SerializeField] private NetworkRunner _runner;
    [SerializeField] private NetworkScene _scene;
    private Action<NetworkRunner, ConnectionStatus, string> _connectionCallback;
    private ConnectionStatus _status;

    public enum ConnectionStatus
    {
        Disconnected,
        Connecting,
        Failed,
        Connected,
        Loading,
        Loaded
    }

    public void EnterRoom()
    {
        Launch(GameMode.Shared, _room.text,  OnConnectUpdate, _scene);
    }

    public async void Launch(GameMode mode, 
            string room,
            Action<NetworkRunner, ConnectionStatus, string> onConnect , INetworkSceneManager sceneLoader)
    {

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

    private void OnConnectUpdate(NetworkRunner runner, ConnectionStatus status, string reason)
    {
        if (!this)
            return;

        if (status != _status)
        {
            switch (status)
            {
                case ConnectionStatus.Disconnected:
                    break;
                case ConnectionStatus.Failed:
                    break;
            }
        }

        _status = status;

    }

    private void OnConnectedToServer()
    {
        Debug.Log("Connected");
        SetConnectionStatus(ConnectionStatus.Connected, "");
    }


    public void SetConnectionStatus(ConnectionStatus status, string message)
    {
        _status = status;
        if (_connectionCallback != null)
            _connectionCallback(_runner, status, message);
    }

}
