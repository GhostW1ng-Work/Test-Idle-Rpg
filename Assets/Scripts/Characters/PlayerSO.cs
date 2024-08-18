
using System;
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
	public int CurrentXp => _currentXp;

	public event Action XpChanged;
	public event Action LevelIncreased;

	public void GetXP(int xp)
	{
		_currentXp += xp;
		if(_currentXp >= _needXpForNextLevel)
		{
			_currentXp = 0;
			_currentLevel++;
			_needXpForNextLevel += _xpIncreaseAmount;
			IncreaseMaxHealth(false);
			IncreaseUpgradePoints();
			LevelIncreased?.Invoke();
		}
		XpChanged?.Invoke();
	}
}
