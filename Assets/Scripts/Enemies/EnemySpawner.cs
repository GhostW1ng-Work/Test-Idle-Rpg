using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private List<Attacker> _enemies;
	[SerializeField] private Attacker _playerAttacker;
	[SerializeField] private WarriorHealth _player;
	[SerializeField] private FightStarter _fightStarter;
	[SerializeField] private Transform _spawnPoint;

	private bool _fightStarted = false;
	private Attacker _currentAttacker;

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
			CreateEnemy();
		}
		else
		{
			DestroyEnemy();
		}
	}

	private void OnEnemyDied()
	{
		StartCoroutine(RespawnEnemy());
	}

	private IEnumerator RespawnEnemy()
	{
		DestroyEnemy();
		yield return new WaitForSeconds(0.1f);
		CreateEnemy();
	}

	private void DestroyEnemy()
	{
		if (!_currentAttacker.Character.IsPlayer)
			_currentAttacker.GetComponent<WarriorHealth>().EnemyDied -= OnEnemyDied;
		Destroy(_currentAttacker.gameObject);
		_currentAttacker = null;
	}

	private void CreateEnemy()
	{
		int spawnValue = Random.Range(0, 101);
		if(_enemies.Count < 2)
		{
			InstantiateEnemy(0);
			return;
		}

		for (int i = 0; i < _enemies.Count; i++)
		{
			if (_enemies[i].Character is EnemySO enemy)
			{
				if (spawnValue <= enemy.SpawnChance)
				{
					InstantiateEnemy(i);
					break;
				}
				else
				{
					spawnValue -= enemy.SpawnChance;
				}
			}
		}
	}

	private void InstantiateEnemy(int index)
	{
		Attacker newAttacker = Instantiate(_enemies[index], _spawnPoint.position, Quaternion.Euler(0, 180, 0));
		newAttacker.transform.position = newAttacker.Character.SpawnPosition;
		newAttacker.Initialize(_player, _fightStarter);
		_currentAttacker = newAttacker;
		if (!_currentAttacker.Character.IsPlayer)
			_currentAttacker.GetComponent<WarriorHealth>().EnemyDied += OnEnemyDied;
		WarriorHealth newEnemy = newAttacker.GetComponent<WarriorHealth>();
		_playerAttacker.Initialize(newEnemy, _fightStarter);
	}

	public void SetEnemies(List<Attacker> newEnemies)
	{
		_enemies = newEnemies;
	}
}
