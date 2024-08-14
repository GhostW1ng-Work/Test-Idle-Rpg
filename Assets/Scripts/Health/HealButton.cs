using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(CanvasGroup))]
public class HealButton : MonoBehaviour
{
	[SerializeField] private FightStarter _starter;
	[SerializeField] private WarriorHealth _playerHealth;

	private Button _button;
	private CanvasGroup _canvasGroup;
	private bool _fightStarted = false;

	private void Awake()
	{
		_canvasGroup = GetComponent<CanvasGroup>();
		_button = GetComponent<Button>();
	}

	private void OnEnable()
	{
		_starter.FightStateChanged += OnFightStateChanged;
		_button.onClick.AddListener(OnClick);
	}

	private void OnDisable()
	{
		_starter.FightStateChanged -= OnFightStateChanged;
		_button.onClick.RemoveListener(OnClick);
	}

	private void Start()
	{
		ChangeCanvasGroup(true);
	}

	private void ChangeCanvasGroup(bool state)
	{
		if (state)
			_canvasGroup.alpha = 1;
		else
			_canvasGroup.alpha = 0;
		_canvasGroup.interactable = state;
		_canvasGroup.blocksRaycasts = state;

	}

	private void OnFightStateChanged()
	{
		_fightStarted = !_fightStarted;
		ChangeCanvasGroup(!_fightStarted);
	}

	private void OnClick()
	{
		int healAmount = _playerHealth.MaxHealth - _playerHealth.CurrentHealth;
		_playerHealth.Heal(healAmount);
	}

}
