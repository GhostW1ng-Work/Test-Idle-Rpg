using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
public class WeaponSO : ScriptableObject
{
	[SerializeField] private float _attackSpeed;
	[SerializeField] private int _attackStrength;
	[SerializeField] private Sprite _weaponSprite;

	public float AttackSpeed => _attackSpeed;
	public int AttackStrength => _attackStrength;
	public Sprite WeaponSprite => _weaponSprite;
}
