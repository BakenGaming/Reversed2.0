using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandler
{
    public void Initialize();
    public abstract void HandleDeath();
    public PlayerStatsSO GetPlayerStatsSO();
}
