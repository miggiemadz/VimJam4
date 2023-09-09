public enum ItemType {
	BoomBox,
	Bottle,
	Rock,
}

public class Item {
	ItemType type;

	public Item(ItemType type) {
		this.type = type;
	}
}
