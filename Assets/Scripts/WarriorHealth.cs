using System;
using UnityEngine;

public class WarriorHealth : MonoBehaviour
{
	[SerializeField] private int _maxHealth = 10;
	[SerializeField] private int _armor = 0;

	private int _currentHealth;

	public int MaxHealth => _maxHealth;
	public int CurrentHealth => _currentHealth;

	public event Action HealthChanged;

	private void Awake()
	{
		_currentHealth = _maxHealth;
	}

	private void Die()
	{
		print(name + " Die");
	}

	public void TakeDamage(int damage)
	{
		if (damage < 0) damage = 0;

		if (damage <= _armor)
			damage = 0;
		else
			damage -= _armor;

		_currentHealth -= damage;
		if (_currentHealth <= 0)
		{
			_currentHealth = 0;
			Die();
		}
		HealthChanged?.Invoke();
	}
}
