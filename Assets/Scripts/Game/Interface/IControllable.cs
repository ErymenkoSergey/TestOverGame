namespace TestOverMobile.Interface
{
    public interface IControllable
    {
        void SetPause(in bool pause);
        void NewGame();
        void ToMenu();
        void RestartGame();
        void QuitGame();
    }
}