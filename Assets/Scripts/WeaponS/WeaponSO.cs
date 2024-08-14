using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
public class WeaponSO : ScriptableObject
{
	[SerializeField] private float _attackSpeed;
	[SerializeField] private int _attackStrength;

	public float AttackSpeed => _attackSpeed;
	public int AttackStrength => _attackStrength;
}
