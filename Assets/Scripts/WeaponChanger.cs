using UnityEngine;
using UnityEngine.UI;

public enum Weapon
{
	Melee,
	Range
}

public class WeaponChanger : MonoBehaviour
{
	[SerializeField] private Attacker _playerAttacker;
	[SerializeField] private WeaponSO[] _weapons;
	[SerializeField] private Sprite _swordImage;
	[SerializeField] private Sprite _bowImage;

	private Image _image;
	private Button _button;

	private bool _isChanged = true;
	private bool _isMelee = true;

	private void Awake()
	{
		_image = GetComponent<Image>();
		_button = GetComponent<Button>();
	}

	private void OnEnable()
	{
		_button.onClick.AddListener(ChangeWeapon);
		_playerAttacker.WeaponChanged += OnWeaponChanged;
	}

	private void OnDisable()
	{
		_button.onClick.RemoveListener(ChangeWeapon);
		_playerAttacker.WeaponChanged -= OnWeaponChanged;
	}

	private void Start()
	{
		if (_isMelee)
		{
			_image.sprite = _swordImage;
		}
		else
		{
			_image.sprite = _bowImage;
		}
	}

	private void OnWeaponChanged()
	{
		_isChanged = true;
		if (_isMelee)
		{
			_image.sprite = _swordImage;
		}
		else
		{
			_image.sprite = _bowImage;
		}
	}

	private void ChangeWeapon()
	{
		if (_isChanged)
		{
			_isChanged = false;
			_isMelee = !_isMelee;

			if (_isMelee)
			{
				_playerAttacker.SetWeapon(_weapons[(int)Weapon.Melee]);
			}
			else
			{
				_playerAttacker.SetWeapon(_weapons[(int)Weapon.Range]);
			}
		}
	}
}
