public class Room
{
	public enum RoomType { ENEMY, ITEM, TRAP};

	public RoomType roomType;
	public Trap trap;
	public Enemy enemy;
	public Item item;

	public Room(RoomType type, Trap trap)
	{
		this.roomType = RoomType.TRAP;
		this.trap = trap;
	}

	public Room(RoomType type, Enemy enemy)
	{
		this.roomType = RoomType.ENEMY;
		this.enemy = enemy;
	}

	public Room(RoomType type, Item item)
	{
		this.roomType = RoomType.ITEM;
		this.item = item;
	}

	public void EnterRoom()
	{
		switch (this.roomType)
		{
			case RoomType.ENEMY:
				this.enemy.Greet();
				break;
			case RoomType.ITEM:
				this.item.DrawItem();
				break;
			case RoomType.TRAP:
				this.trap.ActivateTrap();
				break;
		}
	}
}
