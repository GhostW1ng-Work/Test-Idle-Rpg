using TMPro;
using UnityEngine;

public class UpgradePointsTextUpdater : MonoBehaviour
{
	[SerializeField] private WarriorHealth _playerHealth;

	private TMP_Text _text;

	private void Awake()
	{
		_text = GetComponent<TMP_Text>();
	}

	private void OnEnable()
	{
		_playerHealth.Character.UpgradePointsChanged += OnUpgradePointsIncreased;
	}

	private void OnDisable()
	{
		_playerHealth.Character.UpgradePointsChanged -= OnUpgradePointsIncreased;
	}

	private void Start()
	{
		OnUpgradePointsIncreased();
	}

	private void OnUpgradePointsIncreased()
	{
		_text.text = _playerHealth.Character.UpgradePoints.ToString();
	}
}
