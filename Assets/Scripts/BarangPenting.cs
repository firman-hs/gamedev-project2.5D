using UnityEngine;

public class BarangPenting : MonoBehaviour
{
    [Header("Inisialisasi")]
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private Barang item;
    [SerializeField] private string message;
    [SerializeField] private KeyCode interactionKey;

    private InventoryManager inventoryManager;
    private bool isPlayerNearby = false;
    private string newMessage;

    private void Start()
    {
        image.sprite = item.image;
        inventoryManager = InventoryManager.instance;
        newMessage = $"{message} {item.name} ({interactionKey})";
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(interactionKey))
        {
            if(item.type != ItemType.Consumable )
            {
                inventoryManager.AddKeyItem(item);

                Destroy(gameObject);
                HUDController.instance.DisableInteractionText();
                Debug.Log("Item berhasil disimpan");
            }
            else
            {
                bool result = inventoryManager.AddItem(item);
                if (result)
                {
                    Destroy(gameObject);
                    HUDController.instance.DisableInteractionText();
                    Debug.Log("Item berhasil disimpan");
                }
                else
                {
                    Barang swapItem = inventoryManager.ChangeItem(item);
                    item = swapItem;
                    image.sprite = item.image;

                    newMessage = $"{message} {item.name} ({interactionKey})";
                    HUDController.instance.EnableInteractionText(newMessage);
                    Debug.Log("Item berhasil diganti");
                }
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            
            HUDController.instance.EnableInteractionText(newMessage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;

            HUDController.instance.DisableInteractionText();
        }
    }
}
