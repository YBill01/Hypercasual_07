using GameName.Data;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour, IUpdatable
{
	[SerializeField]
	private ObstacleConfigData m_config;

	[Space]
	[SerializeField]
	private Transform m_body;

	[Space]
	[SerializeField]
	private Material m_damageMaterial;

	private MeshRenderer _meshRenderer;

	private bool _isDispose;
	public bool IsDispose => _isDispose;
	
	private bool _isDamage;
	public bool IsDamage => _isDamage;

	private Material _defaultMaterial;

	private Vector3 _defaultPosition;
	private Quaternion _defaultQuaternion;
	private Vector3 _defaultScale;

	private Vector3 _disposePosition;
	private Quaternion _disposeStartQuaternion;
	private Quaternion _disposeQuaternion;
	private Vector3 _disposeScale;

	private float _time = 0.0f;

	private void Awake()
	{
		_meshRenderer = m_body.GetComponent<MeshRenderer>();

		_defaultMaterial = _meshRenderer.material;

		_defaultPosition = m_body.localPosition;
		_defaultQuaternion = m_body.localRotation;
		_defaultScale = m_body.localScale;
	}

	public void Init()
	{
		_isDispose = false;
		_isDamage = false;

		_time = 0.0f;

		_meshRenderer.material = _defaultMaterial;

		m_body.localPosition = _defaultPosition;
		m_body.localRotation = _defaultQuaternion;
		m_body.localScale = _defaultScale;

		Vector3 rotation = m_body.localRotation.eulerAngles;
		_disposeStartQuaternion = Quaternion.Euler(rotation.x, Random.Range(-180.0f, 180.0f), rotation.z);
		m_body.localRotation = _disposeStartQuaternion;
	}

	public void Damage()
	{
		_isDamage = true;

		_meshRenderer.material = m_damageMaterial;

		_disposePosition = _defaultPosition + new Vector3(0.0f, Random.Range(2.0f, 3.0f), 0.0f);
		_disposeQuaternion = Random.rotation;
		_disposeScale = Vector3.zero;
	}

	public void OnUpdate(float deltaTime)
	{
		if (_isDamage)
		{
			_time += deltaTime;

			float t = Mathf.Clamp01(m_config.damageTimeCurve.Evaluate(_time / m_config.damageDuration));

			m_body.localPosition = Vector3.Lerp(_defaultPosition, _disposePosition, t);
			m_body.localRotation = Quaternion.Lerp(_disposeStartQuaternion, _disposeQuaternion, t);
			m_body.localScale = Vector3.Lerp(_defaultScale, _disposeScale, t);

			if (_time >= m_config.damageDuration)
			{
				_isDispose = true;
			}
		}
	}
}