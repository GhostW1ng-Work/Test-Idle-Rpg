using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "New Enemy")]
public class EnemySO : CharacterSO
{
	[SerializeField] private int _spawnChance;

	public int SpawnChance => _spawnChance;
}
