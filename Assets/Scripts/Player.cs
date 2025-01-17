using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Inisialisasi")]
    [SerializeField] private SpriteRenderer sprite;

    [Header("Settings")]
    [SerializeField] private float gravity = 5f;
    [SerializeField] private float moveSpeed = 5f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI indicatorText;

    private Rigidbody rb;
    private Transform cameraPosition;
    private Vector3 moveInput;
    private bool isHidden = false;
    private bool isNearHideBox = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraPosition = Camera.main.transform;
        indicatorText.enabled = false;

    }

    void Update()
    {
        HandleMovement();
        HandleCamera();

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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HidingSpot"))
        {
            isNearHideBox = false;
            indicatorText.enabled = false;
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

        if (moveInput.x > 0)
        {
            sprite.flipX = true;
        }
        else if (moveInput.x < 0)
        {
            sprite.flipX = false;
        }

        rb.velocity = moveInput * moveSpeed;
    }

    private void HandleCamera()
    {
        Vector3 playerPosition = transform.position;
        float cameraOffsetZ = -10f;
        cameraPosition.position = new Vector3(playerPosition.x, cameraPosition.position.y, playerPosition.z + cameraOffsetZ);
    }

    public void ToggleHide()
    {
        isHidden = !isHidden;
        SetChildrenRenderers(isHidden);
    }

    private void SetChildrenRenderers(bool hide)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(!hide);
        }

        Debug.Log("Player bersembunyi = " + hide);
    }

}
