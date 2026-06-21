using UnityEngine;

/// <summary>
/// Pushes the player sideways while they are standing on the conveyor,
/// making it harder to control the ball and easier to fall off the edge.
/// </summary>
public class ConveyorHazard : MonoBehaviour
{
    public float pushForce = 4f;

    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerMovement pm = collider.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.externalSidePush = pushForce;
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerMovement pm = collider.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.externalSidePush = 0f;
            }
        }
    }
}
