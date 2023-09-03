using System;
using System.Collections.Generic;
using TestOverMobile.Interface;
using UnityEngine;
using System.IO;

namespace TestOverMobile.SaveSystem
{
    public class SaveServices : MonoBehaviour, ISaveble
    {
        private PlayerCard _currentPlayer;

        public static SaveServices Instance;

        [Header("Current Player")]
        public string PlayerName;
        public int Score;

        [Header("Players")]
        [SerializeField] private List<PlayerCard> _players = new List<PlayerCard>();

        [Header("Save Configurations ")]
        [SerializeField] private string _savePath;
        [SerializeField] private string _localFloder = "/Data/Save/";
        [SerializeField] private string _saveFileName = "data.json";

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this.gameObject);
            else
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }

            _savePath = Path.Combine(Application.dataPath + _localFloder, _saveFileName);
            LoadFromFile();
        }

        public void Save()
        {
            AddPlayer();
        }

        public void AddPlayer()
        {
            _players.Add(_currentPlayer);
            SaveFile();
        }

        public List<PlayerCard> GetPlayerCards() => _players;

        public void CreateNewPlayer(string name)
        {
            _currentPlayer = new PlayerCard();
            PlayerName = _currentPlayer.Name = name;
        }

        public string GetPlayerName() => _currentPlayer.Name;

        public void SetResultCurrentPlayer(int score)
        {
            Score = _currentPlayer.Score = score;
        }

        public void SaveFile()
        {
            SaveStruct save = new SaveStruct
            {
                Players = _players
            };

            string json = JsonUtility.ToJson(save, true);

            try
            {
                File.WriteAllText(_savePath, json);
            }
            catch (Exception e)
            {
                Debug.Log("{GameLog} => [GameCore] - (<color=red>Error</color>) - SaveToFile -> " + e.Message);
            }
        }

        public void LoadFromFile()
        {
            if (!File.Exists(_savePath))
            {
                Debug.Log("{GameLog} => [GameCore] - LoadFromFile -> File Not Found!");
                return;
            }

            try
            {
                string json = File.ReadAllText(_savePath);

                SaveStruct save = JsonUtility.FromJson<SaveStruct>(json);
                _players = save.Players;
            }
            catch (Exception e)
            {
                Debug.Log("{GameLog} - [GameCore] - (<color=red>Error</color>) - LoadFromFile -> " + e.Message);
            }
        }
    }

    [Serializable]
    public struct SaveStruct
    {
        public List<PlayerCard> Players;
    }

    [Serializable]
    public struct PlayerCard
    {
        public string Name;
        public int Score;
    }
}