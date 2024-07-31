using UnityEngine;

public class CueBallController : MonoBehaviour
{
    // Константы
    private const float DefaultInitialForce = 5f;
    private static readonly Vector3 InitialDirection = new Vector3(1, 0, 1).normalized;

    // Сериализованные поля
    [SerializeField]
    private float initialForce = DefaultInitialForce;

    // Методы жизненного цикла Unity
    private void Start()
    {
        ApplyInitialForce();
    }

    // Публичные методы

    // Все остальные методы
    private void ApplyInitialForce()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(InitialDirection * initialForce, ForceMode.Impulse);
        }
    }

    // Встроенные типы
}
