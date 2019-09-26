using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Labyrinth
{
	List<Room> labyrinth;
	Player player;
	int currentIndexRoom;

	public Labyrinth(Player player)
	{
		this.labyrinth = new List<Room>();
		this.currentIndexRoom = -1;
		this.player = player;
	}

	public void AddRoom(Room room)
	{
		this.labyrinth.Add(room);
	}

	public void ChangeRoom()
	{
		this.currentIndexRoom++;
		
		Debug.Log("Entering new room...");

		if (this.labyrinth.Count >= this.currentIndexRoom)
		{
			Debug.Log("Advancing towards next room.");
			this.labyrinth[this.currentIndexRoom].EnterRoom();

			switch (GetCurrentRoom().roomType)
			{
				case Room.RoomType.ITEM:
					this.player.ApplyItem(GetCurrentRoom().item);
					ChangeRoom();
					break;
				case Room.RoomType.TRAP:
					this.player.ApplyTrap(GetCurrentRoom().trap);
					ChangeRoom();
					break;
				default:
					break;
			}
		}
	}

	public Room GetCurrentRoom()
	{
		if (this.currentIndexRoom < this.labyrinth.Count && this.labyrinth[this.currentIndexRoom] != null)
			return this.labyrinth[this.currentIndexRoom];

		return null;
	}

	public void UpdateLabyrinth(float time)
	{
		if (GetCurrentRoom().roomType == Room.RoomType.ENEMY)
			GetCurrentRoom().enemy.UpdateEnemy(time);
	}

	public bool IsFinished()
	{
		return this.labyrinth.Count <= this.currentIndexRoom;
	}
}