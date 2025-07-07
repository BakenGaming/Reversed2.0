using UnityEngine;

public class LevelEndHandler : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("WINNER!");
    }
}
