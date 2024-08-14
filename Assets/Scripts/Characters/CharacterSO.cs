using UnityEngine;

public abstract class CharacterSO : ScriptableObject
{
	[SerializeField] private int _maxHealth = 10;
	[SerializeField] private int _armor;
	[SerializeField] private int _attackStrength;
	[SerializeField] private float _cooldown;

	public int MaxHealth => _maxHealth;
	public int Armor => _armor;
	public int AttackStrength => _attackStrength;
	public float Cooldown => _cooldown;
}
