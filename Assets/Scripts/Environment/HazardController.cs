using UnityEngine;

public class HazardController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        IHandler newHandler = collision.GetComponent<IHandler>();
        newHandler.HandleDeath();
    }
}
