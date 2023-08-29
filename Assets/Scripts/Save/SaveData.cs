//using UnityEngine;

//namespace TestOverMobile.SaveSystem
//{
//    public class SaveData : MonoBehaviour
//    {
//        public string Name;
//        public int Score;

//        private Storage _storage;
//        private GameData _gameData;

//        private void Start()
//        {
//            _storage = new Storage();
//            Load();
//        }

//        public void Save()
//        {
//            _gameData.PlayerName = Name;
//            _gameData.Score = Score;
//            _storage.Save(_gameData);
//        }

//        private void Load()
//        {
//            _gameData = (GameData)_storage.Load(new GameData());
//            Name = _gameData.PlayerName;
//            Score = _gameData.Score;
//        }
//    }
//}