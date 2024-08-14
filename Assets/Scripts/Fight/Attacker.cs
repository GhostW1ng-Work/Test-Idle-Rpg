using UnityEngine;
using UnityEngine.UI;

public class Attacker : MonoBehaviour
{
	[SerializeField] private WarriorHealth _enemyHealth;
	[SerializeField] private FightStarter _fightStarter;
	[SerializeField] private WeaponSO _weapon;
	[SerializeField] private CharacterSO _character;

	private float _currentCooldown;
	private float _currentAttackSpeed;

	private bool _canAttack;

	public float Cooldown => _character.Cooldown;
	public float CurrentCooldown => _currentCooldown;
	public float CurrentAttackSpeed => _currentAttackSpeed;
	public WeaponSO Weapon => _weapon;

	private void OnEnable()
	{
		_fightStarter.FightStateChanged += ChangeCanAttack;
	}

	private void OnDisable()
	{
		_fightStarter.FightStateChanged -= ChangeCanAttack;
	}

	private void Start()
	{
		_canAttack = false;
		ResetAttack();
	}

	private void Update()
	{
		if (_canAttack)
		{
			Attack();
		}
	}

	private void ResetAttack()
	{
		_currentCooldown = 0;
		_currentAttackSpeed = 0;

	}

	private void ChangeCanAttack()
	{
		_canAttack = !_canAttack;
		if (!_canAttack)
		{
			ResetAttack();
		}
	}

	public void Attack()
	{
		_currentCooldown += Time.deltaTime;

		if (_currentCooldown >= 1 / _character.Cooldown)
		{
			_currentAttackSpeed += Time.deltaTime;

			if (_currentAttackSpeed >= 1 / _weapon.AttackSpeed)
			{
				_enemyHealth.TakeDamage(_character.AttackStrength + _weapon.AttackStrength);
				ResetAttack();
			}
		}
	}
}
