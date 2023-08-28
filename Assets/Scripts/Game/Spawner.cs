using TestOverMobile.Characters;
using TestOverMobile.Data;
using TestOverMobile.Interface;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace TestOverMobile.Core
{
    public sealed class Spawner : MonoBehaviour, ISpawner
    {
        [Tooltip("Game Limits")]
        [SerializeField] private RectTransform[] _startPoints;
        [SerializeField] private RectTransform[] _finishPoints;
        [SerializeField] private Transform _parentPoints;

        [Space(15)]
        [Header("Data Balls")]
        [SerializeField] private BallsSetting _data;
        readonly List<int> _ballsPool = new List<int>();

        private int _queueIndex = 0;
        private Dictionary<int, int> _queueBalls = new Dictionary<int, int>(0);

        private int _keyIndexReused;
        private bool _isFirstSpawn = true;
        // for complexity
        private int _countBallsReused = 1;
        private float _timeMoving = 10f;
        private int _reward = 1;

        private const float _TIMESPAWN = 0.25f;

        public event Action<int, RectTransform> OnSetScore;
        public event Action OnSetDamage;

        public void StartSpawn(ref int countBalls)
        {
            ClearBalls();
            StartCoroutine(BallSpawn(countBalls));
        }

        private void ClearBalls()
        {
            _queueIndex = 0;
            _ballsPool.Clear();
        }

        public IEnumerator BallSpawn(int countBalls)
        {
            for (int i = 0; i < countBalls; i++)
            {
                CreateBall(GetRandomInt(_startPoints.Length), _data.PrefabBall, _isFirstSpawn ? i : _keyIndexReused);
                _ballsPool.Add(_isFirstSpawn ? i : _keyIndexReused);
                yield return new WaitForSeconds(_TIMESPAWN);
            }

            _isFirstSpawn = false;
        }

        private void CreateBall(int indexSpawn, BaseBall balls, int id)
        {
            BaseBall baseBall = Instantiate(balls, _startPoints[indexSpawn].anchoredPosition, Quaternion.identity);
            BallData ball = CreateDataBall(id);
            baseBall.SetData(ref ball);
            baseBall.transform.SetParent(_parentPoints);
        }

        private BallData CreateDataBall(int id)
        {
            BallData ball = new BallData();
            ball.StartPosition = _startPoints[GetRandomInt(_startPoints.Length)];
            ball.FinishPosition = _finishPoints[GetRandomInt(_finishPoints.Length)];
            ball.Index = id;
            ball.Reward = _reward;
            ball.TimeMoving = _timeMoving;
            ball.spawner = this;
            return ball;
        }

        private int GetRandomInt(int maxValue) => UnityEngine.Random.Range(0, maxValue);

        public ref Color GetRandomColor()
        {
            int index = UnityEngine.Random.Range(0, _data.BallsColors.Length);
            return ref _data.BallsColors[index].Color;
        }

        public void ReUsedBall(ReusedData data)
        {
            if (data.IsClick)
                OnSetScore.Invoke(data.Reward, data.Position);
            else
                OnSetDamage.Invoke();

            if (_isFirstSpawn)
            {
                _queueBalls.Add(_queueIndex++, data.Index);
            }
            else
            {
                if (_queueBalls.Count == 0)
                {
                    StartSpawn(data.Index);
                }
                else
                {
                    foreach (var ball in _queueBalls)
                        StartSpawn(ball.Value);

                    _queueBalls.Clear();
                }
            }
        }

        private void StartSpawn(int key)
        {
            _ballsPool.Remove(key);
            _keyIndexReused = key;
            StartCoroutine(BallSpawn(_countBallsReused));
        }

        public Transform GetParentPosition() => _parentPoints;
    }
}