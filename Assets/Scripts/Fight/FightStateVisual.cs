using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class FightStateVisual : MonoBehaviour
{
	[SerializeField] private CanvasGroup _actionCanvas;
	[SerializeField] private Image _timerImage;
	[SerializeField] private Image _actionImage;

	[SerializeField] private Sprite _reloadSprite;

	[SerializeField] private Attacker _attacker;
	[SerializeField] private FightStarter _starter;

	private Slider _timer;
	private bool _canAttack = false;

	private void Awake()
	{
		_timer = GetComponent<Slider>();
	}

	private void OnEnable()
	{
		if(_starter != null)
			_starter.FightStateChanged += OnFightStateChanged;
		if(_attacker.Character.IsPlayer)
			_attacker.WeaponChanged += OnWeaponChanged;
	}

	private void OnDisable()
	{
		_starter.FightStateChanged -= OnFightStateChanged;
		if (_attacker.Character.IsPlayer)
			_attacker.WeaponChanged -= OnWeaponChanged;
	}

	private void Start()
	{
		if (_attacker.Character.IsPlayer)
		{
			_actionCanvas.alpha = 0;
			_timer.value = 0;
		}	
	}

	private void Update()
	{
		if(_canAttack)
		{
			Attack();
		}
	}

	private void OnWeaponChanged()
	{
		_actionImage.sprite = _attacker.Weapon.WeaponSprite;
	}

	public void OnFightStateChanged()
	{
		_canAttack = !_canAttack;
		if(!_canAttack)
		{
			_actionCanvas.alpha = 0;
			_timer.value = 0;
		}
		else
		{
			_actionCanvas.alpha = 1;
		}
	}

	public void Inititialize(FightStarter starter)
	{
		_starter = starter;
		OnFightStateChanged();
	}

	public void Attack()
	{
		_actionImage.sprite = _reloadSprite;
		_timerImage.color = Color.yellow;

		_timer.maxValue = 1 / _attacker.Cooldown;
		_timer.value = Mathf.Lerp(_timer.value, _attacker.CurrentCooldown, 1f);

		if (_attacker.CurrentCooldown >= 1 / _attacker.Cooldown)
		{
			_actionImage.sprite = _attacker.Weapon.WeaponSprite;
			_timerImage.color = Color.gray;

			_timer.maxValue = 1 / _attacker.Weapon.AttackSpeed;
			_timer.value = Mathf.Lerp(_timer.value, _attacker.CurrentAttackSpeed, 1f);
		}
	}
}
