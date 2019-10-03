using UnityEngine;

public class Item
{
	public string name;
	public int lifeUnits;
	public int manaUnits;
	public int damageUnits;
	public int defenseUnits;
	public int expUnits;
	public int goldUnits;
	
	public Item (string name, int lifeUnits, int manaUnits, int damageUnits, int defenseUnits, int expUnits, int goldUnits)
	{
		this.name = name;
		this.lifeUnits = lifeUnits;
		this.manaUnits = manaUnits;
		this.damageUnits = damageUnits;
		this.defenseUnits = defenseUnits;
		this.expUnits = expUnits;
		this.goldUnits = goldUnits;
	}

	public void DrawItem ()
	{
		Debug.Log("Item: " + this.name);
		Debug.Log("HP: " + this.lifeUnits);
		Debug.Log("Mana: " + this.manaUnits);
		Debug.Log("Damage: " + this.damageUnits);
		Debug.Log("Defense: " + this.defenseUnits);
		Debug.Log("Experience points: " + this.expUnits);
		Debug.Log("Gold: " + this.goldUnits);
	}
}