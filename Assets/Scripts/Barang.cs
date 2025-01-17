using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Barang")]
public class Barang : ScriptableObject
{
    [Header("Hanya gameplay")]
    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);

    [Header("Hanya UI")]
    public bool stackable = true;

    [Header("Keduanya")]
    public Sprite image;
}

public enum ItemType
{
    Kunci,
    Sesajen,
    Penyembuh
}

public enum ActionType
{
    Membuka,
    Menaruh,
    Menyembuhkan
}
