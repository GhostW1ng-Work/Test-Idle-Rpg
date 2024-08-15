using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
	private const string IS_ATTACK = "IsAttack";

	[SerializeField] private Attacker _enemyAttacker;

	private Animator _animator;

	private void Start()
	{
		_animator = GetComponent<Animator>();
	}

	private void OnEnable()
	{
		_enemyAttacker.AttackStarted += OnAttack;
	}

	private void OnDisable()
	{
		_enemyAttacker.AttackStarted -= OnAttack;
	}

	private void OnAttack(float s)
	{
		_animator.SetTrigger(IS_ATTACK);
	}
}
