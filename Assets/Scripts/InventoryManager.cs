using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [Header("Prefab")]
    public GameObject inventoryItemPrefab;

    [Header("Pegangan")]
    public InventorySlot[] peganganSlots;

    [Header("Tas")]
    public Tas tas;
    public InventorySlot[] inventorySlots;
    public TMP_Text itemName;
    public TMP_Text description;

    bool isShown = false;
    int selectedSlot = -1;
    int activeSlotIndex = -1;

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
        if (!isShown && Input.GetKeyDown(KeyCode.Q))
        {
            int newSlot = (selectedSlot + 1) % peganganSlots.Length;
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

        if (isShown && Input.GetKeyDown(KeyCode.Q))
        {
            ChangeActiveSlot(-1); // Berpindah ke slot sebelumnya
        }

        if (isShown && Input.GetKeyDown(KeyCode.E))
        {
            ChangeActiveSlot(1); // Berpindah ke slot berikutnya
        }
    }

    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            peganganSlots[selectedSlot].Deselect();
        }

        peganganSlots[newValue].Select();
        selectedSlot = newValue;
    }

    public Barang ChangeItem(Barang item)
    {
        InventorySlot slot = peganganSlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        Barang barang = itemInSlot.item;

        itemInSlot.InitialiseItem(item);

        return barang;
    }

    public bool AddItem(Barang item)
    {
        for (int i = 0; i < peganganSlots.Length; i++)
        {
            InventorySlot slot = peganganSlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    public void AddKeyItem(Barang item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return;
            }
        }
    }

    void SpawnNewItem(Barang item, InventorySlot slot)
    {
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Barang GetSelectedItem()
    {
        InventorySlot slot = peganganSlots[selectedSlot];
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

        if (isShown)
        {
            ShowSlot(0);
        }
        else
        {
            HideAllSlots();
        }

        Time.timeScale = isShown ? 0 : 1;
        Debug.Log("Inventory ditampilkan = " + isShown);
    }

    private void ShowSlot(int index)
    {
        HideAllSlots();
        
        if (index >= 0 && index < inventorySlots.Length)
        {
            InventorySlot slot = inventorySlots[index];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                slot.gameObject.SetActive(true);
                itemName.text = itemInSlot.item.name;
                description.text = itemInSlot.item.description;

                activeSlotIndex = index;
            }
        }
    }

    private void HideAllSlots()
    {
        foreach (var slot in inventorySlots)
        {
            slot.gameObject.SetActive(false);
        }
    }

    public void ChangeActiveSlot(int direction)
    {
        if (!isShown) return;

        int nextIndex = activeSlotIndex + direction;
        
        while (nextIndex >= 0 && nextIndex < inventorySlots.Length)
        {
            InventorySlot slot = inventorySlots[nextIndex];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                ShowSlot(nextIndex);
                return;
            }

            nextIndex += direction;
        }

        Debug.Log("Tidak ada slot yang tersedia ke arah: " + direction);
    }

}
