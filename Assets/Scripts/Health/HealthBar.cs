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

	private void Start()
	{
		_slider.maxValue = _warriorHealth.MaxHealth;
		_slider.value = _warriorHealth.CurrentHealth;
	}

	private void Update()
	{
		_slider.value = Mathf.Lerp(_slider.value, _warriorHealth.CurrentHealth,_speed);
	}
}
