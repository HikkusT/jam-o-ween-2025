using UnityEngine;

namespace DefaultNamespace.GhostComponents
{
    public enum ChasePatterns
    {
        DirectToPlayer,
        PredictPlayer,
        Random
    }
    
    public interface IChaseStrategy
    {
        Vector2Int GetTarget();
    }
    
    public class ChasePlayerStrategy : IChaseStrategy
    {
        private readonly CharacterController _player;

        public ChasePlayerStrategy(CharacterController player)
        {
            _player = player;
        }

        public Vector2Int GetTarget() => _player.Motor.CurrentPosition;
    }
    
    public class ChasePlayerAheadStrategy : IChaseStrategy
    {
        private readonly GameMap _map;
        private readonly CharacterController _player;
        private readonly int _predictionAmount;

        public ChasePlayerAheadStrategy(GameMap map, CharacterController player, int predictionAmount)
        {
            _map = map;
            _player = player;
            _predictionAmount = predictionAmount;
        }

        public Vector2Int GetTarget()
        {
            Vector2Int result = _player.Motor.CurrentPosition;

            if (_player.CurrentDirection.HasValue)
            {
                for (int i = 1; i <= _predictionAmount; i++)
                {
                    if (_map.IsTileEmpty(result + _player.CurrentDirection.Value))
                    {
                        result += _player.CurrentDirection.Value;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return result;
        }
    }

    public class RandomChaseStrategy : IChaseStrategy
    {
        private readonly GameMap _map;
        private readonly CharacterMotor _motor;
        private readonly int _radius;

        private Vector2Int? _currentTarget;

        public RandomChaseStrategy(GameMap map, CharacterMotor motor, int radius)
        {
            _map = map;
            _motor = motor;
            _radius = radius;
        }

        public Vector2Int GetTarget()
        {
            if (_currentTarget == null || _motor.CurrentPosition == _currentTarget.Value)
            {
                _currentTarget = RandomizePosition();
            }

            return _currentTarget.Value;
        }

        private Vector2Int RandomizePosition()
        {
            while (true)
            {
                Vector2Int testPosition = _motor.CurrentPosition + Vector2Int.RoundToInt(Random.insideUnitCircle * _radius);
                if (_map.IsTileEmpty(testPosition))
                {
                    return testPosition;
                }
            }
        }
    }
    
    public class ScatterChaseStrategy : IChaseStrategy
    {
        private readonly GameMap _map;
        private readonly CharacterMotor _motor;

        private Vector2Int _target;

        public ScatterChaseStrategy(GameMap map, CharacterMotor motor)
        {
            _map = map;
            _motor = motor;
            
            _target = new Vector2Int(Random.value < 0.5f ? 1 : _map.GetMapWidth() - 3, Random.value < 0.5f ? 1 : _map.GetMapHeight() - 2);
        }

        public Vector2Int GetTarget()
        {
            if (_motor.CurrentPosition == _target)
            {
                _target = new Vector2Int(Random.value < 0.5f ? 1 : _map.GetMapWidth() - 3, Random.value < 0.5f ? 1 : _map.GetMapHeight() - 2);
            }

            return _target;
        }
    }
}