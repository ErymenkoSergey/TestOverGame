using TestOverMobile.Characters;
using System;
using UnityEngine;
using TestOverMobile.Interface;

namespace TestOverMobile.Data
{
    [CreateAssetMenu(fileName = "BallsSetting", menuName = "Game/Configuration/New BallsSetting")]
    public class BallsSetting : ScriptableObject
    {
        [SerializeField] private BaseBall _prefabBall;
        public BaseBall PrefabBall => _prefabBall;

        [SerializeField] private BallColors[] _ballColors;
        public BallColors[] BallsColors => _ballColors;
    }

    public struct BallData
    {
        public RectTransform StartPosition;
        public RectTransform FinishPosition;
        public int Index;
        public int Reward;
        public float TimeMoving;
        public ISpawner spawner;
    }

    public struct ReusedData
    {
        public int Index;
        public bool IsClick;
        public int Reward;
        public RectTransform Position;
    }

    [Serializable]
    public struct BallColors
    {
        public Color Color;
    }
}