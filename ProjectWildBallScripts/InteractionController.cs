using UnityEngine;
using TMPro;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private string message = "Нажмите E";
    [SerializeField] private TMP_Text interactionText;
    [SerializeField] private GameObject wall;
    [SerializeField] private float moveSpeed = 2f;

    private bool isNearWall = false;
    private bool isWallMoving = false;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private Rigidbody wallRigidbody;

    private void Start()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
        initialPosition = wall.transform.position;
        float wallHeight = wall.GetComponent<Renderer>().bounds.size.y;
        targetPosition = initialPosition + new Vector3(0, wallHeight * 2, 0);
        wallRigidbody = wall.GetComponent<Rigidbody>();
        wallRigidbody.isKinematic = true;
    }

    private void Update()
    {
        if (isNearWall && !isWallMoving)
        {
            if (interactionText != null)
            {
                interactionText.text = message;
                interactionText.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                isWallMoving = true;
                if (interactionText != null)
                {
                    interactionText.gameObject.SetActive(false);
                }
                wallRigidbody.isKinematic = false;
            }
        }

        if (isWallMoving)
        {
            MoveWall();
        }
    }

    private void MoveWall()
    {
        Vector3 newPosition = Vector3.MoveTowards(wall.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        wallRigidbody.MovePosition(newPosition);

        if (wall.transform.position == targetPosition)
        {
            isWallMoving = false;
            wallRigidbody.isKinematic = true;
        }
    }

    public void SetIsNearWall(bool value)
    {
        isNearWall = value;
    }
}
