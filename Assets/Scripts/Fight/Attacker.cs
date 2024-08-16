using System;
using System.Collections;
using UnityEngine;

public enum AttackState
{
	Preparation,
	Attack
}

public class Attacker : MonoBehaviour
{
	[SerializeField] private WarriorHealth _enemyHealth;
	[SerializeField] private FightStarter _fightStarter;
	[SerializeField] private FightStateVisual _stateVisual;
	[SerializeField] private WeaponSO _weapon;
	[SerializeField] private CharacterSO _character;

	private float _currentCooldown;
	private float _currentAttackSpeed;
	private float _changeWeaponCooldown = 2f;

	private AttackState _currentState = AttackState.Preparation;
	private bool _canAttack;
	private bool _weaponIsSwitching = false;

	public float Cooldown => _character.Cooldown;
	public float CurrentCooldown => _currentCooldown;
	public float CurrentAttackSpeed => _currentAttackSpeed;
	public WeaponSO Weapon => _weapon;
	public FightStateVisual FightStateVisual => _stateVisual;
	public CharacterSO Character => _character;

	public event Action WeaponChanged;
	public event Action<float> AttackStarted;
	public event Action AttackEnded;

	private void OnEnable()
	{
		if (_character.IsPlayer)
			_fightStarter.FightStateChanged += ChangeCanAttack;
	}

	private void OnDisable()
	{
		if (_character.IsPlayer)
			_fightStarter.FightStateChanged -= ChangeCanAttack;
	}

	private void Start()
	{
		if (_character.IsPlayer)
		{
			_canAttack = false;
			ResetAttack();
		}
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

	public void Initialize(WarriorHealth enemyHealth, FightStarter starter)
	{
		_enemyHealth = enemyHealth;
		if (!_character.IsPlayer)
		{
			_fightStarter = starter;
			_canAttack = true;
			ResetAttack();
			_stateVisual.Inititialize(starter);
		}
	}

	public void Attack()
	{
		AttackEnded?.Invoke();
		_currentState = AttackState.Preparation;
		if (!_weaponIsSwitching)
		{
			_currentCooldown += Time.deltaTime;

			if (_currentCooldown >= 1 / _character.Cooldown)
			{
				AttackStarted?.Invoke(1 / _weapon.AttackSpeed);
				_currentState = AttackState.Attack;
				_currentAttackSpeed += Time.deltaTime;
				if (_currentAttackSpeed >= 1 / _weapon.AttackSpeed)
				{
					float crit = UnityEngine.Random.Range(0, 101);
					if (crit <= _character.Luck)
						_enemyHealth.TakeDamage((_character.AttackStrength + _weapon.AttackStrength) * 2);
					else
						_enemyHealth.TakeDamage(_character.AttackStrength + _weapon.AttackStrength);

					ResetAttack();
				}
			}
		}
	}

	private IEnumerator WaitBeforeSwitchWeapon(float seconds, WeaponSO weapon)
	{
		_weaponIsSwitching = true;
		yield return new WaitForSeconds(seconds);
		_weapon = weapon;
		_weaponIsSwitching = false;
		WeaponChanged?.Invoke();
	}

	private IEnumerator WaitBeforeAttackEnded(float seconds, WeaponSO weapon)
	{
		yield return new WaitForSeconds(seconds);
		_weaponIsSwitching = true;
		yield return new WaitForSeconds(_changeWeaponCooldown);
		_weapon = weapon;
		_weaponIsSwitching = false;
		WeaponChanged?.Invoke();
	}

	public void SetWeapon(WeaponSO weapon)
	{
		if (!_canAttack)
		{
			_weapon = weapon;
			WeaponChanged?.Invoke();
		}
		else
		{
			if (_currentState == AttackState.Preparation)
			{
				StartCoroutine(WaitBeforeSwitchWeapon(_changeWeaponCooldown, weapon));
			}
			else
			{
				StartCoroutine(WaitBeforeAttackEnded(1 / _weapon.AttackSpeed - _currentAttackSpeed, weapon));
			}
		}
	}
}
