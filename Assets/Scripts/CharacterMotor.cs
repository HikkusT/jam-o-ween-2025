using System;
using Interfaces;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    [SerializeField] private GameMap _map;
    [SerializeField] private Transform _view;

    private Vector2Int? _targetPosition;
    private ISpeedProvider _speedProvider;
    
    public bool IsBusy =>  _targetPosition.HasValue;
    public Vector2Int CurrentPosition => Vector2Int.FloorToInt(new Vector2(transform.position.x, transform.position.z));

    public void Setup(ISpeedProvider speedProvider)
    {
        _speedProvider = speedProvider;
    }

    public bool TryMoveTowards(Vector2Int direction)
    {
        if (IsBusy)
        {
            Debug.LogError("Trying to move character in mid movement");
            return false;
        }

        if (direction == Vector2Int.zero)
        {
            Debug.LogError("Trying to move with zero direction");
            return false;
        }

        Vector2Int desiredPosition = CurrentPosition + direction;
        
        if (!_map.IsTileEmpty(desiredPosition)) return false;

        _targetPosition = desiredPosition;
        if (_view)
        {
            Quaternion look = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y), Vector3.up);
            _view.rotation = Quaternion.Euler(_view.rotation.eulerAngles.x, look.eulerAngles.y, look.eulerAngles.z);
        }

        return true;
    }

    
    
    private void Update()
    {
        if (_targetPosition == null) return;

        Vector3 delta = new Vector3(_targetPosition.Value.x + 0.5f, transform.position.y, _targetPosition.Value.y + 0.5f) - transform.position;
        Vector3 amountToMove = Vector3.ClampMagnitude(Time.deltaTime * _speedProvider.Speed * delta.normalized, delta.magnitude);
        
        transform.position += amountToMove;
        if (amountToMove == delta)
        {
            _targetPosition = null;
        }
    }
}
