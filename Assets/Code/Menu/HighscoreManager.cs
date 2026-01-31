using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// TODO: Remove this struct
[Serializable]
public struct PlayerData
{
    public string name;
    public int score;
}

namespace Code.Manager
{
    public class HighscoreManager : MonoBehaviour
    {
        [SerializeField] private int _maxScoreCount = 5;

        [SerializeField] private List<PlayerData> _playerDataList;
        [SerializeField] private GameObject _playerDataPrefab;
        [SerializeField] private Transform _dataParent;

        private void Start()
        {
            //TODO: get player data from player data file manager here

            StartUpHighScore();
        }

        private void StartUpHighScore()
        {
            if (_playerDataList == null || _playerDataList.Count < 1)
                return;


            for (int i = 0; i < _maxScoreCount && i < _playerDataList.Count; i++)
            {
                GameObject playerDataGO = Instantiate(_playerDataPrefab, _dataParent);
                TextMeshProUGUI playerDataText = playerDataGO.GetComponent<TextMeshProUGUI>();
                playerDataText.text = _playerDataList[i].name + " | " + _playerDataList[i].score;
            }
        }
    }

}
