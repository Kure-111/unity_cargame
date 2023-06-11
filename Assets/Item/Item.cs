using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{
    public Type type; // 種類
    public String infomation; // 情報
    public Sprite sprite; // 画像(追加)

    public enum Type
    {
        BoostItem,
        AttackItem,
        BarrierItem,
    }

    public Item(Item item)
    {
        this.type = item.type;
        this.infomation = item.infomation;
        this.sprite = item.sprite; // 画像(追加)
    }
}
