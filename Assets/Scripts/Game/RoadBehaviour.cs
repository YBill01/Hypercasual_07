using GameName.Utilities;
using System;
using System.Linq;
using UnityEngine;
using VContainer;

[RequireComponent(typeof(BoxCollider))]
public class RoadBehaviour : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer _roadSpriteRenderer;

	[Space]
	[SerializeField]
	private LayerMask m_interactLayerMask;

	private BoxCollider _boxCollider;

	private PlayerBehaviour _player;

	[Inject]
	public void Construct(
		PlayerBehaviour player)
	{
		_player = player;
	}

	private void Awake()
	{
		_boxCollider = GetComponent<BoxCollider>();
	}

	private void OnEnable()
	{
		_player.OnEnergyChange += PlayerOnEnergyChange;
	}
	private void OnDisable()
	{
		_player.OnEnergyChange -= PlayerOnEnergyChange;
	}

	public void SetSize(float size)
	{
		size = Math.Max(size, 0.0f);

		_roadSpriteRenderer.size = new Vector2(_roadSpriteRenderer.size.x, size);
		_boxCollider.size = new Vector3(size, _boxCollider.size.y, _boxCollider.size.z);
	}

	private void PlayerOnEnergyChange()
	{
		SetSize(_player.Energy / RuntimeConstants.ENERGY_TO_SCALE);
	}

	public bool CheckObstacle(out Vector3 nearestObstaclePosition)
	{
		nearestObstaclePosition = Vector3.zero;

		Vector3 worldCenter = _boxCollider.transform.TransformPoint(_boxCollider.center);
		Vector3 worldHalfExtents = Vector3.Scale(_boxCollider.size, _boxCollider.transform.lossyScale) * 0.5f;

		Collider[] colliders = Physics.OverlapBox(worldCenter, worldHalfExtents, _boxCollider.transform.rotation, m_interactLayerMask);

		if (colliders.Length > 0)
		{
			nearestObstaclePosition = colliders.OrderBy(c => c.transform.position.z)
				.First()
				.transform.position;

			return true;
		}

		return false;
	}
}