using UnityEngine;

public abstract class CharacterSO : ScriptableObject
{
	[SerializeField] private bool _isPlayer = false;
	[SerializeField] private int _maxHealth = 10;
	[SerializeField] private int _armor;
	[SerializeField] private int _attackStrength;
	[SerializeField] private float _luck = 5;
	[SerializeField] private float _cooldown;
	[SerializeField] private int _xp;
	[SerializeField] private Vector3 _spawnPosition;

	private int _healthIncreaseAmount = 5;
	private int _attackStrengthIncreaseAmount = 1;
	private float _luckIncreaseAmount = 1;
	private float _cooldownIncreaseAmount = 0.1f;

	public bool IsPlayer => _isPlayer;
	public int MaxHealth => _maxHealth;
	public int Armor => _armor;
	public int AttackStrength => _attackStrength;
	public float Luck => _luck;
	public float Cooldown => _cooldown;
	public int XP => _xp;
	public Vector3 SpawnPosition => _spawnPosition;

	protected void IncreaseStats()
	{
		_maxHealth += _healthIncreaseAmount;
		_attackStrength += _attackStrengthIncreaseAmount;
		_luck += _luckIncreaseAmount;
		_cooldown += _cooldownIncreaseAmount;
	}
}
