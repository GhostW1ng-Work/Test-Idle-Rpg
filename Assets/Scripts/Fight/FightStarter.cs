using System;
using UnityEngine;
using UnityEngine.UI;

public class FightStarter : MonoBehaviour
{
	private Button _button;
	private bool _fightStarted = false;

	public event Action FightStateChanged;

	private void Awake()
	{
		_button = GetComponent<Button>();
	}

	private void OnEnable()
	{
		_button.onClick.AddListener(ChangeFightState);
	}

	private void OnDisable()
	{
		_button.onClick.RemoveListener(ChangeFightState);
	}

	private void ChangeFightState()
	{
		_fightStarted = !_fightStarted;
		FightStateChanged?.Invoke();
	}
}
