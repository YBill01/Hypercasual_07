using GameName.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesBehaviour : MonoBehaviour, IUpdatable
{
	public event Action OnDamage;

	private const int NUM_PLACED_TRIES = 8;

	private LevelConfigData _levelConfig;

	[Space]
	[SerializeField]
	private Bounds m_bounds;

	[Space]
	[SerializeField]
	private Transform m_objectsContainer;
	
	[Space]
	[SerializeField]
	private float m_obstaclesFrequency = 0.5f;
	[SerializeField]
	private float m_obstaclesFrequencyScale = 1.0f;

	[Space]
	[SerializeField]
	private LayerMask m_interactLayerMask;

	public bool IsDamage => _damageObstacles.Count > 0;

	private GameObjectFactories _factories;

	private List<ObstacleBehaviour> _obstacles;
	private List<ObstacleBehaviour> _damageObstacles;

	private void Awake()
	{
		_factories = new GameObjectFactories(m_objectsContainer);
		_obstacles = new List<ObstacleBehaviour>();
		_damageObstacles = new List<ObstacleBehaviour>();
	}

	public void Generate(LevelConfigData config)
	{
		_levelConfig = config;

		Vector3 newPosition = Vector3.zero;

		for (int i = 0; i < _levelConfig.numObstacles; i++)
		{
			if (GetObstaclePosition(out newPosition))
			{
				CreateObstacle(newPosition);
			}
		}
	}
	public void ClearAll()
	{
		for (int i = _obstacles.Count - 1; i >= 0; i--)
		{
			RemoveObstacle(_obstacles[i]);
		}

		_damageObstacles.Clear();

		OnDamage = null;
	}

	public ObstacleBehaviour CreateObstacle(Vector3 position)
	{
		ObstacleBehaviour obstacle = _factories.Instantiate(_levelConfig.obstaclePrefab, position, _levelConfig.obstaclePrefab.transform.rotation, Vector3.one)
			.GetComponent<ObstacleBehaviour>();

		obstacle.Init();

		_obstacles.Add(obstacle);

		return obstacle;
	}
	public void RemoveObstacle(ObstacleBehaviour obstacle)
	{
		_factories.Dispose(_levelConfig.obstaclePrefab, obstacle.gameObject);
		_obstacles.Remove(obstacle);
	}

	public void DamageObstacleSphere(Vector3 center, float radius)
	{
		Collider[] colliders = Physics.OverlapSphere(center, radius, m_interactLayerMask);

		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].TryGetComponent(out ObstacleBehaviour obstacle))
			{
				obstacle.Damage();
				_damageObstacles.Add(obstacle);
			}
		}
	}

	public void OnUpdate(float deltaTime)
	{
		if (IsDamage)
		{
			for (int i = _damageObstacles.Count - 1; i >= 0; i--)
			{
				_damageObstacles[i].OnUpdate(deltaTime);

				if (_damageObstacles[i].IsDispose)
				{
					RemoveObstacle(_damageObstacles[i]);
					_damageObstacles.RemoveAt(i);
				}
			}

			if (!IsDamage)
			{
				OnDamage?.Invoke();
			}
		}
	}

	private bool GetObstaclePosition(out Vector3 position)
	{
		float x;
		float z;

		int triesCount = 0;

		bool nope;

		do
		{
			x = UnityEngine.Random.value * m_obstaclesFrequencyScale;
			z = UnityEngine.Random.value * m_obstaclesFrequencyScale;

			nope = Mathf.PerlinNoise(x % m_obstaclesFrequencyScale, z % m_obstaclesFrequencyScale) < m_obstaclesFrequency;

			triesCount++;
		}
		while (nope && triesCount < NUM_PLACED_TRIES);

		position = new Vector3(m_bounds.min.x + (m_bounds.size.x * (x / m_obstaclesFrequencyScale)), 0.0f, m_bounds.min.z + (m_bounds.size.z * (z / m_obstaclesFrequencyScale)));

		return true;
	}

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(m_bounds.center, m_bounds.size);
	}
#endif
}