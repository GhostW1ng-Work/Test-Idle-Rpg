using TMPro;
using UnityEngine;

public class XpTextUpdater : MonoBehaviour
{
	[SerializeField] private WarriorHealth _warrior;

	private TMP_Text _xpText;
	private PlayerSO _player;
	private void Awake()
	{
		_player = _warrior.Character as PlayerSO;
		_xpText = GetComponent<TMP_Text>();
	}

	private void OnEnable()
	{

		_player.XpChanged += OnXpChanged;
	}

	private void OnDisable()
	{
		_player.XpChanged -= OnXpChanged;
	}

	private void Start()
	{
		_xpText.text = $"{_player.CurrentXp} / {_player.NeedXpForNextLevel}";
	}

	private void OnXpChanged()
	{
		_xpText.text = $"{_player.CurrentXp} / {_player.NeedXpForNextLevel}";
	}
}
