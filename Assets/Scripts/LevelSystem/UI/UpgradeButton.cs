using UnityEngine;
using UnityEngine.UI;

public enum Stat
{
	MaxHealth,
	AttackStrength,
	Cooldown,
	Luck
}

public class UpgradeButton : MonoBehaviour
{
	[SerializeField] private WarriorHealth _player;
	[SerializeField] private Stat _stat;

	private CanvasGroup _canvasGroup;
	private Button _button;

	private void Awake()
	{
		_button = GetComponent<Button>();
		_canvasGroup = GetComponent<CanvasGroup>();
	}

	private void OnEnable()
	{
		_button.onClick.AddListener(OnClick);
		_player.Character.UpgradePointsChanged += OnUpgradePointsChanged;
	}

	private void OnDisable()
	{
		_button.onClick.RemoveListener(OnClick);
		_player.Character.UpgradePointsChanged -= OnUpgradePointsChanged;
	}

	private void Start()
	{
		if (_player.Character.UpgradePoints <= 0)
			SwitchButtonActivity(false);
		else
			SwitchButtonActivity(true);
	}

	private void OnClick()
	{
		if(_player.Character.UpgradePoints > 0)
		{
			switch (_stat)
			{
				case Stat.MaxHealth:
					_player.Character.IncreaseMaxHealth(true);
					break;
				case Stat.AttackStrength:
					_player.Character.IncreaseAttackStrength();
					break;
				case Stat.Cooldown:
					_player.Character.IncreaseCooldown();
					break;
				case Stat.Luck:
					_player.Character.IncreaseLuck();
					break;
			}
		}
	}

	private void OnUpgradePointsChanged()
	{
		if(_player.Character.UpgradePoints > 0)
			SwitchButtonActivity(true);
		else
			SwitchButtonActivity(false);
	}

	private void SwitchButtonActivity(bool isActive)
	{
		if (isActive)
			_canvasGroup.alpha = 1;
		else
			_canvasGroup.alpha = 0;

		_canvasGroup.interactable = isActive;
		_canvasGroup.blocksRaycasts = isActive;
	}
}
