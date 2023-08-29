using UnityEngine;

namespace TestOverMobile.Audio
{
    public class AudioEffects : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _volume = 1.0f;
        [SerializeField] private AudioClip _clickButtonSpound;

        public static AudioEffects Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this.gameObject);
            else
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        public void ClickButton()
        {
            _audioSource.PlayOneShot(_clickButtonSpound, _volume);
        }
    }
}