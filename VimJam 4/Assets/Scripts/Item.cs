public enum ItemType
{
	Rock,
	Bottle,
	Can,
	ExplodingCat,
}

public class Item
{
	public ItemType type;

	public Item(ItemType type)
	{
		this.type = type;
	}
}
