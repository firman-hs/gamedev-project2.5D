using UnityEngine;
using TMPro;

public class BarangPenting : MonoBehaviour
{
    [Header("Inisialisasi")]
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private Barang item;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI indicatorText;

    private InventoryManager inventoryManager;
    private bool isPlayerNearby = false;

    private void Start()
    {
        image.sprite = item.image;
        inventoryManager = InventoryManager.instance;
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            bool result = inventoryManager.AddItem(item);
            if (result)
            {
                Destroy(gameObject);
                indicatorText.enabled = false;

                Debug.Log("Item berhasil disimpan");
            }
            else
            {
                Barang swapItem = inventoryManager.ChangeItem(item);
                item = swapItem;
                image.sprite = item.image;
                indicatorText.text = "Ada " + item.name;

                Debug.Log("Item berhasil diganti");
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            indicatorText.enabled = true;
            indicatorText.text = "Ada " + item.name;
}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            indicatorText.enabled = false;
        }
    }
}
