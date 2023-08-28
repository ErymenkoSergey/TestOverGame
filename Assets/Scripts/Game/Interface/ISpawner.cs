using System;
using TestOverMobile.Data;
using UnityEngine;

namespace TestOverMobile.Interface
{
    public interface ISpawner
    {
        event Action<int, RectTransform> OnSetScore;
        event Action OnSetDamage;
        void StartSpawn(ref int countEnemy);
        ref Color GetRandomColor();
        void ReUsedBall(ReusedData data);
        Transform GetParentPosition();
    }
}