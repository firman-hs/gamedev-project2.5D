using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image image;

    [HideInInspector] public Barang barang;
    
    public void InitialiseItem(Barang barangBaru)
    {
        barang = barangBaru;
        image.sprite = barangBaru.image;
    }
}
