using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    [SerializeField] private string message;
    [SerializeField] private KeyCode interactionKey;

    private bool isPlayerNearby = false;
    private bool isHidden = false;
    private GameObject player;

    private void Start()
    {
        message += $" ({interactionKey})";
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(interactionKey))
        {
            ToggleHide();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            player = other.gameObject;
            HUDController.instance.EnableInteractionText(message);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            player = null;
            HUDController.instance.DisableInteractionText();
        }
    }

    public void ToggleHide()
    {
        isHidden = !isHidden;
        if (isHidden)
        {
            player.transform.position = transform.position;
            HUDController.instance.EnableInteractionText($"Keluar ({interactionKey})");
        }
        else
        {
            Vector3 exitPosition = transform.position;
            exitPosition.z += -2f;
            player.transform.position = exitPosition;
        }

        player.SetActive(!isHidden);
    }
}