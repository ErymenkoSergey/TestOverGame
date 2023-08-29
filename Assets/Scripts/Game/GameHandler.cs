using TestOverMobile.Interface;
using UnityEngine;
using TestOverMobile.Data;
using TestOverMobile.Services;
using TestOverMobile.SaveSystem;

namespace TestOverMobile.Core
{
    public sealed class GameHandler : MonoBehaviour, IStartupable
    {
        [Header("Data Configuration")]
        [SerializeField] private GameSetting _gameSetting;
        [SerializeField] private EffectData _effectData;

        [Header("Interfaces Objects")]
        [SerializeField] private GameObject _uI;
        private IDisplaying _iDisplaying;

        [SerializeField] private GameObject _spawner;
        private ISpawner _iSpawner;

        private ISaveble _iSaveble;

        private GameSequence _sequenceServices;
        private EffectServices _effectServices;
        private ControlServices _controlServices;
        private OptimizeServices _optimizeServices;

        [SerializeField] private bool _isGameOver = false;

        public bool StartGame()
        {
            CreateServices();

            _iSpawner.StartSpawn(ref _gameSetting.MaxCountBalls);
            return true;
        }

        private void CreateServices()
        {
            if (_spawner.TryGetComponent(out ISpawner spawning))
            {
                _iSpawner = spawning;
                _iSpawner.OnSetScore += ScoreDistributor;
                _iSpawner.OnSetDamage += SetDamage;
            }

            _sequenceServices = new GameSequence(_gameSetting.Lives);
            _effectServices = new EffectServices(ref _effectData.EffectsConfiguration, _iSpawner.GetParentPosition());
            _controlServices = new ControlServices();
            _optimizeServices = new OptimizeServices();

            ConnectToData();

            if (_uI.TryGetComponent(out IDisplaying displaying))
            {
                _iDisplaying = displaying;
                _iDisplaying.SetControllable(_controlServices, _iSaveble);
            }

            _controlServices.SetPause(false);
        }

        private void ConnectToData()
        {
            _iSaveble = SaveServices.Instance;
        }

        private void OnDestroy() => DeInitialized();

        public void DeInitialized()
        {
            _iSpawner.OnSetScore -= ScoreDistributor;
            _iSpawner.OnSetDamage -= SetDamage;
            _optimizeServices.Optimize();
        }

        public void ScoreDistributor(int count, RectTransform positionEffect)
        {
            if (_effectData.IsUseEffect)
                _effectServices.PlayEffect(positionEffect);

            _isGameOver = _sequenceServices.SetScore(count, out int currentScore, out int currentLives);
            _iDisplaying.SetScore(ref currentScore, ref currentLives);

            if (_isGameOver)
                GameOver();
        }

        public void SetDamage()
        {
            (int lives, bool _isGameOver) = _sequenceServices.SetDamage();
            _iDisplaying.SetDamage(lives);

            if (_isGameOver)
                GameOver();
        }

        private void GameOver()
        {
            _iDisplaying.GameOver();
            _controlServices.SetPause(true);
        }
    }
}