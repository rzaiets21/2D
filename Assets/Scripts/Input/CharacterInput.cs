using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    private const string PlayerActionMapName = "Player";
    
    [SerializeField] private InputActionAsset inputActionAsset;

    private InputAction _movementAction;
    private InputAction _attackAction;

    public event Action onClickAttack;
    
    private void OnEnable()
    {
        Init();
    }

    private void OnDisable()
    {
        _movementAction?.Disable();

        if (_attackAction != null)
        {
            _attackAction.performed += OnAttackActionPerformed;
            _attackAction.Disable();
        }
    }

    private void Init()
    {
        var playerActionMap = inputActionAsset.actionMaps.FirstOrDefault(x => x.name == PlayerActionMapName);
        if (playerActionMap == null)
            throw new NullReferenceException($"Action map with name: {PlayerActionMapName} not found!");
        
        _movementAction = inputActionAsset.FindAction("Movement");
        _movementAction.Enable();
        
        _attackAction = inputActionAsset.FindAction("Attack");
        _attackAction.performed += OnAttackActionPerformed;
        _attackAction.Enable();
    }

    public Vector2 GetMovementVector()
    {
        return _movementAction.ReadValue<Vector2>();
    }

    private void OnAttackActionPerformed(InputAction.CallbackContext obj)
    {
        onClickAttack?.Invoke();
    }
}