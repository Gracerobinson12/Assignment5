using UnityEngine;

/// <summary>
/// Instant-lose hazard. Touching this ends the run immediately,
/// same as falling off the platform.
/// </summary>
public class SpikeHazard : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
        }
    }
}
