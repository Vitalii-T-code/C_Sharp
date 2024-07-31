using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Сериализованные поля
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _soundButton;

    // Несериализованные поля
    private bool _isSoundOn = true;

    // Методы жизненного цикла Unity
    void Start()
    {
        SetupUIFunctionality();
    }

    // Приватные методы
    private void SetupUIFunctionality()
    {
        if (_mainMenu == null)
        {
            Debug.LogError("MainMenu не назначено в инспекторе");
            return;
        }

        if (_playButton == null)
        {
            Debug.LogError("PlayButton не назначено в инспекторе");
            return;
        }

        if (_soundButton == null)
        {
            Debug.LogError("SoundButton не назначено в инспекторе");
            return;
        }

        _playButton.onClick.AddListener(OnPlayButtonClicked);
        _soundButton.onClick.AddListener(OnSoundButtonClicked);

        UpdateSoundButtonText();
    }

    private void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Level1");
    }

    private void OnSoundButtonClicked()
    {
        _isSoundOn = !_isSoundOn;

        if (_isSoundOn)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }

        UpdateSoundButtonText();
    }

    private void UpdateSoundButtonText()
    {
    }
}
