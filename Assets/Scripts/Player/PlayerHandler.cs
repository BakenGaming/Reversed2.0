using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerHandler : MonoBehaviour, IHandler, IDataPersistence
{
    #region Events
    public static event Action OnPlayerDeath;
    #endregion
    #region Variables
    [SerializeField] private PlayerStatsSO playerStatsSO;
    private bool firstSetup = true;
    private int deathCount = 0;

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
        deathCount++;
        OnPlayerDeath?.Invoke();
    }

    public PlayerStatsSO GetPlayerStatsSO() { return playerStatsSO; }
    #endregion

    #region Player Setup
    private void SetupPlayer()
    {
        if (firstSetup) GetComponent<IInputHandler>().Initialize();
        else GetComponent<IInputHandler>().ReInitialize();
        firstSetup = false;
    }

    private void ResetSetup() { firstSetup = true; }
    #endregion

    #region Save and Load Data
    public void LoadData(GameData _data)
    {
        deathCount = _data.deathCount;
    }

    public void SaveData(ref GameData _data)
    {
        _data.deathCount = deathCount;
    }
    #endregion
}
