using UnityEngine;

public interface IDataPersistence
{
    public void LoadData(GameData _data);
    public void SaveData(ref GameData _data);
}
