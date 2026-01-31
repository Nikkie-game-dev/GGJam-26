using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerDataHandler : MonoBehaviour
{
    private FileHandler<List<PlayerData>> _fileHandler;
    [SerializeField] private string _dataFileName;
    private List<PlayerData> _data;

    private void Start()
    {
        _fileHandler = new FileHandler<List<PlayerData>>(Application.persistentDataPath, _dataFileName);
    }

    public void SavePlayerData(string name, int score)
    {
        _data = _fileHandler.Load();

        if (_data == null)
            _data = new List<PlayerData>();

        _data.Add(new(name, score));

        _fileHandler.Save(_data);
    }

    public List<PlayerData> GetAllPlayersData()
    {
        List<PlayerData> playerDatas = null;

        try
        {
            playerDatas = _fileHandler.Load();
        }
        catch (System.Exception error)
        {
            Debug.LogError(error);
            throw;
        }

        return playerDatas;
    }
}

public struct PlayerData
{
    public string name;
    public int score;

    public PlayerData(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}

public class FileHandler<T>
{
    private string _dataDirPath;
    private string _dataFileName;

    private string _fullPath;

    public FileHandler(string dataDirPath, string dataFileName)
    {
        this._dataDirPath = dataDirPath;
        this._dataFileName = dataFileName;
        this._fullPath = Path.Combine(dataDirPath, dataFileName);
    }

    public void Save(T data)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_fullPath));

            string toStore = JsonUtility.ToJson(data);

            using (FileStream stream = new FileStream(_fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(toStore);
                }
            }
        }
        catch (System.Exception error)
        {
            Debug.LogException(error);
            throw;
        }
    }

    public T Load()
    {
        T data = default;

        try
        {
            string dataToLoad = "";

            using (FileStream stream = new FileStream(_fullPath, FileMode.Create))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    dataToLoad = reader.ReadToEnd();
                }
            }

            data = JsonUtility.FromJson<T>(dataToLoad);
        }
        catch (System.Exception error)
        {
            Debug.LogError(error);
            throw;
        }

        return data;
    }
}

