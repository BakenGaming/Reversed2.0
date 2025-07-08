using TMPro;
using UnityEngine;
using System.Collections;
using System;

public class TimeManager : MonoBehaviour
{
    #region Events
    public static event Action OnTimerFinish;
    public static event Action OnLevelFail;
    #endregion
    #region Variables
    [Header("Game UI Configuration")]
    [SerializeField] private TextMeshProUGUI levelStartTimerText;
    [SerializeField] private Animator coundownTimerAnim;

    [Header("UI Panel Configuration")]
    [SerializeField] private TextMeshProUGUI levelNameText;
    [SerializeField] private TextMeshProUGUI levelTimerText;
    [SerializeField] private Animator levelTimerAnim;
    [SerializeField] private TextMeshProUGUI levelBestTimeText;
    [SerializeField] private TextMeshProUGUI levelMaxTimeToComplete;
    [SerializeField] private TextMeshProUGUI levelAverageTimeText;
    [SerializeField] private TextMeshProUGUI levelTopTimeText;
    private float currentLevelMaxTime, levelTimer=0, millis;
    private bool gameStarted = false;
    private int minutes, seconds;
    #endregion
    #region Setup
    public void Initialize(LevelStatsSO _levelStats)
    {
        StartCoroutine(BeginCountDown(_levelStats.countDownTimer));
        levelNameText.text = _levelStats.levelName;
        levelMaxTimeToComplete.text = _levelStats.maxTimeToComplete.ToString();
        currentLevelMaxTime = _levelStats.maxTimeToComplete;
        levelTimer = currentLevelMaxTime;
        CalculateandUpdateCurrentTime();
    }
    #endregion
    #region Level Time Functions
    private void Update() 
    {
        if (gameStarted) UpdateLevelTimer();

        if (gameStarted && !GameManager.i.GetIsPaused())
        {
            levelTimer -= Time.deltaTime;
            //timeToComplete += Time.deltaTime;
        }
        
        if(levelTimer <= 0 /*&& !isBabyMode*/) 
        {
            OnLevelFail?.Invoke();
        }
    }

    private void UpdateLevelTimer()
    {
        if (GameManager.i.GetIsPaused()) return;

        CalculateandUpdateCurrentTime();

        /*if(GameManager.i.GetCurrentGameTime() < GameManager.i.GetLevelTimes()[1])
        {
            if(!pulsed) gameTimerAnim.SetTrigger("pulse");
            pulsed = true;
        }
        
        if(GameManager.i.GetCurrentGameTime() < GameManager.i.GetLevelTimes()[0])
        {
            OnGameTimerAboutToExpire?.Invoke();
            levelTimerText.color = Color.red;
            gameTimerAnim.SetBool("timerLow", true);
            border.SetActive(true);
            if (!tempoIncreased)
            {
                SoundManager.IncreaseTempo(sceneMusic.GetComponent<AudioSource>());
                tempoIncreased = true;
            }
        }  */
    }

    private void CalculateandUpdateCurrentTime()
    {
        minutes = (int)(levelTimer / 60);
        seconds = (int)(levelTimer - (minutes * 60));
        millis = levelTimer - (minutes * 60 + seconds);
        
        levelTimerText.text = minutes.ToString("D1") + " : " + seconds.ToString("D2") + millis.ToString(".##");
    }

    #endregion
    #region Start Timer Functions
    IEnumerator BeginCountDown(int timer)
    {
        for (int i = timer; i > 0; i--)
        {
            levelStartTimerText.text = i.ToString();
            //SoundManager.PlaySound(SoundManager.Sound.countDown);
            //coundownTimerAnim.SetTrigger("trigger");
            yield return new WaitForSecondsRealtime(1);
        }
        levelStartTimerText.text = "Start";
        //SoundManager.PlaySound(SoundManager.Sound.start);
        OnTimerFinish?.Invoke();
        StartCoroutine(DelayStart());
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSecondsRealtime(.5f);
        levelStartTimerText.text = "";
        OnTimerFinish?.Invoke();
        gameStarted = true;
    }
    #endregion
}
