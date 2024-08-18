using UnityEngine;
using UnityEngine.UI;

public enum ButtonType
{
	Open,
	Close
}

public class PlayerStatsOpener : MonoBehaviour
{
	[SerializeField] private ButtonType _type;
	[SerializeField] private CanvasGroup _statsPanel;

	private Button _button;

	private void Awake()
	{
		_button = GetComponent<Button>();
	}

	private void OnEnable()
	{
		_button.onClick.AddListener(OnClick);
	}

	private void OnDisable()
	{
		_button.onClick.RemoveListener(OnClick);
	}

	private void Start()
	{
		SwitchPanelActivity(false);
	}

	private void OnClick()
	{
		if(_type == ButtonType.Open)
			SwitchPanelActivity(true);
		else
			SwitchPanelActivity(false);
	}

	private void SwitchPanelActivity(bool active)
	{
		if (active)
		{
			_statsPanel.alpha = 1;
		}
		else
		{
			_statsPanel.alpha = 0;
		}
		_statsPanel.blocksRaycasts = active;
		_statsPanel.interactable = active;
	}
}
