using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    private const float explosionForce = 3000.0f;
    private const float explosionRadius = 5.0f;

    [SerializeField] private Transform explosionPoint;

    private void Start()
    {
        ApplyExplosionForce();
    }

    private void ApplyExplosionForce()
    {
        Collider[] colliders = Physics.OverlapSphere(explosionPoint.position, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, explosionPoint.position, explosionRadius);
            }
        }
    }
}
