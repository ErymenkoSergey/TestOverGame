using TestOverMobile.Interface;
using UnityEngine;

namespace TestOverMobile.Core
{
    public sealed class AppStartup : MonoBehaviour
    {
        [SerializeField] private GameObject _handler;
        private IStartupable _game;

        [Tooltip("For Debug")]
        [SerializeField] private bool _isStartGameStatus = false;

        private void Start() //Main start
        {
            CreateReferences();
            Initialized();
        }

        private void CreateReferences()
        {
            if (_handler.TryGetComponent(out IStartupable handler))
                _game = handler;
        }

        private void Initialized()
        {
            _isStartGameStatus = _game.StartGame();
        }
    }
}