using UnityEngine;
using UnityEngine.UI;

public class Attacker : MonoBehaviour
{
	[SerializeField] private WarriorHealth _enemyHealth;
	[SerializeField] private FightStarter _fightStarter;
	[SerializeField] private FightStateVisual _stateVisual;
	[SerializeField] private WeaponSO _weapon;
	[SerializeField] private CharacterSO _character;

	private float _currentCooldown;
	private float _currentAttackSpeed;

	private bool _canAttack;

	public float Cooldown => _character.Cooldown;
	public float CurrentCooldown => _currentCooldown;
	public float CurrentAttackSpeed => _currentAttackSpeed;
	public WeaponSO Weapon => _weapon;
	public FightStateVisual FightStateVisual => _stateVisual;
	public CharacterSO Character => _character;

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
