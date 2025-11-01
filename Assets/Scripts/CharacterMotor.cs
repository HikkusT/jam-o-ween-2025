using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class CharacterMotor : MonoBehaviour
    {
        [SerializeField] private GameMap _map;
        [SerializeField] private float _speed;

        private Vector2Int? _targetPosition;
        
        public bool IsBusy =>  _targetPosition.HasValue;

        public bool TryMoveTowards(Vector2Int direction)
        {
            if (IsBusy)
            {
                Debug.LogError("Trying to move character in mid movement");
                return false;
            }

            if (direction == Vector2Int.zero)
            {
                throw new ArgumentNullException("Trying to move with zero direction");
            }

            Vector2Int desiredPosition =
                Vector2Int.FloorToInt(new Vector2(transform.position.x, transform.position.z)) +
                direction;
            
            if (!_map.IsTileEmpty(desiredPosition)) return false;

            _targetPosition = desiredPosition;
            return true;
        }

        private void Update()
        {
            if (_targetPosition == null) return;

            Vector3 delta = new Vector3(_targetPosition.Value.x + 0.5f, transform.position.y, _targetPosition.Value.y + 0.5f) - transform.position;
            Vector3 amountToMove = Vector3.ClampMagnitude(Time.deltaTime * _speed * delta.normalized, delta.magnitude);
            
            transform.position += amountToMove;
            if (amountToMove == delta)
            {
                _targetPosition = null;
            }
        }
    }
}