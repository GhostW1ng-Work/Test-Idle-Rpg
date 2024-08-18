using System;
using UnityEngine;

public abstract class CharacterSO : ScriptableObject
{
	[SerializeField] private bool _isPlayer = false;
	[SerializeField] private int _maxHealth = 10;
	[SerializeField] private int _armor;
	[SerializeField] private int _attackStrength;
	[SerializeField] private float _luck = 5;
	[SerializeField] private float _cooldown;
	[SerializeField] private int _xp;
	[SerializeField] private int _upgradePoints = 0;
	[SerializeField] private float _maxCooldown = 3;
	[SerializeField] private int _maxLuck = 99;
	[SerializeField] private Vector3 _spawnPosition;

	private int _healthIncreaseAmount = 5;
	private int _attackStrengthIncreaseAmount = 1;
	private float _luckIncreaseAmount = 1;
	private float _cooldownIncreaseAmount = 0.1f;



	public bool IsPlayer => _isPlayer;
	public int MaxHealth => _maxHealth;
	public int Armor => _armor;
	public int AttackStrength => _attackStrength;
	public float Luck => _luck;
	public float Cooldown => _cooldown;
	public int XP => _xp;
	public int UpgradePoints => _upgradePoints;

	public float MaxCooldown => _maxCooldown;
	public int MaxLuck => _maxLuck;
	public Vector3 SpawnPosition => _spawnPosition;

	public event Action StatIncreased;
	public event Action UpgradePointsChanged;

	public void IncreaseUpgradePoints()
	{
		_upgradePoints += 1;
		UpgradePointsChanged?.Invoke();
	}

	public void IncreaseMaxHealth(bool spendUpgradePoints)
	{
		if (spendUpgradePoints)
		{
			_upgradePoints -= 1;
			UpgradePointsChanged?.Invoke();
		}

		_maxHealth += _healthIncreaseAmount;
		StatIncreased?.Invoke();
	}

	public void IncreaseAttackStrength()
	{
		_upgradePoints -= 1;
		UpgradePointsChanged?.Invoke();
		_attackStrength += _attackStrengthIncreaseAmount;
		StatIncreased?.Invoke();
	}

	public void IncreaseLuck()
	{
		_upgradePoints -= 1;
		UpgradePointsChanged?.Invoke();
		_luck += _luckIncreaseAmount;
		StatIncreased?.Invoke();
	}

	public void IncreaseCooldown()
	{
		_upgradePoints -= 1;
		UpgradePointsChanged?.Invoke();
		_cooldown += _cooldownIncreaseAmount;
		StatIncreased?.Invoke();
	}
}
