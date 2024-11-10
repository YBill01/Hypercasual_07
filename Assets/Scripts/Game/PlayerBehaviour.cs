using GameName.Data;
using GameName.Utilities;
using System;
using UnityEngine;
using VContainer;

[RequireComponent(typeof(CharacterController))]
public class PlayerBehaviour : MonoBehaviour, IUpdatable
{
	public event Action OnShoot;
	public event Action OnEnergyGone;
	public event Action OnMoveToComplete;
	public event Action OnEnergyChange;

	[SerializeField]
	private PlayerConfigData m_config;

	[field: SerializeField, Space]
	public VCameraTarget CameraTarget { get; private set; }

	[Space]
	[SerializeField]
	private Transform m_barrel;

	[SerializeField]
	private Transform m_body;

	private float _energy;
	public float Energy => _energy;
	public bool IsEnergyNotEnough => _energy < m_config.minEnergyChargeStart;

	private bool _isShooting;
	public bool IsShooting => _isShooting;

	private bool _isMoving;
	public bool IsMoving => _isMoving;

	private CharacterController _characterController;

	private Vector3 _barrelPosition;

	private BulletBehaviour _bullet;

	private Vector3 _moveToStartPosition;
	private Vector3 _moveToEndPosition;
	private float _moveToTimer;

	private BulletsBehaviour _bullets;

	[Inject]
	public void Construct(
		BulletsBehaviour bullets)
	{
		_bullets = bullets;
	}

	private void Awake()
	{
		_characterController = GetComponent<CharacterController>();
		_barrelPosition = m_barrel.localPosition;
	}

	public void Init()
	{
		OnShoot = null;
		OnEnergyGone = null;
		OnMoveToComplete = null;

		_isShooting = false;
		_isMoving = false;

		_bullet = null;

		SetEnergy(0);
	}

	public float TakeEnergy(float energy)
	{
		_energy -= energy;

		SetEnergy(_energy);

		return energy;
	}
	public void SetEnergy(float energy)
	{
		_energy = energy;

		m_body.localScale = Vector3.one * (_energy / RuntimeConstants.ENERGY_TO_SCALE);

		m_barrel.localPosition = _barrelPosition + (Vector3.forward * (m_body.localScale.x - 1));

		OnEnergyChange?.Invoke();

		if (_energy <= 0.0f)
		{
			OnEnergyGone?.Invoke();
		}
	}

	public void ShootStart()
	{
		if (_isShooting)
		{
			return;
		}

		_isShooting = true;

		_bullet = _bullets.CreateBullet(m_barrel.position);
		_bullet.AddEnergy(TakeEnergy(m_config.minEnergyChargeStart));
	}
	public void ShootEnd()
	{
		if (!_isShooting)
		{
			return;
		}

		_isShooting = false;

		_bullet.Shoot();
		_bullet = null;

		OnShoot?.Invoke();
	}

	public void MoveTo(Vector3 position, bool useBarrelCompensation = false)
	{
		if (useBarrelCompensation)
		{
			position -= m_barrel.position - transform.position;
		}

		_isMoving = true;

		_moveToStartPosition = transform.position;
		_moveToEndPosition = position;

		_moveToTimer = 0.0f;
	}

	public void OnUpdate(float deltaTime)
	{
		if (_isShooting)
		{
			_bullet.AddEnergy(TakeEnergy(m_config.energyChargeSpeed * deltaTime));
		}

		if (_isMoving)
		{
			_moveToTimer += deltaTime;

			float t = Mathf.Clamp01(_moveToTimer / m_config.moveToDuration);

			Vector3 position = Vector3.Lerp(_moveToStartPosition, _moveToEndPosition, m_config.moveToPositionCurve.Evaluate(t));
			position.y += m_config.moveToHeight * m_config.moveToHeightCurve.Evaluate(t);

			transform.position = position;

			if (_moveToTimer >= m_config.moveToDuration)
			{
				_isMoving = false;

				OnMoveToComplete?.Invoke();
			}
		}
	}
}