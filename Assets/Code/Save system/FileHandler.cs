using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerDataHandler : MonoBehaviour
{
    private FileHandler<List<PlayerData>> fileHandler;
    [SerializeField] string dataFileName;
    private List<PlayerData> data;

    private void Start()
    {
        fileHandler = new FileHandler<List<PlayerData>>(Application.persistentDataPath, dataFileName);
    }

    public void SavePlayerData(string name, int score)
    {
        data = fileHandler.Load();

        if (data == null)
            data = new List<PlayerData>();

        data.Add(new(name, score));

        fileHandler.Save(data);
    }

    public List<PlayerData> GetAllPlayersData()
    {
        List<PlayerData> playerDatas = null;

        try
        {
            playerDatas = fileHandler.Load();
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
    private string dataDirPath;
    private string dataFileName;

    private string fullPath;

    public FileHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.fullPath = Path.Combine(dataDirPath, dataFileName);
    }

    public void Save(T data)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string toStore = JsonUtility.ToJson(data);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
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

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
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

