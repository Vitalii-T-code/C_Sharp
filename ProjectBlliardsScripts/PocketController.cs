using UnityEngine;
using System.Collections;

public class PocketController : MonoBehaviour
{
    // Константы
    private const float SinkDuration = 1.0f;
    private static readonly Vector3 SinkOffset = new Vector3(0, -0.5f, 0);

    // Методы жизненного цикла Unity
    private void OnTriggerEnter(Collider other)
    {
        HandleBallCollision(other);
    }

    // Публичные методы

    // Все остальные методы
    private void HandleBallCollision(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            StartCoroutine(SinkBall(other.gameObject));
        }
    }

    private IEnumerator SinkBall(GameObject ball)
    {
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Отключаем физику для шара
        }

        Vector3 startPosition = ball.transform.position;
        Vector3 endPosition = startPosition + SinkOffset; // Опускаем шар вниз

        float elapsedTime = 0;

        while (elapsedTime < SinkDuration)
        {
            ball.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / SinkDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(ball); // Удаляем шар после анимации
    }

    // Встроенные типы
}
