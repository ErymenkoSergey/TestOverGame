using TestOverMobile.Interface;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TestOverMobile.Services
{
    public sealed class ControlServices : IControllable
    {
        public void SetPause(in bool pause)
        {
            Time.timeScale = pause ? 0 : 1;
        }

        public void RestartGame()
        {
            SetPause(false);
            SceneManager.LoadScene("Game");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}