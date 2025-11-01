using PlayerComponents;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private CharacterMotor _motor;
    [SerializeField] private float _bufferTime = 0.2f;

    private float _inputLastUpdatedAt = float.MaxValue;
    private Vector2Int? _inputBuffer;
    private Vector2Int? _currentDirection;

    private void Start()
    {
        InputSystem.actions.FindAction("Move").performed += StoreInput;
        InputSystem.actions.FindAction("Move").canceled += _ => _inputLastUpdatedAt = Time.time;
        _motor.Setup(_playerStats);
    }

    private void Update()
    {
        if (Time.time > _inputLastUpdatedAt + _bufferTime)
        {
            _inputBuffer = null;
        }

        if (_inputBuffer.HasValue && !_motor.IsBusy)
        {
            if (_motor.TryMoveTowards(_inputBuffer.Value))
            {
                _currentDirection = _inputBuffer.Value;
            }
            else if (_currentDirection.HasValue)
            {
                _motor.TryMoveTowards(_currentDirection.Value);
            }
        }
    }

    private void StoreInput(InputAction.CallbackContext ctx)
    {
        var rawInput = Vector2Int.RoundToInt(ctx.ReadValue<Vector2>());
        if (Mathf.Abs(rawInput.x) > Mathf.Abs(rawInput.y))
        {
            rawInput = new Vector2Int(rawInput.x, 0);
        }
        else
        {
            rawInput = new Vector2Int(0, rawInput.y); 
        }

        _inputBuffer = rawInput;
        _inputLastUpdatedAt = float.MaxValue;
    }
}