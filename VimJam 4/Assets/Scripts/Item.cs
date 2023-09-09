public enum ItemType {
	Rock,
	Bottle,
	BoomBox,
}

public class Item {
	public ItemType type;

	public Item(ItemType type) {
		this.type = type;
	}
}
