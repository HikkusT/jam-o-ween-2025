using Interfaces;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] private CharacterMotor _motor;
    [SerializeField] private float _speed;
    
    private CharacterController _player;
    private PathFinder _pathFinder;

    private void Start()
    {
        _player = FindFirstObjectByType<CharacterController>();
        _pathFinder = new PathFinder(FindFirstObjectByType<GameMap>());
        _motor.Setup(new ConstantSpeed(_speed));
    }
    
    private void Update()
    {
        if (_motor.IsBusy) return;
       
        _pathFinder.UpdatePathing(from: _motor.CurrentPosition, to: _player.Motor.CurrentPosition);
        
        if (_pathFinder.TryGetNextNode(out Vector2Int next))
        {
            var direction = next - _motor.CurrentPosition;
            _motor.TryMoveTowards(direction);
        }
    }
}