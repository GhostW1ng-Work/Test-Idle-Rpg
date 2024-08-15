using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Attacker),typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
	[SerializeField] private AudioClip[] _attackSounds;

	private Attacker _attacker;
	private AudioSource _source;

	private void Awake()
	{
		_source = GetComponent<AudioSource>();
		_attacker = GetComponent<Attacker>();
	}

	private void OnEnable()
	{
		_attacker.AttackStarted += OnAttackStarted;
	}

	private void OnDisable()
	{
		_attacker.AttackStarted -= OnAttackStarted;
	}

	private void OnAttackStarted(float duration)
	{
		int index = Random.Range(0, _attackSounds.Length);
		_source.clip = _attackSounds[index];
		_source.Play();

	}

	private IEnumerator PlaySound(int index)
	{

		yield return new WaitForSeconds(_source.clip.length);
		_source.Stop();
	}
}
