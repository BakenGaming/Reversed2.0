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

    #endregion
    #region Initialize
    public void Initialize()
    {
        SetupPlayer();
    }

    #endregion

    #region Handle Player Functions

    public void HandleDeath()
    {
        OnPlayerDeath?.Invoke();
    }

    public PlayerStatsSO GetPlayerStatsSO() { return playerStatsSO; }
    #endregion

    #region Player Setup
    private void SetupPlayer()
    {
        _statSystem = new StatSystem(playerStatsSO);
        GetComponent<IInputHandler>().Initialize();
    }
    #endregion
}
