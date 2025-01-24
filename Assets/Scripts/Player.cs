using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Inisialisasi")]
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator anime;

    [Header("Settings")]
    [SerializeField] private float gravity = 5f;
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float runSpeed = 25f;  // Tambahkan kecepatan saat berlari
        
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI indicatorText;

    private Rigidbody rb;
    private Transform currentHidingSpot;
    private Vector3 moveInput;
    private bool isHidden = false;
    private bool isNearHideBox = false;
    private bool isRunning = false; // Tambahkan variabel untuk mengecek apakah sedang berlari

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        indicatorText.enabled = false;
    }

    void Update()
    {
        HandleMovement();

        if (isNearHideBox && Input.GetKeyDown(KeyCode.E))
        {
            ToggleHide();
        }
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

        // Cek jika tombol Shift ditekan
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        float currentSpeed = isRunning ? runSpeed : moveSpeed;
        rb.velocity = moveInput * currentSpeed;

          if (moveInput != Vector3.zero)
    {
        if (isRunning)
        {
            anime.SetBool("move-left-run", true);
            anime.SetBool("move-left", false);
        }
        else
        {
            anime.SetBool("move-left-run", false);
            anime.SetBool("move-left", true);
        }
    }
    else
    {
        anime.SetBool("move-left", false);
        anime.SetBool("move-left-run", false);
    }


        // Membalik sprite sesuai arah pergerakan
        if (moveInput.x > 0)
        {
            sprite.flipX = true;
        }
        else if (moveInput.x < 0)
        {
            sprite.flipX = false;
        }
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
