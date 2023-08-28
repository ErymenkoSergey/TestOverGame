using System;
using TestOverMobile.Effect;
using UnityEngine;

namespace TestOverMobile.Data
{
    [CreateAssetMenu(fileName = "EffectData", menuName = "Game/Configuration/New EffectData")]
    public class EffectData : ScriptableObject
    {
        [SerializeField] private bool _isUseEffects;
        public bool IsUseEffect => _isUseEffects;

        [SerializeField] private EffectConfig[] _effectsConfiguration;
        public ref EffectConfig[] EffectsConfiguration => ref _effectsConfiguration;
    }

    [Serializable]
    public struct EffectConfig
    {
        public UIParticleSystem Prefab;
    }
}