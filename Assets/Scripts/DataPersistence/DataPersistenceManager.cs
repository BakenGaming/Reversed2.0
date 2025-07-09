using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class DataPersistenceManager : MonoBehaviour 
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    private static DataPersistenceManager _i;
    public static DataPersistenceManager i { get { return _i; } }

    private GameData gameData;
    private List<IDataPersistence> dataPersistanceObjects;
    private FileDataHandler dataHandler;

    private void Awake()
    {
        if (_i != null)
            Debug.LogError("Found more than one Data Persistence Manager in the scene");
        _i = this;
    }
    void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        dataPersistanceObjects = FindAllDataPersistanceObjects();
    }



    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        foreach (IDataPersistence dpo in dataPersistanceObjects)
        {
            dpo.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (IDataPersistence dpo in dataPersistanceObjects)
        {
            dpo.SaveData(ref gameData);
        }
    }

    private List<IDataPersistence> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects =
            FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
