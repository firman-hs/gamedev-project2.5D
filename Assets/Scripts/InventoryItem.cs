using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image image;

    [HideInInspector] public Barang item;
    
    public void InitialiseItem(Barang newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
    }
}
