using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Barang")]
public class Barang : ScriptableObject
{
    [Header("Hanya gameplay")]
    public ItemType type;
    public ActionType action;
    public int unlockID;
    public int healAmount;
    public float effectDuration;

    [Header("Hanya UI")]
    public string description;

    [Header("Keduanya")]
    public new string name;
    public Sprite image;
}

public enum ItemType
{
    KeyItem,
    Memo,
    Consumable
}

public enum ActionType
{
    Heal,
    Protect,
    Examine
}
