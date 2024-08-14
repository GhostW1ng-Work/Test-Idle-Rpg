using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class FightStateVisual : MonoBehaviour
{
	[SerializeField] private Attacker _attacker;
	[SerializeField] private FightStarter _starter;

	private Slider _timer;
	private bool _canAttack = false;

	private void Awake()
	{
		_timer = GetComponent<Slider>();
	}

	private void OnEnable()
	{
		_starter.FightStateChanged += OnFightStateChanged;
	}

	private void OnDisable()
	{
		_starter.FightStateChanged -= OnFightStateChanged;
	}

	private void Start()
	{
		_timer.value = 0;
	}

	private void Update()
	{
		if(_canAttack)
		{
			Attack();
		}
	}

	private void OnFightStateChanged()
	{
		_canAttack = !_canAttack;
		if(!_canAttack)
		{
			_timer.value = 0;
		}
	}

	public void Attack()
	{
		_timer.maxValue = 1 / _attacker.Cooldown;
		_timer.value = Mathf.Lerp(_timer.value, _attacker.CurrentCooldown, 1f);

		if (_attacker.CurrentCooldown >= 1 / _attacker.Cooldown)
		{
			_timer.maxValue = 1 / _attacker.Weapon.AttackSpeed;
			_timer.value = Mathf.Lerp(_timer.value, _attacker.CurrentAttackSpeed, 1f);
		}
	}
}
