using TestOverMobile.Data;
using TestOverMobile.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace TestOverMobile.Characters
{
    public abstract class BaseBall : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] protected RectTransform RectTransform;
        [SerializeField] private Image _image;

        private ISpawner _iSpawner;
        private int _index;
        private int _reward;
        private bool _isClick;

        protected float TimeMoving { get; private set; }
        protected RectTransform StartPosition { get; private set; }
        protected RectTransform FinishPosition { get; private set; }
        protected bool IsGame { get; private set; } = false;
        protected bool IsMoved { get; set; } = false;

        private void OnEnable()
        {
            _button.onClick.AddListener(() => Burst());
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(() => Burst());
        }

        public void SetData(ref BallData data)
        {
            StartPosition = data.StartPosition;
            FinishPosition = data.FinishPosition;
            _index = data.Index;
            _reward = data.Reward;
            TimeMoving = data.TimeMoving;
            _iSpawner = data.spawner;
            SetCurrentPosition(StartPosition);
            SetGameStatus(true);
        }

        private void SetGameStatus(in bool isGame)
        {
            IsGame = isGame;
            if (IsGame)
                SetRandomColor();
        }

        private void SetRandomColor()
        {
            _image.color = _iSpawner.GetRandomColor();
        }

        public void Burst(bool isClick = true)
        {
            SetGameStatus(false);
            _isClick = isClick;
            Destroy(gameObject);
            _iSpawner.ReUsedBall(GetReusedData());
        }

        private ReusedData GetReusedData()
        {
            ReusedData data = new ReusedData();
            data.Index = _index;
            data.IsClick = _isClick;
            data.Reward = _reward;
            data.Position = GetCurrentPosition();
            return data;
        }

        private void SetCurrentPosition(RectTransform position) => RectTransform.anchoredPosition = position.anchoredPosition;

        public RectTransform GetCurrentPosition() => RectTransform;
    }
}