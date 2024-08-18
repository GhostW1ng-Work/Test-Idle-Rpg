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
	[SerializeField] private Attacker _player;
	[SerializeField] private Stats _stat;

	private TMP_Text _text;

	private void Awake()
	{
		_text = GetComponent<TMP_Text>();
	}

	private void OnEnable()
	{
		_player.Character.StatIncreased += OnStatIncreased;
	}

	private void OnDisable()
	{
		_player.Character.StatIncreased -= OnStatIncreased;
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
				_text.text = _player.Character.MaxHealth.ToString();
				break;
			case Stats.AttackStrength:
				_text.text = (_player.Weapon.AttackStrength + _player.Character.AttackStrength).ToString();
				break;
			case Stats.Cooldown:
				_text.text = Math.Round(1 / _player.Character.Cooldown,3).ToString();
				break;
			case Stats.Armor:
				_text.text = _player.Character.Armor.ToString();
				break;
			case Stats.Luck:
				_text.text = _player.Character.Luck.ToString();
				break;
		}
	}
}
