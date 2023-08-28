using TestOverMobile.Data;
using UnityEngine;

namespace TestOverMobile.Services
{
    public sealed class EffectServices
    {
        [SerializeField] private EffectConfig[] _poolVisualEffects;
        private Transform _parentPosition;

        public EffectServices(ref EffectConfig[] config, Transform parent)
        {
            _poolVisualEffects = config;
            _parentPosition = parent;
        }

        public void PlayEffect(RectTransform position)
        {
            EffectConfig effect = _poolVisualEffects[GetRandomInt(_poolVisualEffects.Length)];
            Object.Instantiate(effect.Prefab, position).GetComponent<RectTransform>().SetParent(_parentPosition);
        }

        private int GetRandomInt(int maxValue) => Random.Range(0, maxValue);
    }
}