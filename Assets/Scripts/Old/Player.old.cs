using System;
using UnityEngine;

public class Player
{
	public int level;
	private int gold;
	
	private int damage;
	private int defense;
	private int maxLife;
	private int maxMana;
	private int manaCost;
	private int experienceToNextLevel;

	private int currentLife;
	private int currentMana;
	private int currentExperience;

	private Weapon currentWeapon;
	private Shield currentShield;

	public Player    (int damage, int defense, int maxLife, int maxMana, int experienceToNextLevel, Weapon currentWeapon,
		Shield currentShield)
	{
		this.damage					= damage;
		this.defense				= defense;
		this.maxLife				= maxLife;
		this.maxMana				= maxMana;
		this.manaCost				= 10;
		this.experienceToNextLevel	= experienceToNextLevel;

		this.currentLife			= maxLife;
		this.currentMana			= maxMana;
		this.currentExperience		= 0;
		this.level					= 1;
		this.gold					= 0;

		this.currentWeapon 			= currentWeapon;
		this.currentShield 			= currentShield;
	}

	public void AddExperience(int experience)
	{
		if (experience < this.experienceToNextLevel) // This case is prepared for exp gains that are < 0
		{
			this.currentExperience		= Mathf.Max(this.currentExperience + experience, 0);
			this.experienceToNextLevel	= Mathf.Min(this.experienceToNextLevel - experience, CalculateExpToNextLevel());
		}
		else
		{
			LevelUp(experience - this.experienceToNextLevel);
		}
	}

	// For the time being, level scaling will be kept simpler than the mechanism of a botijo
	public int CalculateExpToNextLevel ()
	{
		return this.level * 100;
	}

	public void LevelUp (int experienceCarry)
	{
		this.level++;
		this.currentExperience		= experienceCarry;
		this.experienceToNextLevel	= CalculateExpToNextLevel() - experienceCarry;
		this.damage					+= 10;
		this.defense				+= 10;
		this.maxLife				+= 10;
		this.maxMana				+= 10;
		this.currentLife			= this.maxLife;
		this.currentMana			= this.maxMana;
	}

	public void ReceiveDamage (int damage)
	{
		int actualDamage = 1 + (int) Mathf.Floor(damage / (1 + Mathf.Log(this.defense + this.currentShield.resistance)));
		this.currentLife -= actualDamage;
	}

	public void Attack (Enemy enemy)
	{
		int actualDamage = 1 + Mathf.Max(this.damage, currentWeapon.damage);
		enemy.ReceiveDamage(actualDamage);
		
		Debug.Log("You deal " + actualDamage + " to enemy " + enemy.name);
	}

	public void StrongAttack (Enemy enemy)
	{
		if (this.currentMana - this.manaCost >= 0)
		{
			int actualDamage = 1 + (int) (currentWeapon.damage + this.damage * Math.Floor(Math.Sqrt(this.level)));
			enemy.ReceiveDamage(actualDamage);
			
			Debug.Log("You deal " + actualDamage + " to enemy " + enemy.name);
		}
		else
			Attack(enemy);
	}

	public void SetWeapon (Weapon weapon)
	{
		this.currentWeapon = weapon;
	}

	public void SetShield (Shield shield)
	{
		this.currentShield = shield;
	}

	public void ApplyItem (Item item)
	{
		this.currentLife	= Mathf.Clamp(this.currentLife + item.lifeUnits, 0, this.maxLife);
		this.currentMana	= Mathf.Clamp(this.currentMana + item.manaUnits, 0, this.maxMana);
		this.damage			= Mathf.Max(this.damage + item.damageUnits, 0);
		this.defense		= Mathf.Max(this.defense + item.defenseUnits, 0);
		AddExperience(item.expUnits);
		this.gold			= Mathf.Max(this.gold + item.goldUnits, 0); // Debt is not supported for the moment
	}

	public void ApplyTrap (Trap trap)
	{
		this.currentLife	= Mathf.Clamp(this.currentLife + trap.lifeDamage, 0, this.maxLife);
		this.currentMana	= Mathf.Clamp(this.currentMana + trap.manaDamage, 0, this.maxMana);
	}
	
	public bool IsDead()
	{
		return this.currentLife <= 0;
	}
}
