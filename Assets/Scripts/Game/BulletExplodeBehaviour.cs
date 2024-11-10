using GameName.Data;
using GameName.Utilities;
using UnityEngine;

public class BulletExplodeBehaviour : MonoBehaviour, IUpdatable
{
	[SerializeField]
	private BulletConfigData m_config;

	[Space]
	[SerializeField]
	private Transform m_body;

	[Space]
	[SerializeField]
	private LayerMask m_interactLayerMask;

	private float _energy;
	public float Energy => _energy;

	private bool _isDispose;
	public bool IsDispose => _isDispose;

	private bool _isExplode;
	private bool _isExploded;
	public bool IsExplode => _isExplode && !_isExploded;

	public Vector3 ExplodePosition => transform.position;
	public float ExplodeRadius => _endSize / 2;

	private float _time = 0.0f;

	private float _startSize;
	private float _endSize;

	public void Init()
	{
		_isDispose = false;
		_isExplode = false;
		_isExploded = false;

		_time = 0.0f;

		SetEnergy(0.0f);
	}

	public void SetEnergy(float energy)
	{
		_energy = energy;

		_startSize = _energy / RuntimeConstants.ENERGY_TO_SCALE;
		_endSize = _startSize * m_config.explosionSizeMultiplier;

		SetSize(_startSize);
	}

	private void SetSize(float size)
	{
		m_body.localScale = Vector3.one * size;
	}

	public void Explode()
	{
		_isExploded = true;
	}

	public void OnUpdate(float deltaTime)
	{
		_time += deltaTime;

		float t = Mathf.Clamp01(m_config.explosionScaleCurve.Evaluate(_time / m_config.explosionDuration));

		SetSize(_startSize + ((_endSize - _startSize) * t));

		if (t >= m_config.explosionDelayRatio)
		{
			_isExplode = true;
		}

		if (_time >= m_config.explosionDuration)
		{
			_isDispose = true;
		}
	}
}