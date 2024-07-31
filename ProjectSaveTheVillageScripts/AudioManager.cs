using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Публичные поля
    public static AudioManager Instance { get; private set; }

    // Сериализованные поля
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectsSource;
    [SerializeField] private AudioClip _buttonClickSound;
    [SerializeField] private AudioClip _gameMusic;

    // Несериализованные поля
    private bool _isMuted = false;

    // Методы жизненного цикла Unity
    private void Awake()
    {
        InitializeInstance();
    }

    private void Start()
    {
        PlayMusic(_gameMusic);
    }

    // Публичные методы
    public void PlayMusic(AudioClip clip)
    {
        if (_musicSource != null)
        {
            _musicSource.clip = clip;
            _musicSource.loop = true;
            _musicSource.Play();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (_effectsSource != null)
        {
            _effectsSource.PlayOneShot(clip);
        }
    }

    public void ToggleMute()
    {
        _isMuted = !_isMuted;
        AudioListener.pause = _isMuted;
    }

    public AudioClip ButtonClickSound => _buttonClickSound;

    // Все остальные методы
    private void InitializeInstance()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
