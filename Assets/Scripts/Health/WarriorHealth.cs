using System;
using UnityEngine;

public class WarriorHealth : MonoBehaviour
{
	[SerializeField] private CharacterSO _character;

	private int _currentHealth;
	private PlayerSO _playerSO;

	public CharacterSO Character => _character;
	public int MaxHealth => _character.MaxHealth;
	public int CurrentHealth => _currentHealth;

	public event Action HealthChanged;
	public event Action PlayerDied;
	public event Action EnemyDied;

	private void Awake()
	{
		if(_character is PlayerSO)
			_playerSO = (PlayerSO)_character;
		_currentHealth = _character.MaxHealth;
	}

	private void OnEnable()
	{
		if(_character is PlayerSO)
			_playerSO.LevelIncreased += OnLevelIncreased;

	}

	private void OnDisable()
	{
		if (_character is PlayerSO)
			_playerSO.LevelIncreased -= OnLevelIncreased;
	}

	private void OnLevelIncreased()
	{
		int healAmount = _character.MaxHealth - _currentHealth;
		Heal(healAmount);
	}

	private void Die()
	{
		if(_character.IsPlayer)
			PlayerDied?.Invoke();
		else
		{
			EnemyDied?.Invoke();
		}
	}

	public void Heal(int healAmount)
	{
		_currentHealth += healAmount;
		if (healAmount <= 0)
			_currentHealth += 0;

		if(_currentHealth >= MaxHealth)
			_currentHealth = MaxHealth;

		HealthChanged?.Invoke();
	}

	public void TakeDamage(int damage)
	{
		if (damage < 0) damage = 0;

		if (damage <= _character.Armor)
			damage = 0;
		else
			damage -= _character.Armor;

		_currentHealth -= damage;
		if (_currentHealth <= 0)
		{
			_currentHealth = 0;
			Die();
		}
		HealthChanged?.Invoke();
	}
}
