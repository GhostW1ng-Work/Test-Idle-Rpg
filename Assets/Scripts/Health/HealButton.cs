using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(CanvasGroup))]
public class HealButton : BaseButton
{
	protected override void OnClick()
	{
		int healAmount = _playerHealth.MaxHealth - _playerHealth.CurrentHealth;
		_playerHealth.Heal(healAmount);
	}

}
