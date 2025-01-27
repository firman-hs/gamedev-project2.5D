using UnityEngine;
using TMPro;
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

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI indicatorText;
    [SerializeField] private Slider staminaBar;

    private Rigidbody rb;
    private Transform currentHidingSpot;
    private Vector3 moveInput;
    private bool isHidden = false;
    private bool isNearHideBox = false;
    private bool isExhausted = false;
    private float currentStamina;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        indicatorText.enabled = false;
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = currentStamina;
    }

    void Update()
    {
        HandleMovement();

        if (isNearHideBox && Input.GetKeyDown(KeyCode.E))
        {
            ToggleHide();
        }

        staminaBar.value = currentStamina;
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector3.down * gravity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            isNearHideBox = true;
            indicatorText.enabled = true;
            indicatorText.text = "Tekan 'E' untuk bersembunyi";
            currentHidingSpot = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            isNearHideBox = false;
            indicatorText.enabled = false;
            currentHidingSpot = null;
        }
    }

    private void HandleMovement()
    {
        if (isHidden)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.z = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && !isExhausted;
        float currentSpeed = isRunning ? moveSpeed * 1.5f : moveSpeed;

        if (isRunning)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

            if (currentStamina <= 0)
            {
                isExhausted = true;
            }
        }
        else
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

            if (currentStamina >= minStaminaToRun)
            {
                isExhausted = false;
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

    public void ToggleHide()
    {
        isHidden = !isHidden;
        if (isHidden)
        {
            transform.position = currentHidingSpot.position;
        }
        else
        {
            Vector3 exitPosition = currentHidingSpot.position;
            exitPosition.z += -2f;
            transform.position = exitPosition;
        }
        
        sprite.enabled = !isHidden;
    }

}
