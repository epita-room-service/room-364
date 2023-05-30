
using UnityEngine;

[CreateAssetMenu(fileName = "Item",menuName = "Items/New item")]
public class ItemData : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite visual;
    public GameObject prefab;

    public ItemType itemType;
}

public enum ItemType
{
    // Tout ce qui peut être équipé (ex: une lampe torche)
    Equipment,
    // Tout ce qui est retiré de l'inventaire après utilisation (ex: un fusible)
    Consumable,
    // Tout ce qui peut être utilisé plusieurs fois (ex: une clé)
    NotConsumable
}