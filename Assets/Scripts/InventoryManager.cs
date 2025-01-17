using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [Header("Prefab")]
    public GameObject inventoryItemPrefab;

    [Header("Pegangan")]
    public InventorySlot[] inventorySlots;

    private void Awake()
    {
        instance = this;
    }

    public bool AddItem(Barang barang)
    {
        // Cari slot inventory yang kosong
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(barang, slot);
                return true;
            }
        }

        return false;
    }

    public void SpawnNewItem(Barang barang, InventorySlot slot)
    {
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(barang);
    }
}
