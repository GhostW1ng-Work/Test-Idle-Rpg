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

	private SkeletonAnimation _skeletonAnimation;
	private float _timeForDeadAnimation = 0.5f;

	private void Awake()
	{
		_skeletonAnimation = GetComponent<SkeletonAnimation>();
	}

	private void OnEnable()
	{
		_playerHealth.PlayerDied += OnPlayerDied;
	}

	private void OnDisable()
	{
		_playerHealth.PlayerDied -= OnPlayerDied;
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

	private void OnAttack()
	{

	}
}
