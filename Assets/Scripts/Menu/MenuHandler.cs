using TestOverMobile.Interface;
using TestOverMobile.SaveSystem;
using TestOverMobile.Services;
using UnityEngine;

namespace TestOverMobile.Core
{
    public class MenuHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _menuUI;
        private iDisplayed _iDisplayed;

        private ControlServices _controlServices;

        //[SerializeField] private GameObject _saveServices;
        private ISaveble _iSaveble;

        [Header("Debug Flags")]
        [SerializeField] private bool _isStartGame = false;
        [SerializeField] private bool _isStartSaveService = false;
        [SerializeField] private bool _isStartUIService = false;

        private void Start()
        {
            _isStartGame = Initialaze();
        }

        private bool Initialaze()
        {
            if (_menuUI.TryGetComponent(out iDisplayed component))
                _iDisplayed = component;
            
            _controlServices = new ControlServices();
            _isStartSaveService = ConnectToData();
            _isStartUIService = _iDisplayed.SetControllable(_controlServices, _iSaveble);
            
            return true;
        }

        private void SetNamePlayer()
        {

        }

        private bool ConnectToData()
        {
            _iSaveble = SaveServices.Instance;

            if (_iSaveble != null)
                return true;
            else
                return false;
        }
    }
}