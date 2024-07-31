using UnityEngine;

public class WallTrigger : MonoBehaviour
{
    [SerializeField] private InteractionController interactionController;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactionController.SetIsNearWall(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactionController.SetIsNearWall(false);
        }
    }
}
