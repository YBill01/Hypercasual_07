using System;
using System.Collections.Generic;
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
		_isPointerOverGameObject = EventSystem.current.IsPointerOverGameObject();
	}

	private void OnAttackButton(InputAction.CallbackContext context)
	{
		bool value = context.ReadValueAsButton();

#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
		if (IsPointerOverGameObject())
		{
			return;
		}
#else
		if (_isPointerOverGameObject)
		{
			return;
		}
#endif

		OnAttack?.Invoke(value);
	}

	private bool IsPointerOverGameObject()
	{
		if (Touchscreen.current.primaryTouch.value.phase == UnityEngine.InputSystem.TouchPhase.Began)
		{
			PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
			{
				position = Touchscreen.current.primaryTouch.position.ReadValue()
			};

			List<RaycastResult> results = new List<RaycastResult>();
			EventSystem.current.RaycastAll(pointerEventData, results);

			return results.Count > 0;
		}

		return false;
	}
}