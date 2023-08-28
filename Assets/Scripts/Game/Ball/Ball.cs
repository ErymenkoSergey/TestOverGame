using UnityEngine;
using DG.Tweening;

namespace TestOverMobile.Characters
{
    public sealed class Ball : BaseBall
    {
        private void Update()
        {
            if (!IsGame)
                return;

            CheckPosition();

            if (IsMoved)
                return;

            MoveBall();
        }

        private void CheckPosition()
        {
            if (RectTransform.localPosition == FinishPosition.localPosition)
            {
                Burst(false);
                IsMoved = false;
            }
        }

        private void MoveBall()
        {
            IsMoved = true;
            RectTransform.anchoredPosition = StartPosition.anchoredPosition;
            RectTransform.DOLocalMove(FinishPosition.anchoredPosition, TimeMoving);
        }
    }
}