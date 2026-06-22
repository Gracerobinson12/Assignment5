using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float forwardSpeed = 6f;
    [SerializeField] float sideSpeed = 4f;

    Rigidbody rb;
    Vector2 moveInput;
    float currentSideInput;
    float sideVelocity;
    float originalForwardSpeed;

    [HideInInspector] public float externalSidePush = 0f;
    bool controlsLocked = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalForwardSpeed = forwardSpeed;
    }

    public void ActivateSpeedBoost(float boostSpeed, float duration)
    {
        StartCoroutine(SpeedBoostRoutine(boostSpeed, duration));
    }

    public void LockControls()
    {
        controlsLocked = true;
        if (rb != null) rb.linearVelocity = Vector3.zero;
    }

    private IEnumerator SpeedBoostRoutine(float boostSpeed, float duration)
    {
        forwardSpeed = boostSpeed;
        yield return new WaitForSeconds(duration);
        forwardSpeed = originalForwardSpeed;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        if (controlsLocked) return;

        currentSideInput = Mathf.SmoothDamp(currentSideInput, moveInput.x, ref sideVelocity, 0.1f);

        Vector3 movement = new Vector3(
            currentSideInput * sideSpeed + externalSidePush,
            rb.linearVelocity.y,
            forwardSpeed
        );

        rb.linearVelocity = movement;
    }

    void Update() { }
}
