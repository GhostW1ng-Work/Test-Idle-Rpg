using TMPro;
using UnityEngine;

public class LevelTextUpdater : MonoBehaviour
{
	[SerializeField] private WarriorHealth _warrior;

	private TMP_Text _levelText;
	private PlayerSO _player;

	private void Awake()
	{
		_player = _warrior.Character as PlayerSO;
		_levelText = GetComponent<TMP_Text>();
	}

	private void OnEnable()
	{
		_player.LevelIncreased += OnLevelIncreased;
	}

	private void OnDisable()
	{
		_player.LevelIncreased -= OnLevelIncreased;
	}

	private void Start()
	{
		_levelText.text = $"{_player.CurrentLevel}";
	}

	private void OnLevelIncreased()
	{
		_levelText.text = $"{_player.CurrentLevel}";
	}
}
