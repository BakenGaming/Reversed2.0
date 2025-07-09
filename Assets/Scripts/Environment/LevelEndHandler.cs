using UnityEngine;
using System;

public class LevelEndHandler : MonoBehaviour
{
    public static event Action OnLevelEndReached;
    void OnTriggerEnter2D(Collider2D collision)
    {
        OnLevelEndReached?.Invoke();
    }
}
