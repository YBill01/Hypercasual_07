using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TargetBehaviour : MonoBehaviour, IPausable
{
	[Space]
	[SerializeField]
	private LayerMask m_interactLayerMask;

	[Space]
	[SerializeField]
	private Transform m_goal;

	public Vector3 GoalPosition => m_goal.position;

	private Animator _animator;

	private int _animIDDoorsOpen;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	private void Start()
	{
		AssignAnimationIDs();
	}

	public void Init()
	{
		_animator.Rebind();
	}

	private void OnTriggerEnter(Collider collision)
	{
		if (IsInteract(collision))
		{
			_animator.SetTrigger(_animIDDoorsOpen);
		}
	}
	private void OnTriggerExit(Collider collision)
	{
		if (IsInteract(collision))
		{
			//TODO close doors...
		}
	}

	public void SetPause(bool pause)
	{
		if (pause)
		{
			_animator.speed = 0.0f;
		}
		else
		{
			_animator.speed = 1.0f;
		}
	}

	private bool IsInteract(Collider collision)
	{
		return (m_interactLayerMask & (1 << collision.gameObject.layer)) != 0;
	}

	private void AssignAnimationIDs()
	{
		_animIDDoorsOpen = Animator.StringToHash("DoorsOpen");
	}
}