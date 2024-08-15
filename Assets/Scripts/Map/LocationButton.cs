using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationButton : MonoBehaviour
{
	[SerializeField] private List<Attacker> _enemiesOnLocation;
	[SerializeField] private SpriteRenderer _background;
	[SerializeField] private Sprite _locationSprite;
	[SerializeField] private EnemySpawner _spawner;

	private Button _button;

	public static event Action LocationChanged;

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

	private void OnClick()
	{
		_background.sprite = _locationSprite;
		_spawner.SetEnemies(_enemiesOnLocation);
		LocationChanged?.Invoke();
	}
}
