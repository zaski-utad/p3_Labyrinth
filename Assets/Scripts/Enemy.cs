using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy
{
	public Player player;

	public string name;
	public int level;
	public int damage;
	public int defense;
	public int maxLife;
	public int maxMana;
	public int rewardExperience;
	public int rewardGold;
	public float timeBetweenAttacks;
	public int chanceStrongAttack;

	public int currentLife;
	private int currentMana;
	private float counterTime;

	public Enemy (Player player, string name, int damage, int defense, int maxLife, int maxMana, int rewardExperience, int rewardGold, int timeBetweenAttacks, int chanceStrongAttack)
	{
		this.player				= player;
		this.name				= name;
		this.level				= 1;
		this.damage				= damage;
		this.defense			= defense;
		this.maxLife			= maxLife;
		this.maxMana			= maxMana;
		this.rewardExperience	= rewardExperience;
		this.rewardGold			= rewardGold;
		this.timeBetweenAttacks	= timeBetweenAttacks;
		this.chanceStrongAttack = Mathf.Clamp(chanceStrongAttack, 0, 100);

		this.currentLife		= maxLife;
		this.currentMana		= maxMana;
		this.counterTime		= 0;
	}

	public void Greet ()
	{
		Debug.Log("Enemy: " + this.name);
		Debug.Log("Enemy HP: " + this.currentLife);
		Debug.Log("Enemy damage: " + this.damage);
	}

	public void Attack()
	{
		int levelDifference = this.level - this.player.level;
		float dmgMultiplier = Mathf.Max(1, Mathf.Pow(Mathf.Abs(levelDifference), 1.5f));
		
		int damageDealt = (int) ((levelDifference > 0) ? Mathf.Ceil(1 + this.damage * dmgMultiplier) : Mathf.Max(1, Mathf.Ceil(this.damage / dmgMultiplier)));

		int actualDmg;
		
		if (Random.Range(0, 100) > this.chanceStrongAttack)
			actualDmg = damageDealt;
		else
			actualDmg = (int) Mathf.Ceil(damageDealt * 1.2f);
		
		this.player.ReceiveDamage(actualDmg);

		Debug.Log("Enemy deals: " + actualDmg + " damages");
	}

	public void ReceiveDamage (int damage)
	{
		int actualDamage = 1 + (int) Mathf.Floor(damage / (1 + Mathf.Log(this.defense)));
		this.currentLife -= actualDamage;
	}

	public bool IsDead ()
	{
		return this.currentLife <= 0;
	}

	public void UpdateEnemy (float timeDelta)
	{
		this.counterTime += timeDelta;

		if (this.counterTime > this.timeBetweenAttacks)
		{
			this.counterTime = 0;
			Attack();
		}
	}
}