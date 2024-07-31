using UnityEngine;

public class BallController : MonoBehaviour
{
    // Поле для задания скорости движения, доступное в инспекторе Unity
    [SerializeField] private float _movementSpeed = 5f;

    // Поле для хранения ссылки на компонент Rigidbody
    private Rigidbody _rigidbody;

    // Метод жизненного цикла Unity для инициализации
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Метод жизненного цикла Unity для обновления каждый кадр
    private void Update()
    {
        HandleMovement();
    }

    // Приватный метод для обработки движения шарика
    private void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        _rigidbody.AddForce(movement * _movementSpeed);
    }
}
