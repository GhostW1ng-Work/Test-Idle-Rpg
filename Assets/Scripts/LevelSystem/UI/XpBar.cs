using UnityEngine;
using UnityEngine.UI;

public class XpBar : MonoBehaviour
{
	[SerializeField] private WarriorHealth _warriorHealth;
	[SerializeField] private float _speed = 0.1f;

	private Slider _slider;
	private PlayerSO _player;

	private void Awake()
	{
		_player = _warriorHealth.Character as PlayerSO;
		_slider = GetComponent<Slider>();
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
		_slider.maxValue = _player.NeedXpForNextLevel;
		_slider.value = _player.CurrentXp;
	}

	private void Update()
	{
		_slider.value = Mathf.Lerp(_slider.value, _player.CurrentXp, _speed);
	}

	private void OnLevelIncreased()
	{
		_slider.maxValue = _player.NeedXpForNextLevel;
	}
}
