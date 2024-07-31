using UnityEngine;

public class Coin : MonoBehaviour
{
    // Функция, вызываемая при входе в триггер
    private void OnTriggerEnter(Collider collision)
    {
        // Проверка тега объекта, с которым произошло столкновение
        if (collision.gameObject.tag == "Player")
        {
            // Уничтожение объекта монетки
            Destroy(gameObject);
        }
    }
}
