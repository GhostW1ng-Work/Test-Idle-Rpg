using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FightStarter : MonoBehaviour
{
	[SerializeField] private WarriorHealth _playerHealth;
	[SerializeField] private CanvasGroup _needHealingText;
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
		_playerHealth.PlayerDied += ChangeFightState;
		_button.onClick.AddListener(OnClick);
	}

	private void OnDisable()
	{
		_playerHealth.PlayerDied -= ChangeFightState;
		_button.onClick.RemoveListener(OnClick);
	}

	private void Start()
	{
		_needHealingText.alpha = 0;
		_image.sprite = _notActive;

	}

	private void OnClick()
	{
		if (!_fightStarted && _playerHealth.CurrentHealth > 0)
		{
			ChangeFightState();
		}
 		else if(!_fightStarted && _playerHealth.CurrentHealth <= 0)
		{
			StartCoroutine(ShowText());
		}
		else if (_fightStarted)
		{
			ChangeFightState();
		}
	}

	private void ChangeFightState()
	{
		_fightStarted = !_fightStarted;

		if (_fightStarted)
			_image.sprite = _active;
		else
		{
			_image.sprite = _notActive;
		}

		FightStateChanged?.Invoke();
	}

	private IEnumerator ShowText()
	{
		_needHealingText.alpha = 1;
		yield return new WaitForSeconds(1);
		_needHealingText.alpha = 0;
	}
}
