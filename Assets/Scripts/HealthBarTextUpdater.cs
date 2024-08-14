using TMPro;
using UnityEngine;

public class HealthBarTextUpdater : MonoBehaviour
{
	[SerializeField] private WarriorHealth _warrior;

	private TMP_Text _healthText;

	private void Awake()
	{
		_healthText = GetComponent<TMP_Text>();
	}

	private void OnEnable()
	{
		_warrior.HealthChanged += OnHealthChanged;
	}

	private void OnDisable()
	{
		_warrior.HealthChanged -= OnHealthChanged;
	}

	private void Start()
	{
		_healthText.text = $"{_warrior.CurrentHealth} / {_warrior.MaxHealth}";
	}

	private void OnHealthChanged()
	{
		_healthText.text = $"{_warrior.CurrentHealth} / {_warrior.MaxHealth}";
	}
}
