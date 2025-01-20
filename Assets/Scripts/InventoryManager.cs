using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [Header("Prefab")]
    public GameObject inventoryItemPrefab;

    [Header("Pegangan")]
    public InventorySlot[] inventorySlots;

    [Header("Tas")]
    public Tas tas;
    private bool isShown = false;


    int selectedSlot = -1;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ChangeSelectedSlot(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            int newSlot = (selectedSlot + 1) % inventorySlots.Length;
            ChangeSelectedSlot(newSlot);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            GetSelectedItem();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }

        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(Barang item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(Barang item, InventorySlot slot)
    {
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Barang GetSelectedItem()
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Barang item = itemInSlot.item;
            Debug.Log(item.name + " digunakan!");
            return itemInSlot.item;
        }

        Debug.Log("Tidak memiliki apapun");
        return null;
    }

    public void ToggleInventory()
    {
        isShown = !isShown;
        
        tas.gameObject.SetActive(isShown);
        
        Time.timeScale = isShown ? 0 : 1;
        Debug.Log("Inventory ditampilkan = " + !isShown);
    }

}
