
using UnityEngine;

[CreateAssetMenu(fileName ="Player",menuName ="New Player")]
public class PlayerSO : CharacterSO
{
	[SerializeField] private int _currentLevel = 1;
	[SerializeField] private int _xpIncreaseAmount = 5;
	[SerializeField] private int _needXpForNextLevel = 10;
	[SerializeField] private int _currentXp = 0;

	public int CurrentLevel =>_currentLevel;
	public int XPIncreaseAmount =>_xpIncreaseAmount;
	public int NeedXpForNextLevel => _needXpForNextLevel;

	public void GetXP(int xp)
	{
		_currentXp += xp;
		if(_currentXp >= _needXpForNextLevel)
		{
			_currentXp = 0;
			_currentLevel++;
			_needXpForNextLevel += _xpIncreaseAmount;
			IncreaseStats();
		}
	}

	private void Awake()
	{
		_currentLevel = 1;
		_xpIncreaseAmount = 5;
		_needXpForNextLevel = 10;
		_currentXp = 0;
	}
}
