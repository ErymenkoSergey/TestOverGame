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
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _menuGameOverButton;

        [Space(15)]
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private GameObject _gameOverPanel;

        private IControllable _iControllable;
        private ISaveble _iSaveble;

        public void SetControllable(IControllable controllable, ISaveble saveble)
        {
            _iControllable = controllable;
            _iSaveble = saveble;
            Initialized();
            SetDataUI();
        }

        public void Initialized()
        {
            _pauseButton.onClick.AddListener(() => SetPausePanelStatus(true));
            _continueButton.onClick.AddListener(() => SetPausePanelStatus(false));
            _menuButton.onClick.AddListener(() => GoToMenu());
            _menuGameOverButton.onClick.AddListener(() => GoToMenu());
        }

        private void GoToMenu()
        {
            _iSaveble.Save();
            _iControllable.ToMenu();
        }

        private void SetDataUI()
        {
            _playerName.text = _iSaveble.GetPlayerName();
        }

        private void OnDestroy()
        {
            DeInitialized();
        }

        public void DeInitialized()
        {
            _pauseButton.onClick.RemoveAllListeners();
            _continueButton.onClick.RemoveAllListeners();
            _menuButton.onClick.RemoveAllListeners();
            _menuGameOverButton.onClick.RemoveAllListeners();
        }

        public void SetScore(ref int score, ref int lives)
        {
            _currentScore.text = score.ToString();
            _iSaveble.SetResultCurrentPlayer(score);
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
            _pausePanel.SetActive(isOpen);
            _iControllable.SetPause(isOpen);
        }

        public void GameOver()
        {
            _continueButton.enabled = false;
            _gameOverPanel.SetActive(true);
        }
    }
}