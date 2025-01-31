using UnityEngine;
using TMPro;

public class BarangPenting : MonoBehaviour
{
    [Header("Inisialisasi")]
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private Barang barang;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI indicatorText;

    private InventoryManager inventoryManager;
    private bool isPlayerNearby = false;

    private void Start()
    {
        image.sprite = barang.image;
        inventoryManager = InventoryManager.instance;
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            bool result = inventoryManager.AddItem(barang);
            if (result)
            {
                Destroy(gameObject);
                Debug.Log("Item berhasil disimpan");
                indicatorText.enabled = false;
            }
            else
            {
                Debug.Log("Gagal tersimpan");
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            indicatorText.enabled = true;
            indicatorText.text = "Ada " + barang.name;
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
