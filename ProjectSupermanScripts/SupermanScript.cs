using UnityEngine;

public class SupermanScript : MonoBehaviour
{
    // Константы
    private const float DefaultForceStrength = 10f;

    // Сериализованные поля
    [SerializeField]
    private float forceStrength = DefaultForceStrength;

    // Методы жизненного цикла Unity
    private void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        HandleTriggerEnter(other);
    }

    // Публичные методы

    // Все остальные методы
    private void HandleCollision(Collision collision)
    {
        string layerName = LayerMask.LayerToName(collision.gameObject.layer);

        if (layerName == "GoodBoy")
        {
            // Супермен проходит сквозь GoodBoy
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        else if (layerName == "BadGuy")
        {
            Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Наносим урон BadGuy
                Vector3 forceDirection = collision.transform.position - transform.position;
                rb.AddForce(forceDirection.normalized * forceStrength, ForceMode.Impulse);
            }
        }
    }

    private void HandleTriggerEnter(Collider other)
    {
        string layerName = LayerMask.LayerToName(other.gameObject.layer);

        if (layerName == "GoodBoy")
        {
            // Игнорируем столкновение с GoodBoy
            Physics.IgnoreCollision(other, GetComponent<Collider>());
        }
    }

    // Встроенные типы
}
