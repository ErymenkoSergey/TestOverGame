using UnityEngine;

namespace TestOverMobile.Data
{
    [CreateAssetMenu(fileName = "GameSetting", menuName = "Game/Configuration/New GameSetting")]
    public class GameSetting : ScriptableObject
    {
        [SerializeField] private int _maxCountBalls = 3;
        public ref int MaxCountBalls => ref _maxCountBalls;

        [SerializeField] private int _lives = 3;
        public ref int Lives => ref _lives;
    }
}