using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletsBehaviour : MonoBehaviour, IUpdatable
{
	public event Action OnBulletDispose;
	public event Action<Vector3, float> OnBulletExplode;

	[SerializeField]
	private BulletBehaviour m_bulletPrefab;
	[SerializeField]
	private BulletExplodeBehaviour m_bulletExplodePrefab;

	[Space]
	[SerializeField]
	private Transform m_objectsContainer;

	private GameObjectFactories _factories;

	private List<BulletBehaviour> _bullets;
	private List<BulletExplodeBehaviour> _bulletsExplode;

	private void Awake()
	{
		_factories = new GameObjectFactories(m_objectsContainer);
		_bullets = new List<BulletBehaviour>();
		_bulletsExplode = new List<BulletExplodeBehaviour>();
	}

	public BulletBehaviour CreateBullet(Vector3 position)
	{
		BulletBehaviour bullet = _factories.Instantiate(m_bulletPrefab.gameObject, position, m_bulletPrefab.transform.rotation, Vector3.one)
			.GetComponent<BulletBehaviour>();

		bullet.Init();

		_bullets.Add(bullet);

		return bullet;
	}
	public void RemoveBullet(BulletBehaviour bullet)
	{
		_factories.Dispose(m_bulletPrefab.gameObject, bullet.gameObject);
		_bullets.Remove(bullet);
	}

	public void CreateBulletExplode(Vector3 position, float energy)
	{
		BulletExplodeBehaviour bulletExplode = _factories.Instantiate(m_bulletExplodePrefab.gameObject, position, m_bulletExplodePrefab.transform.rotation, Vector3.one)
			.GetComponent<BulletExplodeBehaviour>();

		bulletExplode.Init();
		bulletExplode.SetEnergy(energy);

		_bulletsExplode.Add(bulletExplode);
	}
	public void RemoveBulletExplode(BulletExplodeBehaviour bulletExplode)
	{
		_factories.Dispose(m_bulletExplodePrefab.gameObject, bulletExplode.gameObject);
		_bulletsExplode.Remove(bulletExplode);
	}

	public void ClearAll()
	{
		for (int i = _bullets.Count - 1; i >= 0; i--)
		{
			RemoveBullet(_bullets[i]);
		}

		for (int i = _bulletsExplode.Count - 1; i >= 0; i--)
		{
			RemoveBulletExplode(_bulletsExplode[i]);
		}

		OnBulletDispose = null;
		OnBulletExplode = null;
	}

	public void OnUpdate(float deltaTime)
	{
		for (int i = _bullets.Count - 1; i >= 0; i--)
		{
			_bullets[i].OnUpdate(deltaTime);

			if (_bullets[i].IsDispose)
			{
				RemoveBullet(_bullets[i]);

				OnBulletDispose?.Invoke();
			}
			else if (_bullets[i].IsExplode)
			{
				CreateBulletExplode(_bullets[i].ExplodePosition, _bullets[i].Energy);

				RemoveBullet(_bullets[i]);
			}
		}

		for (int i = _bulletsExplode.Count - 1; i >= 0; i--)
		{
			_bulletsExplode[i].OnUpdate(deltaTime);

			if (_bulletsExplode[i].IsDispose)
			{
				RemoveBulletExplode(_bulletsExplode[i]);
			}
			else if (_bulletsExplode[i].IsExplode)
			{
				_bulletsExplode[i].Explode();

				OnBulletExplode?.Invoke(_bulletsExplode[i].ExplodePosition, _bulletsExplode[i].ExplodeRadius);
			}
		}
	}
}