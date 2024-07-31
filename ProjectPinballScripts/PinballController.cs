using UnityEngine;

public class PinballController : MonoBehaviour
{
    // Константы
    public KeyCode flipperLeftKey = KeyCode.A;
    public KeyCode flipperRightKey = KeyCode.S;
    public float restRotationAngleLeft = 0f;
    public float activeRotationAngleLeft = 45f;
    public float restRotationAngleRight = 0f;
    public float activeRotationAngleRight = -45f;
    public float flipperSpeed = 40f;
    public float launchForce = 10f;
    public float launchInterval = 2f;
    public float wallBounceForce = 5f;
    public float bumperBounceForce = 10f;
    public float constantForceMagnitude = 1f;

    // Сериализованные поля
    public GameObject flipperLeft;
    public GameObject flipperRight;
    public Transform ballResetPoint;

    // Несериализованные поля
    private Quaternion restRotationLeft;
    private Quaternion activeRotationLeft;
    private Quaternion restRotationRight;
    private Quaternion activeRotationRight;
    private float launchTimer;
    private bool ballLaunched = false;
    private Rigidbody ballRigidbody;

    // Методы жизненного цикла Unity
    private void Start()
    {
        InitializeFlipperRotations();
        FindBall();
    }

    private void Update()
    {
        HandleFlipperControls();
        LaunchBallTimer();
    }

    private void FixedUpdate()
    {
        ApplyConstantForceToBall();
    }

    // Публичные методы
    // (нет публичных методов в данном классе)

    // Все остальные методы
    private void InitializeFlipperRotations()
    {
        restRotationLeft = Quaternion.Euler(0, restRotationAngleLeft, 0);
        activeRotationLeft = Quaternion.Euler(0, activeRotationAngleLeft, 0);
        restRotationRight = Quaternion.Euler(0, restRotationAngleRight, 0);
        activeRotationRight = Quaternion.Euler(0, activeRotationAngleRight, 0);
    }

    private void FindBall()
    {
        GameObject ball = GameObject.Find("Ball");
        if (ball != null)
        {
            ballRigidbody = ball.GetComponent<Rigidbody>();
        }
    }

    private void HandleFlipperControls()
    {
        // Управление левой лопастью
        if (Input.GetKey(flipperLeftKey))
        {
            flipperLeft.transform.localRotation = Quaternion.Lerp(flipperLeft.transform.localRotation, activeRotationLeft, Time.deltaTime * flipperSpeed);
        }
        else
        {
            flipperLeft.transform.localRotation = Quaternion.Lerp(flipperLeft.transform.localRotation, restRotationLeft, Time.deltaTime * flipperSpeed);
        }

        // Управление правой лопастью
        if (Input.GetKey(flipperRightKey))
        {
            flipperRight.transform.localRotation = Quaternion.Lerp(flipperRight.transform.localRotation, activeRotationRight, Time.deltaTime * flipperSpeed);
        }
        else
        {
            flipperRight.transform.localRotation = Quaternion.Lerp(flipperRight.transform.localRotation, restRotationRight, Time.deltaTime * flipperSpeed);
        }
    }

    private void LaunchBallTimer()
    {
        launchTimer += Time.deltaTime;
        if (launchTimer >= launchInterval && !ballLaunched)
        {
            LaunchBall();
            launchTimer = 0f;
        }
    }

    private void LaunchBall()
    {
        GameObject ball = GameObject.Find("Ball");
        if (ball != null)
        {
            ballRigidbody = ball.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                ballRigidbody.velocity = Vector3.zero;
                ballRigidbody.AddForce(new Vector3(0, 0.5f, 1) * launchForce, ForceMode.Impulse);
                ballLaunched = true;
            }
        }
    }

    private void ApplyConstantForceToBall()
    {
        if (ballRigidbody != null && ballLaunched)
        {
            ballRigidbody.AddForce(ballRigidbody.velocity.normalized * constantForceMagnitude);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ResetBall(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            HandleBallCollision(collision);
        }
    }

    private void HandleBallCollision(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 bounceDirection = Vector3.Reflect(rb.velocity, collision.contacts[0].normal).normalized;
            if (collision.collider.name.Contains("Bumper"))
            {
                rb.AddForce(bounceDirection * bumperBounceForce, ForceMode.Impulse);
            }
            else if (collision.collider.name.Contains("Wall"))
            {
                rb.AddForce(bounceDirection * wallBounceForce, ForceMode.Impulse);
            }

            if (collision.collider.name == "Wall1")
            {
                ResetBall(collision.gameObject);
            }
        }
    }

    private void ResetBall(GameObject ball)
    {
        ball.transform.position = ballResetPoint.position;
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
        }
        ballLaunched = false; // Позволяет перезапуск шарика
    }
}
