using TestOverMobile.Interface;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TestOverMobile.Services
{
    public sealed class ControlServices : IControllable
    {
        private string _sceneMenuName = "Menu";
        private string _sceneGameName = "Game";

        public void SetPause(in bool pause)
        {
            Time.timeScale = pause ? 0 : 1;
        }

        public void RestartGame()
        {
            SetPause(false);
            SceneManager.LoadScene(_sceneGameName);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void ToMenu()
        {
            SceneManager.LoadScene(_sceneMenuName);
        }

        public void NewGame()
        {
            SceneManager.LoadScene(_sceneGameName);
        }
    }
}