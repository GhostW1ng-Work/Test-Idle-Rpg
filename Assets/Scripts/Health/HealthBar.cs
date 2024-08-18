using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	[SerializeField] private WarriorHealth _warriorHealth;
	[SerializeField] private float _speed = 0.1f;

	private Slider _slider;

	private void Awake()
	{
		_slider = GetComponent<Slider>();
	}

	private void OnEnable()
	{
		if (_warriorHealth.Character.IsPlayer)
			_warriorHealth.Character.StatIncreased += OnStatIncreased;
	}

	private void OnDisable()
	{
		if(_warriorHealth.Character.IsPlayer)
			_warriorHealth.Character.StatIncreased -= OnStatIncreased;
	}

	private void Start()
	{
		_slider.maxValue = _warriorHealth.MaxHealth;
		_slider.value = _warriorHealth.CurrentHealth;
	}

	private void Update()
	{
		_slider.value = Mathf.Lerp(_slider.value, _warriorHealth.CurrentHealth, _speed);
	}

	private void OnStatIncreased()
	{
		_slider.maxValue = _warriorHealth.Character.MaxHealth;
	}
}
