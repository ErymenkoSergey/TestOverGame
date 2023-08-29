using System;
using System.Collections.Generic;
using TestOverMobile.Interface;
using UnityEngine;

namespace TestOverMobile.SaveSystem
{
    public class SaveServices : MonoBehaviour, ISaveble
    {
        public List<PlayerCard> PlayerCards = new List<PlayerCard>();

        private Storage _storage;
        private GameData _gameData;

        private PlayerCard _currentPlayer;

        public static SaveServices Instance;

        public string PlayerName;
        public int Score;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this.gameObject);
            else
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        private void Start()
        {
            _storage = new Storage();
            Load();
        }

        private void OnDisable()
        {
            Save();
        }

        public void SetPlayer(PlayerCard card)
        {
            _currentPlayer = card;
        }

        public void Save()
        {
            PlayerCards.Add(_currentPlayer);
            _gameData.PlayerCards = PlayerCards; //1
            _storage.Save(_gameData);
        }

        public void Load()
        {
            _gameData = (GameData)_storage.Load(new GameData());
            PlayerCards = _gameData.PlayerCards;
        }

        public List<PlayerCard> GetPlayerCards() => PlayerCards;

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
    }

    [Serializable]
    public struct PlayerCard
    {
        public string Name;
        public int Score;
    }
}