namespace TestOverMobile.Services
{
    public sealed class GameSequence
    {
        private int _currentLives;
        private int _currentScore;
        private int _gameOverLives = 0;

        public GameSequence(int numberAttempts)
        {
            _currentScore = 0;
            _currentLives = numberAttempts;
        }

        public bool SetScore(int score, out int currentScore, out int currentLives)
        {
            currentScore = _currentScore += score;
            currentLives = _currentLives;
            return IsCheckGameOver();
        }

        public (int, bool) SetDamage()
        {
            _currentLives--;
            return (_currentLives, IsCheckGameOver());
        }

        private bool IsCheckGameOver()
        {
            return _currentLives <= _gameOverLives ? true : false;
        }
    }
}