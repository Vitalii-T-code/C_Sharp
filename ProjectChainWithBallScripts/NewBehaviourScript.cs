using UnityEngine;

public class ChainAnchor : MonoBehaviour
{
    // Сериализованные поля
    public GameObject firstChainLink;  // Ссылка на первое звено цепи
    public GameObject anchor;          // Ссылка на якорь

    // Несериализованные поля
    private Rigidbody firstLinkRb;

    // Методы жизненного цикла Unity
    void Start()
    {
        InitializeFirstLink();
        AddRigidbodyToAnchor();
        ApplyInitialForceToAnchor();
    }

    void Update()
    {
        CheckAndResetFirstLinkPosition();
    }

    // Публичные методы
    // (нет публичных методов в данном классе)

    // Все остальные методы
    private void InitializeFirstLink()
    {
        // Получаем компонент Rigidbody первого звена цепи
        firstLinkRb = firstChainLink.GetComponent<Rigidbody>();

        // Устанавливаем положение первого звена цепи, чтобы оно было фиксировано в воздухе
        firstLinkRb.isKinematic = true;
    }

    private void AddRigidbodyToAnchor()
    {
        // Добавляем Rigidbody к якорю, чтобы он мог падать
        Rigidbody anchorRb = anchor.AddComponent<Rigidbody>();
        anchorRb.mass = 50;
    }

    private void ApplyInitialForceToAnchor()
    {
        // Задаем начальное направление и силу падения якоря
        Rigidbody anchorRb = anchor.GetComponent<Rigidbody>();
        Vector3 initialForce = new Vector3(1, -1, 0); // Вправо и вниз
        anchorRb.AddForce(initialForce * anchorRb.mass, ForceMode.Impulse);
    }

    private void CheckAndResetFirstLinkPosition()
    {
        // Проверяем, если первое звено цепи отклоняется от начального положения
        if (firstChainLink.transform.position != transform.position)
        {
            // Возвращаем первое звено цепи в начальное положение
            firstChainLink.transform.position = transform.position;
        }
    }
}
