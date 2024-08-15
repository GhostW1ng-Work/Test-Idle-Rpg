using UnityEngine;

public class MapButton : BaseButton
{
	[SerializeField] private CanvasGroup _mainMap;

	private bool _isActive = true;

	protected override void OnClick()
	{
		SwitchMapActivity();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		LocationButton.LocationChanged += SwitchMapActivity;

	}

	protected override void OnDisable()
	{
		base.OnDisable();
		LocationButton.LocationChanged -= SwitchMapActivity;
	}

	private void Start()
	{
		SwitchMapActivity();
	}

	public void SwitchMapActivity()
	{
		_isActive = !_isActive;
		if (_isActive)
			_mainMap.alpha = 1;
        else
			_mainMap.alpha = 0;

		_mainMap.interactable = _isActive;
		_mainMap.blocksRaycasts = _isActive;
	}
}
