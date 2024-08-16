using UnityEngine;

public class XpIncreaser : MonoBehaviour
{
	[SerializeField] private EnemySpawner _spawner;
	[SerializeField] private FightStarter _starter;
	[SerializeField] private WarriorHealth _enemy;

	private Attacker _playerAttacker;
	private bool _isFight = false;

	private void OnEnable()
	{
		_spawner.EnemyCreated += OnEnemyCreated;
		_starter.FightStateChanged += OnFightStateChanged;
	}

	private void OnDisable()
	{
		_spawner.EnemyCreated -= OnEnemyCreated;
	}

	private void Start()
	{
		_playerAttacker = GetComponent<Attacker>();
	}

	private void OnEnemyCreated(WarriorHealth enemy)
	{
		_enemy = enemy;
		_enemy.EnemyDied += OnEnemyDied;
	}

	private void OnFightStateChanged()
	{
		_isFight = !_isFight;
		if (!_isFight)
		{
			_enemy.EnemyDied -= OnEnemyDied;
			_enemy = null;
		}
	}

	private void OnEnemyDied()
	{
		if(_playerAttacker.Character is PlayerSO player)
		{
			player.GetXP(_enemy.Character.XP);
			_enemy.EnemyDied -= OnEnemyDied;
			_enemy = null;
		}
	}
}
