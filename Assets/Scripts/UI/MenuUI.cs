using System.Collections.Generic;
using TestOverMobile.Interface;
using TestOverMobile.SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TestOverMobile.UI
{
    public class MenuUI : MonoBehaviour, iDisplayed
    {
        private IControllable _iControllable;
        private ISaveble _iSaveble;

        [Header("All Buttons")]
        [Space(15)]
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _bestResultButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private Button _closeBestResultButton;

        [Space(15)]
        [SerializeField] private Button _okNameButton;
        [SerializeField] private Button _closeNameButton;

        [Header("Best Result Panel")]
        [Space(15)]
        [SerializeField] private TMP_InputField _inputFieldName;
        [SerializeField] private GameObject _bestResultPanel;
        [SerializeField] private GameObject _enterDataPanel;
        [SerializeField] private GameObject _prefabPlayerCard;
        [SerializeField] private RectTransform _contentBestResult;

        private string _playerName;

        public bool SetControllable(IControllable controllable, ISaveble saveble)
        {
            _iControllable = controllable;
            _iSaveble = saveble;
            Initialized();
            if (_iSaveble.GetPlayerName() == null)
                ShowPanelEnterName(true);
            return true;
        }

        public void Initialized()
        {
            _newGameButton.onClick.AddListener(() => _iControllable.NewGame());
            _bestResultButton.onClick.AddListener(() => ShowBestResultPanel(true));
            _quitButton.onClick.AddListener(() => QuitGame());
            _closeBestResultButton.onClick.AddListener(() => ShowBestResultPanel(false));
            _okNameButton.onClick.AddListener(() => ShowPanelEnterName(false));
            _closeNameButton.onClick.AddListener(() => ShowPanelEnterName(false));
            _inputFieldName.onValueChanged.AddListener(SetPlayerName);
        }

        private void OnDestroy()
        {
            DeInitialized();
        }

        public void DeInitialized()
        {
            _newGameButton.onClick.RemoveAllListeners();
            _bestResultButton.onClick.RemoveAllListeners();
            _quitButton.onClick.RemoveAllListeners();
            _closeBestResultButton.onClick.RemoveAllListeners();
            _okNameButton.onClick.RemoveAllListeners();
            _closeNameButton.onClick.RemoveAllListeners();
            _inputFieldName.onValueChanged.RemoveAllListeners();
        }

        private void ShowBestResultPanel(bool isShow)
        {
            _bestResultPanel.SetActive(isShow);

            if (isShow)
                SpawnListPlayers();
        }

        public void ShowPanelEnterName(bool isShow)
        {
            _enterDataPanel.SetActive(isShow);
        }

        private void SetPlayerName(string name)
        {
            if (name == null)
            {
                ShowPanelEnterName(true);
                return;
            }
            else
            {
                _playerName = name;
            }

            _iSaveble.CreateNewPlayer(name);
        }

        public void SpawnListPlayers()
        {
            List<PlayerCard> players = GetPlayerCards();
            foreach (var player in players)
                Instantiate(_prefabPlayerCard, _contentBestResult).GetComponent<ResultCell>().SetData(player);
        }

        private List<PlayerCard> GetPlayerCards()
        {
            return _iSaveble.GetPlayerCards();
        }

        private void QuitGame()
        {
            _iSaveble.Save();
            _iControllable.QuitGame();
        }
    }
}