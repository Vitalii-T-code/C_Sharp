using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject rulesMenu;
    public GameObject authorMenu;
    public GameObject characterSelectMenu;

    private Button playButton;
    private Button rulesButton;
    private Button authorButton;

    private Button rulesBackButton;
    private Button authorBackButton;
    private Button characterBackButton;

    void Start()
    {
        // Привязываем кнопки главного меню
        playButton = mainMenu.transform.Find("PlayButton").GetComponent<Button>();
        rulesButton = mainMenu.transform.Find("RulesButton").GetComponent<Button>();
        authorButton = mainMenu.transform.Find("AuthorButton").GetComponent<Button>();

        playButton.onClick.AddListener(ShowCharacterSelectMenu);
        rulesButton.onClick.AddListener(ShowRulesMenu);
        authorButton.onClick.AddListener(ShowAuthorMenu);

        // Привязываем кнопки меню с правилами
        rulesBackButton = rulesMenu.transform.Find("BackButton").GetComponent<Button>();
        rulesBackButton.onClick.AddListener(ShowMainMenuFromRules);

        // Привязываем кнопки меню с информацией об авторе
        authorBackButton = authorMenu.transform.Find("BackButton").GetComponent<Button>();
        authorBackButton.onClick.AddListener(ShowMainMenuFromAuthor);

        // Привязываем кнопки меню выбора персонажа
        characterBackButton = characterSelectMenu.transform.Find("BackButton").GetComponent<Button>();
        characterBackButton.onClick.AddListener(ShowMainMenuFromCharacterSelect);

        // Изначально показываем главное меню
        ShowMainMenu();
    }

    void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        rulesMenu.SetActive(false);
        authorMenu.SetActive(false);
        characterSelectMenu.SetActive(false);
    }

    void ShowRulesMenu()
    {
        mainMenu.SetActive(false);
        rulesMenu.SetActive(true);
    }

    void ShowAuthorMenu()
    {
        mainMenu.SetActive(false);
        authorMenu.SetActive(true);
    }

    void ShowCharacterSelectMenu()
    {
        mainMenu.SetActive(false);
        characterSelectMenu.SetActive(true);
    }

    void ShowMainMenuFromRules()
    {
        rulesMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    void ShowMainMenuFromAuthor()
    {
        authorMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    void ShowMainMenuFromCharacterSelect()
    {
        characterSelectMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}


