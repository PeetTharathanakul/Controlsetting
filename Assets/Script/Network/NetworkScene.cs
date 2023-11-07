using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkScene : NetworkSceneManagerBase
{
    [SerializeField] private int _lobby;
    [SerializeField] private int[] _levels;
    private Scene _loadedScene;


    protected override void Shutdown(NetworkRunner runner)
    {
        base.Shutdown(runner);
        if (_loadedScene != default)
            SceneManager.UnloadSceneAsync(_loadedScene);
        _loadedScene = default;
    }

    protected override IEnumerator SwitchScene(SceneRef prevScene, SceneRef newScene, FinishedLoadingDelegate finished)
    {
        throw new System.NotImplementedException();
    }
}
