using UnityEngine;

[RequireComponent(typeof(Animator))]
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
		_enemyAttacker.AttackEnded += OnAttackEnded;
	}

	private void OnDisable()
	{
		_enemyAttacker.AttackStarted -= OnAttack;
		_enemyAttacker.AttackEnded -= OnAttackEnded;
	}

	private void OnAttack(float s)
	{
		_animator.SetBool(IS_ATTACK, true);
	}

	private void OnAttackEnded()
	{
		_animator.SetBool(IS_ATTACK, false);
	}


}
