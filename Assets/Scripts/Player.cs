using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Inisialisasi")]
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float maxStamina = 100f;

    [Header("Settings")]
    [SerializeField] private float gravity = 5f;
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float staminaDrainRate = 10f;
    [SerializeField] private float staminaRegenRate = 5f;
    [SerializeField] private float minStaminaToRun = 10f;

    private Rigidbody rb;
    private Vector3 moveInput;
    private bool isExhausted = false;
    private float currentStamina;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        currentStamina = maxStamina;
        HUDController.instance.InitiateStaminaBar(maxStamina);
    }

    void Update()
    {
        HandleMovement();
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector3.down * gravity);
    }
    
    private void HandleMovement()
    {

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.z = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;

        bool isRunning = moveInput.magnitude > 0 && Input.GetKey(KeyCode.LeftShift) && !isExhausted;
        float currentSpeed = isRunning ? moveSpeed * 1.5f : moveSpeed;

        if (isRunning)
        {

            currentStamina -= staminaDrainRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

            if (currentStamina <= 0)
            {
                isExhausted = true;
            }

            HUDController.instance.UpdateStaminaBar(currentStamina);
        }
        else
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

            if (currentStamina >= minStaminaToRun)
            {
                isExhausted = false;
            }

            if (currentStamina >= maxStamina)
            {
                HUDController.instance.DisableStaminaBar();
            }
            else
            {
                HUDController.instance.UpdateStaminaBar(currentStamina);
            }
        }


        if (moveInput.x > 0)
        {
            sprite.flipX = true;
        }
        else if (moveInput.x < 0)
        {
            sprite.flipX = false;
        }

        rb.velocity = moveInput * currentSpeed;
    }
}
