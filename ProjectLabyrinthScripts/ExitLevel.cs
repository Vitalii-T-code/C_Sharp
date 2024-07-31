using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    // Индекс следующего уровня, который нужно загрузить
    [SerializeField] private int nextLevelIndex;

    // Функция, вызываемая при входе в триггер
    private void OnTriggerEnter(Collider collision)
    {
        // Проверка тега объекта, с которым произошло столкновение
        if (collision.gameObject.tag == "Player")
        {
            // Вызов функции смены сцены
            ChangeScene();
        }
    }

    // Функция для смены сцены
    private void ChangeScene()
    {
        // Загрузка сцены с указанным индексом
        SceneManager.LoadScene(nextLevelIndex);
    }
}
