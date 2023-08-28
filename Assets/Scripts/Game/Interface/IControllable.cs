namespace TestOverMobile.Interface
{
    public interface IControllable
    {
        void SetPause(in bool pause);
        void RestartGame();
        void QuitGame();
    }
}