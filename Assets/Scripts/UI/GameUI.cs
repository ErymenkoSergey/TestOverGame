using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TestOverMobile.Interface;

namespace TestOverMobile.UI
{
    public class GameUI : MonoBehaviour, IDisplaying
    {
        [SerializeField] private TextMeshProUGUI _currentScore;
        [SerializeField] private TextMeshProUGUI _currentLives;
        [SerializeField] private TextMeshProUGUI _playerName;

        [Space(15)]
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _quitButton;

        [Space(15)]
        [SerializeField] private GameObject _buttonsPanel;
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private GameObject _gameOverPanel;

        private IControllable _iControllable;

        public void SetControllable(IControllable controllable)
        {
            _iControllable = controllable;
            Initialized();
        }

        public void Initialized()
        {
            _pauseButton.onClick.AddListener(() => SetPausePanelStatus(true));
            //_continueButton.onClick.AddListener(() => SetPausePanelStatus(false));
            //_restartButton.onClick.AddListener(() => _iControllable.RestartGame());
            _quitButton.onClick.AddListener(() => _iControllable.QuitGame());
        }

        private void OnDestroy()
        {
            DeInitialized();
        }

        public void DeInitialized()
        {
            _pauseButton.onClick.RemoveAllListeners();
            //_restartButton.onClick.RemoveAllListeners();
            _quitButton.onClick.RemoveAllListeners();
        }

        public void SetScore(ref int score, ref int lives)
        {
            _currentScore.text = score.ToString();
            UpdateLivesUI(lives);
        }

        public void SetDamage(int currentLives)
        {
            UpdateLivesUI(currentLives);
        }

        private void UpdateLivesUI(int currentLives)
        {
            _currentLives.text = currentLives.ToString();
        }

        private void SetPausePanelStatus(in bool isOpen)
        {
            ShowButtonPanel(isOpen);
            _pausePanel.SetActive(isOpen);
            _iControllable.SetPause(isOpen);
        }

        private void ShowButtonPanel(in bool isOpen) { }// => _buttonsPanel.SetActive(isOpen);

        public void GameOver()
        {
            ShowButtonPanel(true);
            //_continueButton.enabled = false;
            //_gameOverPanel.SetActive(true);
        }
    }
}