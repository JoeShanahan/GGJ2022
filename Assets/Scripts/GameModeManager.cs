using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    [SerializeField] GameStuff _gameState;
    [SerializeField] OnOffUI _buildBar;
    [SerializeField] OnOffUI _topRightBar;
    [SerializeField] TopBarUI _topBar;
    [SerializeField] BuildManager _buildManager;
    [SerializeField] DestroyManager _destroyManager;
    [SerializeField] Transform _buildModeCamera;
    [SerializeField] NavigationHelper _navigation;
    [SerializeField] PauseMenu _pauseMenu;

    private GameStuff.CurrentState _lastKnownState = GameStuff.CurrentState.PlayMode;

    // Start is called before the first frame update
    void Start()
    {
        StartPlayMode();
    }

    void SetNewState(GameStuff.CurrentState newState)
    {
        _gameState.currentState = newState;
        _lastKnownState = newState;
    }

    public void StartPlayMode()
    {
        _navigation.RegenerateNavmesh();
        _topBar.MenuGoAway();
        SetNewState(GameStuff.CurrentState.PlayMode);
        _buildManager.SetBuildModeActive(false);
        _destroyManager.SetDestroyModeActive(false);
        _buildModeCamera.gameObject.SetActive(false);
        _buildBar.TurnOff();
    }

    public void StartBuildMode()
    {
        _topBar.SetMenuBuild();
        SetNewState(GameStuff.CurrentState.BuildMode);
        _buildManager.SetBuildModeActive(true);
        _destroyManager.SetDestroyModeActive(false);
        _buildModeCamera.gameObject.SetActive(true);
        _buildBar.TurnOn();
    }

    public void StartDestroyMode()
    {
        _topBar.SetMenuDestroy();
        SetNewState(GameStuff.CurrentState.DestroyMode);
        _buildManager.SetBuildModeActive(false);
        _destroyManager.SetDestroyModeActive(true);
        _buildModeCamera.gameObject.SetActive(true);
        _buildBar.TurnOff();
    }

    public void BringUpSettingsMenu()
    {
        _pauseMenu.Appear();
        _buildManager.SetBuildModeActive(false);
        _destroyManager.SetDestroyModeActive(false);
        SetNewState(GameStuff.CurrentState.DestroyMode);
    }

    // Update is called once per frame
    void Update()
    {
        if (_lastKnownState != _gameState.currentState)
            OnStateChanged(_gameState.currentState);
    }

    void OnStateChanged(GameStuff.CurrentState newState)
    {
        _lastKnownState = newState;
        Debug.Log("The state changed from outside of the code!");

        if (newState == GameStuff.CurrentState.PlayMode)
            StartPlayMode();

        if (newState == GameStuff.CurrentState.BuildMode)
            StartBuildMode();

        if (newState == GameStuff.CurrentState.DestroyMode)
            StartDestroyMode();
    }
}
