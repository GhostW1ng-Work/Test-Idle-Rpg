using System;
using UnityEngine;

public class WarriorHealth : MonoBehaviour
{
	[SerializeField] private CharacterSO _character;

	private int _currentHealth;

	public CharacterSO Character => _character;
	public int MaxHealth => _character.MaxHealth;
	public int CurrentHealth => _currentHealth;

	public event Action HealthChanged;
	public event Action PlayerDied;
	public event Action EnemyDied;

	private void Awake()
	{
		_currentHealth = _character.MaxHealth;
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
