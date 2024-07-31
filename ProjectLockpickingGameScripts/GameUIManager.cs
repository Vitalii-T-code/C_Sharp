using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    private const int WinValue = 5;
    private const float InitialTime = 60f;
    private const int MinPinValue = 0;
    private const int MaxPinValue = 10;

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;

    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _pin1Text;
    [SerializeField] private TextMeshProUGUI _pin2Text;
    [SerializeField] private TextMeshProUGUI _pin3Text;

    private int _pin1, _pin2, _pin3;
    private float _timer;

    void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        _mainMenu.SetActive(true);
        _gameScreen.SetActive(false);
        _winScreen.SetActive(false);
        _loseScreen.SetActive(false);
    }

    public void StartGame()
    {
        _mainMenu.SetActive(false);
        _gameScreen.SetActive(true);
        _winScreen.SetActive(false);
        _loseScreen.SetActive(false);

        ResetGame();
    }

    public void ShowWinScreen()
    {
        _mainMenu.SetActive(false);
        _gameScreen.SetActive(false);
        _winScreen.SetActive(true);
        _loseScreen.SetActive(false);
    }

    public void ShowLoseScreen()
    {
        _mainMenu.SetActive(false);
        _gameScreen.SetActive(false);
        _winScreen.SetActive(false);
        _loseScreen.SetActive(true);
    }

    public void RestartGame()
    {
        StartGame();
    }

    private void ResetGame()
    {
        _timer = InitialTime;
        _pin1 = Random.Range(MinPinValue, MaxPinValue + 1);
        _pin2 = Random.Range(MinPinValue, MaxPinValue + 1);
        _pin3 = Random.Range(MinPinValue, MaxPinValue + 1);

        UpdatePinText();
    }

    void Update()
    {
        if (_gameScreen.activeSelf)
        {
            _timer -= Time.deltaTime;
            _timerText.text = ((int)_timer).ToString();

            if (_timer <= 0)
            {
                _timer = 0;
                ShowLoseScreen();
            }

            CheckWinCondition();
        }
    }

    public void UseTool(string tool)
    {
        switch (tool)
        {
            case "дрель":
                ModifyPinValues(1, -1, 0);
                break;
            case "молоток":
                ModifyPinValues(-1, 2, -1);
                break;
            case "отмычка":
                ModifyPinValues(-1, 1, 1);
                break;
        }
    }

    private void ModifyPinValues(int pin1Change, int pin2Change, int pin3Change)
    {
        _pin1 = Mathf.Clamp(_pin1 + pin1Change, MinPinValue, MaxPinValue);
        _pin2 = Mathf.Clamp(_pin2 + pin2Change, MinPinValue, MaxPinValue);
        _pin3 = Mathf.Clamp(_pin3 + pin3Change, MinPinValue, MaxPinValue);
        UpdatePinText();
        CheckWinCondition();
    }

    private void UpdatePinText()
    {
        _pin1Text.text = _pin1.ToString();
        _pin2Text.text = _pin2.ToString();
        _pin3Text.text = _pin3.ToString();
    }

    private void CheckWinCondition()
    {
        if (_pin1 == WinValue && _pin2 == WinValue && _pin3 == WinValue)
        {
            ShowWinScreen();
        }
    }
}
