using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
	public event Action<bool> OnAttack;

	private InputSystem_Actions _inputActions;

	private bool _isPointerOverGameObject;

	private void Awake()
	{
		_inputActions = new InputSystem_Actions();
	}

	private void OnEnable()
	{
		_inputActions.Enable();

		_inputActions.Player.Attack.performed += OnAttackButton;
		_inputActions.Player.Attack.canceled += OnAttackButton;
	}
	private void OnDisable()
	{
		_inputActions.Player.Attack.performed -= OnAttackButton;
		_inputActions.Player.Attack.canceled -= OnAttackButton;

		_inputActions.Disable();
	}

	private void Update()
	{
		_isPointerOverGameObject = false;

#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began)
		{
			_isPointerOverGameObject = EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
		}
#else
		_isPointerOverGameObject = EventSystem.current.IsPointerOverGameObject();
#endif
	}

	private void OnAttackButton(InputAction.CallbackContext context)
	{
		bool value = context.ReadValueAsButton();

		if (_isPointerOverGameObject)
		{
			return;
		}

		OnAttack?.Invoke(value);
	}
}