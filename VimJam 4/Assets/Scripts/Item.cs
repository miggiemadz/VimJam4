using UnityEngine;

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

	public static Sprite rockSprite;
	public static Sprite bottleSprite;
	public static Sprite canSprite;
	public static Sprite catSprite;

	public static Sprite GetSprite(ItemType itemType)
	{
		switch (itemType)
		{
			case ItemType.Can:
				if (canSprite == null)
				{
					canSprite = Resources.Load<Sprite>("Art/can");
				}
				return canSprite;
			case ItemType.ExplodingCat:
				if (catSprite == null)
				{
					catSprite = Resources.Load<Sprite>("Art/cat");
				}
				return catSprite;
		}
		if (bottleSprite == null)
		{
			bottleSprite = Resources.Load<Sprite>("Art/bottle");
			Debug.Log(bottleSprite);
		}
		return bottleSprite;
	}

	public Item(ItemType type)
	{
		this.type = type;
	}
}
