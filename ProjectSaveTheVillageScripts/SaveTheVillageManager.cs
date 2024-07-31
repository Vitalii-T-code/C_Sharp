using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SaveTheVillageManager : MonoBehaviour
{
    // Сериализованные поля
    [SerializeField] private TextMeshProUGUI _wheatText;
    [SerializeField] private TextMeshProUGUI _warriorsText;
    [SerializeField] private TextMeshProUGUI _peasantsText;
    [SerializeField] private TextMeshProUGUI _enemiesText;
    [SerializeField] private TextMeshProUGUI _harvestTimerText;
    [SerializeField] private TextMeshProUGUI _raidTimerText;
    [SerializeField] private TextMeshProUGUI _eatingTimerText;
    [SerializeField] private TextMeshProUGUI _nextWaveEnemiesText;
    [SerializeField] private Button _hirePeasantButton;
    [SerializeField] private Button _hireWarriorButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _muteButton;
    [SerializeField] private GameObject _deathScreen;
    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private TextMeshProUGUI _deathStatsText;
    [SerializeField] private TextMeshProUGUI _victoryStatsText;

    // Несериализованные поля
    private int _wheat = 500;
    private int _warriors = 0;
    private int _peasants = 1;
    private int _enemies = 0;
    private int _nextWaveEnemies = 0;
    private int _totalCycles = 0;
    private int _totalEnemiesDefeated = 0;
    private int _totalWheatProduced = 0;

    private float _harvestTimer = 5f;
    private float _raidTimer = 10f;
    private float _eatingTimer = 2f;

    private bool _isPaused = false;

    // Методы жизненного цикла Unity
    private void Start()
    {
        InitializeUI();
        StartCoroutine(HarvestCycle());
        StartCoroutine(RaidCycle());
        StartCoroutine(EatingCycle());

        _hirePeasantButton.onClick.AddListener(HirePeasant);
        _hireWarriorButton.onClick.AddListener(HireWarrior);
        _pauseButton.onClick.AddListener(TogglePause);
        _muteButton.onClick.AddListener(AudioManager.Instance.ToggleMute);

        _deathScreen.SetActive(false);
        _victoryScreen.SetActive(false);
        _pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (!_isPaused)
        {
            UpdateTimers();
            CheckButtonsInteractable();
        }
    }

    // Публичные методы
    public void HirePeasant()
    {
        if (_wheat >= 5)
        {
            _peasants += 1;
            _wheat -= 5;
            UpdateUI();
            AudioManager.Instance.PlaySound(AudioManager.Instance.ButtonClickSound);
        }
    }

    public void HireWarrior()
    {
        if (_wheat >= 10)
        {
            _warriors += 1;
            _wheat -= 10;
            UpdateUI();
            AudioManager.Instance.PlaySound(AudioManager.Instance.ButtonClickSound);
        }
    }

    public void TogglePause()
    {
        _isPaused = !_isPaused;
        _pauseScreen.SetActive(_isPaused);
        Time.timeScale = _isPaused ? 0 : 1;
        AudioManager.Instance.PlaySound(AudioManager.Instance.ButtonClickSound);
    }

    // Все остальные методы
    private void InitializeUI()
    {
        UpdateUI();
        UpdateTimers(); // Добавляем вызов для инициализации таймеров
    }

    private void UpdateTimers()
    {
        _harvestTimerText.text = "Цикл сбора урожая: " + Mathf.Ceil(_harvestTimer);
        _raidTimerText.text = "До набега врагов: " + Mathf.Ceil(_raidTimer);
        _eatingTimerText.text = "Цикл еды: " + Mathf.Ceil(_eatingTimer);
        _nextWaveEnemiesText.text = "Врагов в следующем набеге: " + _nextWaveEnemies;
    }

    private void UpdateUI()
    {
        _wheatText.text = "Пшеница: " + _wheat;
        _warriorsText.text = "Воины: " + _warriors;
        _peasantsText.text = "Крестьяне: " + _peasants;
        _enemiesText.text = "Побежденные враги: " + _totalEnemiesDefeated;
    }

    private void CheckButtonsInteractable()
    {
        _hirePeasantButton.interactable = _wheat >= 5;
        _hireWarriorButton.interactable = _wheat >= 10;
    }

    private IEnumerator HarvestCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (!_isPaused)
            {
                _harvestTimer -= 1f;
                if (_harvestTimer <= 0)
                {
                    _wheat += _peasants * 5;
                    _totalWheatProduced += _peasants * 5;
                    _harvestTimer = 5f;
                    UpdateUI();
                }
            }
        }
    }

    private IEnumerator RaidCycle()
    {
        while (true)
        {
            // Устанавливаем количество врагов для следующего набега в начале цикла
            _nextWaveEnemies = Mathf.Min(_totalCycles + 1, 10);
            UpdateTimers();
            UpdateUI();
            yield return new WaitForSeconds(1f);
            if (!_isPaused)
            {
                _raidTimer -= 1f;
                if (_raidTimer <= 0)
                {
                    _totalCycles++;
                    Battle();
                    _raidTimer = 10f;
                    UpdateUI();

                    if (_totalCycles >= 10)
                    {
                        CheckVictory();
                    }
                }
            }
        }
    }

    private IEnumerator EatingCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (!_isPaused)
            {
                _eatingTimer -= 1f;
                if (_eatingTimer <= 0)
                {
                    _wheat -= Mathf.CeilToInt((_peasants + _warriors) * 0.5f);
                    if (_wheat < 0) _wheat = 0;
                    _eatingTimer = 2f;
                    UpdateUI();
                }
            }
        }
    }

    private void Battle()
    {
        if (_warriors >= _nextWaveEnemies)
        {
            _totalEnemiesDefeated += _nextWaveEnemies;
            _warriors -= _nextWaveEnemies;
            _enemies = _nextWaveEnemies;
            _nextWaveEnemies = 0;
        }
        else
        {
            ShowDeathScreen();
        }
        UpdateTimers(); // Обновляем количество врагов после битвы
    }

    private void ShowDeathScreen()
    {
        _isPaused = true;
        Time.timeScale = 0;
        _deathScreen.SetActive(true);
        _deathStatsText.text = $"Циклы нападений: {_totalCycles}\nПроизведено пшеницы: {_totalWheatProduced}\nКрестьяне: {_peasants}\nВоины: {_warriors}\nПавшие враги: {_totalEnemiesDefeated}";
        AudioManager.Instance.PlaySound(AudioManager.Instance.ButtonClickSound);
    }

    private void ShowVictoryScreen()
    {
        _isPaused = true;
        Time.timeScale = 0;
        _victoryScreen.SetActive(true);
        _victoryStatsText.text = $"Поздравляем!\nВы наняли {_peasants} крестьян\nСобрали {_wheat} пшеницы\nПроизвели {_totalWheatProduced} пшеницы\nПавшие враги: {_totalEnemiesDefeated}";
        AudioManager.Instance.PlaySound(AudioManager.Instance.ButtonClickSound);
    }

    private void CheckVictory()
    {
        if (_totalCycles >= 10 && !_deathScreen.activeSelf)
        {
            ShowVictoryScreen();
        }
    }
}
