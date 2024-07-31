using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Rigidbody _ballRigidbody; // Rigidbody шарика
    [SerializeField] private float _forceMultiplier = 10f; // множитель силы для управления движением шарика

    void Update()
    {
        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 forceDirection = new Vector3(touch.deltaPosition.x, 0, touch.deltaPosition.y).normalized;
                _ballRigidbody.AddForce(forceDirection * _forceMultiplier);
            }
        }
    }
}
