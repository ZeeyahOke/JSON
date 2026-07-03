using System;

[Serializable]
public class PlayerData
{
    public string playerName;
    public int level;
    public float health;
    public Position position;
    public InventoryItem[] inventory;
}
