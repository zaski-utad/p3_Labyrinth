using UnityEngine;

public class Trap
{
	public string name;
	public int lifeDamage;
	public int manaDamage;

	public Trap(string name, int lifeDamage, int manaDamage)
	{
		this.name = name;
		this.lifeDamage = lifeDamage;
		this.manaDamage = manaDamage;
	}

	public void ActivateTrap ()
	{
		Debug.Log("Trap: " + this.name);
		Debug.Log("HP damage: " + this.lifeDamage);
		Debug.Log("Mana damage: " + this.manaDamage);
	}
}