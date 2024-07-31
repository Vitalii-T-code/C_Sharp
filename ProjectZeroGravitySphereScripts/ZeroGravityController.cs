using UnityEngine;
using System.Collections;

public class ZeroGravityController : MonoBehaviour
{
    private const float sinkDuration = 1.0f; // Константа

    [SerializeField] private float gravityOffDuration = 3.0f; // Сериализованное поле

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            StartCoroutine(DisableGravityTemporarily(other.gameObject));
        }
    }

    private IEnumerator DisableGravityTemporarily(GameObject ball)
    {
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;

            // Ожидание заданного времени
            yield return new WaitForSeconds(gravityOffDuration);

            rb.useGravity = true;
        }
    }
}
