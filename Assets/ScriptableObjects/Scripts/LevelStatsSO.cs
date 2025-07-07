using UnityEngine;

[CreateAssetMenu(menuName = "Level Stats")]
public class LevelStatsSO : ScriptableObject
{
    public string levelName;
    public int countDownTimer;
    public float maxTimeToComplete;
}
