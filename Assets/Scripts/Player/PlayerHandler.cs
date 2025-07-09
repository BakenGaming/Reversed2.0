using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerHandler : MonoBehaviour, IHandler
{
    #region Events
    public static event Action OnPlayerDeath;
    #endregion
    #region Variables
    [SerializeField] private PlayerStatsSO playerStatsSO;

    private StatSystem _statSystem;
    private bool firstSetup = true;

    #endregion
    #region Initialize
    public void Initialize()
    {
        LevelEndHandler.OnLevelEndReached += ResetSetup;
        SetupPlayer();
    }

    void OnDisable()
    {
        LevelEndHandler.OnLevelEndReached -= ResetSetup;
    }

    #endregion

    #region Handle Player Functions

    public void HandleDeath()
    {
        Instantiate(GameAssets.i.pfBloodParticle, transform.position, Quaternion.identity);
        OnPlayerDeath?.Invoke();
    }

    public PlayerStatsSO GetPlayerStatsSO() { return playerStatsSO; }
    #endregion

    #region Player Setup
    private void SetupPlayer()
    {
        _statSystem = new StatSystem(playerStatsSO);
        if (firstSetup) GetComponent<IInputHandler>().Initialize();
        else GetComponent<IInputHandler>().ReInitialize();
        firstSetup = false;
    }

    private void ResetSetup() { firstSetup = true; }
    #endregion
}
