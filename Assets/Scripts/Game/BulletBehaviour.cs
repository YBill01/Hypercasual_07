using GameName.Data;
using GameName.Utilities;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour, IUpdatable
{
	[SerializeField]
	private BulletConfigData m_config;

	[Space]
	[SerializeField]
	private Transform m_body;

	[SerializeField]
	private Transform m_center;

	[Space]
	[SerializeField]
	private LayerMask m_interactLayerMask;

	private float _energy;
	public float Energy => _energy;

	private bool _isMove;

	private bool _isDispose;
	public bool IsDispose => _isDispose;

	private bool _isExplode;
	public bool IsExplode => _isExplode;

	public Vector3 ExplodePosition => m_center.position;

	public void Init()
	{
		_isMove = false;
		_isDispose = false;
		_isExplode = false;

		SetEnergy(0.0f);
	}

	public void AddEnergy(float energy)
	{
		_energy += energy;

		SetEnergy(_energy);
	}
	public void SetEnergy(float energy)
	{
		_energy = energy;

		m_body.localScale = Vector3.one * (_energy / RuntimeConstants.ENERGY_TO_SCALE);
	}

	public void Shoot()
	{
		_isMove = true;
	}

	public void OnUpdate(float deltaTime)
	{
		if (_isMove)
		{
			transform.position += Vector3.forward * m_config.speed * deltaTime;
		}
	}

	private void OnTriggerEnter(Collider collision)
	{
		if (IsInteract(collision))
		{
			if (collision.gameObject.layer == LayerMask.NameToLayer("Target"))
			{
				_isDispose = true;
			}
			else
			{
				_isExplode = true;
			}
		}
	}
	
	private bool IsInteract(Collider collision)
	{
		return (m_interactLayerMask & (1 << collision.gameObject.layer)) != 0;
	}
}