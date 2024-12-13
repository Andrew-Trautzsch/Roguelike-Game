[System.Serializable]
public class ItemList
{
    public Items item;
    public string name;
    public int stacks;

    public ItemList(Items newItem, string newName, int newStacks)
    {
        this.item = newItem;
        this.name = newName;
        this.stacks = newStacks;
    }
}