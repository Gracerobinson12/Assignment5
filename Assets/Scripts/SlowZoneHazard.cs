using UnityEngine;

/// <summary>
/// Slows the player's forward speed for a short time, making it harder
/// to react to upcoming hazards and gaps.
/// </summary>
public class SlowZoneHazard : MonoBehaviour
{
    public float slowSpeed = 3f;
    public float slowDuration = 2f;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerMovement pm = collider.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.ActivateSpeedBoost(slowSpeed, slowDuration);
            }
        }
    }
}
