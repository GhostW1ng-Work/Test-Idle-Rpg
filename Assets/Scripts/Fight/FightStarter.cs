using System;
using UnityEngine;
using UnityEngine.UI;

public class FightStarter : MonoBehaviour
{
	[SerializeField] private Sprite _active;
	[SerializeField] private Sprite _notActive;

	private Image _image;

	private Button _button;
	private bool _fightStarted = false;

	public event Action FightStateChanged;

	private void Awake()
	{
		_image = GetComponent<Image>();
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

	private void Start()
	{
		_fightStarted = !_fightStarted;

		if (_fightStarted)
			_image.sprite = _active;
		else
			_image.sprite = _notActive;

	}

	private void ChangeFightState()
	{
		_fightStarted = !_fightStarted;

		if(_fightStarted )
			_image.sprite = _active;
		else
			_image.sprite = _notActive;

		FightStateChanged?.Invoke();
	}
}
