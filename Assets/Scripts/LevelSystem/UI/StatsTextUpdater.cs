using System;
using TMPro;
using UnityEngine;

public enum Stats
{
	MaxHealth,
	AttackStrength,
	Cooldown,
	Armor,
	Luck
}

public class StatsTextUpdater : MonoBehaviour
{
	[SerializeField] private WarriorHealth _playerHealth;
	[SerializeField] private Stats _stat;

	private TMP_Text _text;

	private void Awake()
	{
		_text = GetComponent<TMP_Text>();
	}

	private void OnEnable()
	{
		_playerHealth.Character.StatIncreased += OnStatIncreased;
	}

	private void OnDisable()
	{
		_playerHealth.Character.StatIncreased -= OnStatIncreased;
	}

	private void Start()
	{
		OnStatIncreased();
	}

	private void OnStatIncreased()
	{
		switch (_stat)
		{
			case Stats.MaxHealth:
				_text.text = _playerHealth.Character.MaxHealth.ToString();
				break;
			case Stats.AttackStrength:
				_text.text = _playerHealth.Character.AttackStrength.ToString();
				break;
			case Stats.Cooldown:
				_text.text = Math.Round(1 / _playerHealth.Character.Cooldown,3).ToString();
				break;
			case Stats.Armor:
				_text.text = _playerHealth.Character.Armor.ToString();
				break;
			case Stats.Luck:
				_text.text = _playerHealth.Character.Luck.ToString();
				break;
		}
	}
}
