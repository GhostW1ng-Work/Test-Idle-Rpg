using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private Attacker[] _enemies;
	[SerializeField] private Attacker _playerAttacker;
	[SerializeField] private WarriorHealth _player;
	[SerializeField] private FightStarter _fightStarter;
	[SerializeField] private Transform _spawnPoint;

	private bool _fightStarted = false;

	private void OnEnable()
	{
		_fightStarter.FightStateChanged += OnFightStateChanged;
	}

	private void OnDisable()
	{
		_fightStarter.FightStateChanged -= OnFightStateChanged;
	}

	private void OnFightStateChanged()
	{
		_fightStarted = !_fightStarted;
		if (_fightStarted)
		{
			int index = Random.Range(0, _enemies.Length);
			Attacker newAttacker = Instantiate(_enemies[index], _spawnPoint.position, Quaternion.Euler(0,180,0));
			newAttacker.Initialize(_player, _fightStarter);

			WarriorHealth newEnemy = newAttacker.GetComponent<WarriorHealth>();
			_playerAttacker.Initialize(newEnemy, _fightStarter);
		}
	}

}
