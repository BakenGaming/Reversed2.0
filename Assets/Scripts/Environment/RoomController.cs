using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private Transform startPosition;

    public Transform GetStartPosition() { return startPosition; }
}
