using Spine;
using Spine.Unity;
using System.Collections;
using UnityEngine;

public enum AnimationsName
{
	Idle,
	Attack,
	Dead
}

public class PlayerAnimatorController : MonoBehaviour
{
	[SerializeField] private WarriorHealth _playerHealth;
	[SerializeField] private Attacker _playerAttacker;

	private SkeletonAnimation _skeletonAnimation;

	private void Awake()
	{
		_skeletonAnimation = GetComponent<SkeletonAnimation>();
	}

	private void OnEnable()
	{
		_playerHealth.PlayerDied += OnPlayerDied;
		_playerAttacker.AttackStarted += OnAttack;
	}

	private void OnDisable()
	{
		_playerHealth.PlayerDied -= OnPlayerDied;
		_playerAttacker.AttackStarted -= OnAttack;
	}

	private void Start()
	{
		_skeletonAnimation.AnimationName = nameof(AnimationsName.Idle);


	}

	private void OnPlayerDied()
	{
		StartCoroutine(Died());
	}

	private IEnumerator Died()
	{
		_skeletonAnimation.AnimationName = nameof(AnimationsName.Dead);
		_skeletonAnimation.loop = false;
		TrackEntry track = _skeletonAnimation.state.GetCurrent(0);
		yield return new WaitForSeconds(track.Animation.Duration);
		_skeletonAnimation.loop = true;
		_skeletonAnimation.AnimationName = nameof(AnimationsName.Idle);
	}

	private void OnAttack(float duration)
	{
		StartCoroutine(Attack(duration));
	}

	private IEnumerator Attack(float duration)
	{
		_skeletonAnimation.AnimationName = nameof(AnimationsName.Attack);
		_skeletonAnimation.loop = false;
		TrackEntry track = _skeletonAnimation.state.GetCurrent(0);
		track.Animation.Duration = duration;
		yield return new WaitForSeconds(track.Animation.Duration);
		_skeletonAnimation.loop = true;
		_skeletonAnimation.AnimationName = nameof(AnimationsName.Idle);
	}
}
