using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TestOverMobile.Effect
{
    public sealed class UIParticleSystem : MonoBehaviour
    {
        [SerializeField] private bool _playOnAwake = true;
        [SerializeField] private Sprite _particle;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private bool _looping = false;
        [SerializeField] private float _lifeTime = 2f;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _size = 0.6f;
        [SerializeField] private float _rotation = 660f;
        [SerializeField] private float _gravity = -9.81f;
        [SerializeField] private float _emissionsPerSecond = 10f;
        [SerializeField] private Vector2 _emissionDirection = new Vector2(1f, 1f);
        [SerializeField] private float _emissionAngle = 360f;
        [SerializeField] private Gradient _colorOverLifetime;
        [SerializeField] private AnimationCurve _sizeOverLifetime;
        [SerializeField] private AnimationCurve _speedOverLifetime;
        [SerializeField] private bool _isDestoing = true;

        private bool IsPlaying;
        private float Playtime = 0f;
        private Image[] ParticlePool;
        private int ParticlePoolPointer;
        
        private void Awake()
        {
            if (ParticlePool == null)
                Init();

            if (_playOnAwake)
                Play();
        }

        private void Init()
        {
            ParticlePoolPointer = 0;
            ParticlePool = new Image[(int)(_lifeTime * _emissionsPerSecond * 1.1f + 1)];

            for (int i = 0; i < ParticlePool.Length; i++)
            {
                var gameObject = new GameObject("Particle");
                gameObject.transform.SetParent(transform);
                gameObject.SetActive(false);
                ParticlePool[i] = gameObject.AddComponent<Image>();
                ParticlePool[i].transform.localRotation = Quaternion.identity;
                ParticlePool[i].transform.localPosition = Vector3.zero;
                ParticlePool[i].sprite = _particle;
            }
        }

        public void Play()
        {
            IsPlaying = true;
            StartCoroutine(ParticlePlay());
        }

        private IEnumerator ParticlePlay()
        {
            Playtime = 0f;
            float particleTimer = 0f;

            while (IsPlaying && (Playtime < _duration || _looping))
            {
                Playtime += Time.deltaTime;
                particleTimer += Time.deltaTime;
                while (particleTimer > 1f / _emissionsPerSecond)
                {
                    particleTimer -= 1f / _emissionsPerSecond;
                    ParticlePoolPointer = (ParticlePoolPointer + 1) % ParticlePool.Length;
                    if (!ParticlePool[ParticlePool.Length - 1 - ParticlePoolPointer].gameObject.activeSelf)
                        StartCoroutine(ParticleFly(ParticlePool[ParticlePool.Length - 1 - ParticlePoolPointer]));
                }

                yield return new WaitForEndOfFrame();
            }

            IsPlaying = false;
        }

        private IEnumerator ParticleFly(Image particle)
        {
            particle.gameObject.SetActive(true);
            particle.transform.localPosition = Vector3.zero;
            float particleLifetime = 0f;

            Vector3 emissonAngle = new Vector3(_emissionDirection.x, _emissionDirection.y, 0f);
            emissonAngle = Quaternion.AngleAxis(Random.Range(-_emissionAngle / 2f, _emissionAngle / 2f), Vector3.forward) * emissonAngle;
            emissonAngle.Normalize();

            Vector3 gravityForce = Vector3.zero;

            while (particleLifetime < _lifeTime)
            {
                particleLifetime += Time.deltaTime;
                gravityForce = Vector3.up * _gravity * particleLifetime;
                particle.transform.position += emissonAngle * _speedOverLifetime.Evaluate(particleLifetime / _lifeTime) * _speed + gravityForce;
                particle.transform.localScale = Vector3.one * _sizeOverLifetime.Evaluate(particleLifetime / _lifeTime) * _size;
                particle.transform.localRotation = Quaternion.AngleAxis(_rotation * particleLifetime, Vector3.forward);
                particle.color = _colorOverLifetime.Evaluate(particleLifetime / _lifeTime);
                yield return new WaitForEndOfFrame();
            }

            particle.gameObject.SetActive(false);

            if (_isDestoing)
                Destroy(gameObject);
        }
    }
}