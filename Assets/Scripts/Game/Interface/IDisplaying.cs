namespace TestOverMobile.Interface
{
    public interface IDisplaying
    {
        void SetControllable(IControllable controllable, ISaveble saveble);
        void SetScore(ref int score, ref int lives);
        void SetDamage(int currentLives);
        void GameOver();
    }
}