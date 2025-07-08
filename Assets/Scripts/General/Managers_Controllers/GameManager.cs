using System;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Events
    public static event Action OnPlayerSpawned;
    #endregion
    #region Variables
    private static GameManager _i;
    public static GameManager i { get { return _i; } }
    [Header("Level Setup")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private LevelStatsSO levelStatsSO;
    [SerializeField] private CinemachineCamera cinemachineCamera;
    private GameObject playerGO;
    private TimeManager timeManager;
    private bool isPaused, levelStarted;


    #endregion

    #region Initialize
    private void Awake()
    {
        _i = this;
        PlayerHandler.OnPlayerDeath += ReSpawnPlayerObject;
        SetupObjectPools();
        Initialize();
    }
    void OnDisable()
    {
        PlayerHandler.OnPlayerDeath -= ReSpawnPlayerObject;
        TimeManager.OnTimerFinish -= StartLevel;
        TimeManager.OnLevelFail -= LevelFail;
    }

    private void Initialize()
    {
        GameObject newRoom = Instantiate(levelStatsSO.roomLayout);
        newRoom.transform.parent = null;
        newRoom.transform.position = Vector3.zero;
        spawnPoint = newRoom.GetComponent<RoomController>().GetStartPosition();
        timeManager = GetComponent<TimeManager>();
        TimeManager.OnTimerFinish += StartLevel;
        TimeManager.OnLevelFail += LevelFail;
        isPaused = true;
        SpawnPlayerObject();
    }

    private void SpawnPlayerObject()
    {
        playerGO = Instantiate(GameAssets.i.pfPlayerObject, spawnPoint);
        playerGO.transform.parent = null;
        timeManager.Initialize(levelStatsSO);
        playerGO.GetComponent<IHandler>().Initialize();
        OnPlayerSpawned?.Invoke();
    }

    private void StartLevel()
    {
        levelStarted = true;
        isPaused = false;
        Debug.Log("LEVEL STARTED");
        /*PlayLevelMusic();*/
    }

    private void LevelFail()
    {
        isPaused = true;
        Debug.Log("LEVEL FAILED");
    }
    public void SetupObjectPools()
    {
        //Do the below for all objects that will need pooled for use
        //ObjectPooler.SetupPool(OBJECT, SIZE, "NAME") == Object is pulled from GameAssets, Setup object with a SO that contains size and name

        //The below is placed in location where object is needed from pool
        //==============================
        //PREFAB_SCRIPT instance = ObjectPooler.DequeueObject<PREFAB_SCRIPT>("NAME");
        //instance.gameobject.SetActive(true);
        //instance.Initialize();
        //==============================
    }
    #endregion
    #region Functions
    private void ReSpawnPlayerObject()
    {
        playerGO.SetActive(false);
        playerGO.transform.position = spawnPoint.position;
        OnPlayerSpawned?.Invoke();
        playerGO.SetActive(true);

    }

    public void LevelComplete()
    {
        isPaused = true;
        levelStarted = false;
    }
    #endregion
    #region Get and Receive Functions
    public void PauseGame() { if (isPaused) return; else isPaused = true; }
    public void UnPauseGame() { if (isPaused) isPaused = false; else return; }

    public GameObject GetPlayerGO() { return playerGO; }
    public bool GetIsPaused() { return isPaused; }
    
    public CinemachineCamera GetCinemachineCamera() { return cinemachineCamera; }
    #endregion
}
