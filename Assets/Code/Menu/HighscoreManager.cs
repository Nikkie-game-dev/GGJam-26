using Code.FileManager;
using Code.Player;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Code.Manager
{
    public class HighscoreManager : MonoBehaviour
    {
        [Header("Players data")]
        [SerializeField] private FileData _playersFileData;
        [SerializeField] private List<PlayerData> _playerDataList = new List<PlayerData>();
        private PlayerDataHandler _playerDataHandler;
        [Header("UIData")]
        [SerializeField] private int _maxScoreCount = 5;
        [SerializeField] private GameObject _playerDataPrefab;
        [SerializeField] private Transform _dataParent;
        private List<GameObject> _playerDataGOList;

        private void Start()
        {
            _playerDataHandler = new PlayerDataHandler(_playersFileData);

            RefreshData();
        }

        public void ClearLocalData()
        {
            _playerDataHandler.RemoveAllPlayerData();
        }

        public void RefreshData()
        {
            _playerDataList = _playerDataHandler.GetAllPlayersData();

            _playerDataList.Sort((a, b) => b.score.CompareTo(a.score));
            StartUpHighScore();
        }

        private void StartUpHighScore()
        {
            if (_playerDataList == null || _playerDataList.Count < 1)
                return;

            if (_playerDataGOList == null)
                _playerDataGOList = new();

            if (_playerDataGOList.Count < 1)
            {
                for (int i = 0; i < _maxScoreCount && i < _playerDataList.Count; i++)
                {
                    GameObject playerDataGO = Instantiate(_playerDataPrefab, _dataParent);
                    _playerDataGOList.Add(playerDataGO);
                    SetPlayerDataText(i, playerDataGO);
                }
            }
            else
            {
                for (int i = 0; i < _maxScoreCount && i < _playerDataList.Count; i++)
                {
                    GameObject playerDataGO = _playerDataGOList[i];
                    SetPlayerDataText(i, playerDataGO);
                }
            }
        }

        private void SetPlayerDataText(int i, GameObject playerDataGO)
        {
            TextMeshProUGUI playerDataText = playerDataGO.GetComponent<TextMeshProUGUI>();
            playerDataText.text = _playerDataList[i].name + " | " + _playerDataList[i].score;
        }
    }
}
