using Code.FileManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using UnityEngine;

namespace Code.FileManager
{
    [CreateAssetMenu(fileName = "FileData", menuName = "FileData")]
    public sealed class FileData : ScriptableObject
    {
        public string fileName;
        public string FullPath => Path.Combine(Application.persistentDataPath, fileName);
    }

    public sealed class FileHandler<T>
    {
        private string _fullPath;

        public FileHandler(string dataDirPath, string dataFileName)
        {
            this._fullPath = Path.Combine(dataDirPath, dataFileName);
        }

        public FileHandler(FileData fileData)
        {
            this._fullPath = fileData.FullPath;
        }

        /// <summary>
        /// Saves data. Not intended for collections
        /// </summary>
        /// <param name="data"></param>
        public void Save(T data)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_fullPath));

                string toStore = JsonUtility.ToJson(data, true);

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

        public void SaveCollection(List<T> data)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_fullPath));

                string serializedData = JsonSerializer.Serialize(data);

                using (FileStream stream = new FileStream(_fullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(serializedData);
                    }
                }
            }
            catch (System.Exception error)
            {
                Debug.LogException(error);
                throw;
            }
        }

        /// <summary>
        /// Loads data. Not intended for collections
        /// </summary>
        /// <param name="data"></param>
        public bool TryLoadCollection(out List<T> data)
        {
            data = new List<T>();

            try
            {
                string dataToLoad = "";

                if (File.Exists(_fullPath))
                {
                    using (FileStream stream = new FileStream(_fullPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }

                    data = JsonSerializer.Deserialize<List<T>>(dataToLoad);

                    return true;
                }
            }
            catch (System.Exception error)
            {
                Debug.LogError(error);
                throw;
            }

            return false;
        }

        /// <summary>
        /// Loads data. Not intended for collections
        /// </summary>
        /// <param name="data"></param>
        public bool TryLoad(out T data)
        {
            data = default(T);

            try
            {
                string dataToLoad = "";

                if (File.Exists(_fullPath))
                {
                    using (FileStream stream = new FileStream(_fullPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }

                    data = JsonUtility.FromJson<T>(dataToLoad);

                    return true;
                }
            }
            catch (System.Exception error)
            {
                Debug.LogError(error);
                throw;
            }

            return false;
        }

        public void RemoveFile()
        {
            if (File.Exists(_fullPath))
                File.Delete(_fullPath);
        }
    }
}

namespace Code.Player
{
    public sealed class PlayerDataHandler
    {
        private FileManager.FileHandler<PlayerData> _fileHandler;
        private List<PlayerData> _data;

        public PlayerDataHandler(FileData fileData)
        {
            _fileHandler = new FileHandler<PlayerData>(fileData);
        }

        public void SavePlayerData(string name, int score)
        {
            if (!_fileHandler.TryLoadCollection(out _data))
                _data = new List<PlayerData>();

            PlayerData playerData = new PlayerData();
            playerData.name = name;
            playerData.score = score;

            _data.Add(playerData);

            _fileHandler.SaveCollection(_data);
        }

        public List<PlayerData> GetAllPlayersData()
        {
            List<PlayerData> playerDatas = null;

            try
            {
                if (_fileHandler.TryLoadCollection(out playerDatas))
                    return playerDatas;
            }
            catch (System.Exception error)
            {
                Debug.LogError(error);
                throw;
            }

            return null;
        }

        public void RemoveAllPlayerData()
        {
            _fileHandler.RemoveFile();
        }
    }


    public class PlayerData
    {
        public string name { get; set; }
        public int score { get; set; }
    }

}


