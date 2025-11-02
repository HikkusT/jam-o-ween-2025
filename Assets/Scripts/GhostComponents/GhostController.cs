using System.Collections.Generic;
using DefaultNamespace.GhostComponents;
using Interfaces;
using PlayerComponents;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] private CharacterMotor _motor;
    [SerializeField] private float _speed;

    private Frenzy _frenzy;
    private PathFinder _pathFinder;
    
    private Dictionary<ChasePatterns, IChaseStrategy> _chaseStrategies = new();
    public ChasePatterns ChasePattern { get; private set; }
    private IChaseStrategy _frenzyStrategy;

    private void Awake()
    {
        ChasePattern = (ChasePatterns)Random.Range(0, System.Enum.GetValues(typeof(ChasePatterns)).Length);
    }
    
    private void Start()
    {
        var player = FindFirstObjectByType<CharacterController>();
        var map = FindFirstObjectByType<GameMap>();
        _frenzy = player.GetComponent<Frenzy>();
        _pathFinder = new PathFinder(map);

        _chaseStrategies.Add(ChasePatterns.DirectToPlayer, new ChasePlayerStrategy(player));
        _chaseStrategies.Add(ChasePatterns.PredictPlayer, new ChasePlayerAheadStrategy(map, player, 2));
        _chaseStrategies.Add(ChasePatterns.Random, new RandomChaseStrategy(map, _motor, 10));
        
        _motor.Setup(new ConstantSpeed(_speed));
    }
    
    private void Update()
    {
        if (_motor.IsBusy) return;

        if (_frenzy.IsActive)
        {
            _frenzyStrategy ??= new ScatterChaseStrategy(FindFirstObjectByType<GameMap>(), _motor);
            _pathFinder.UpdatePathing(from: _motor.CurrentPosition, to: _frenzyStrategy.GetTarget());
        }
        else
        {
            _frenzyStrategy = null;
            _pathFinder.UpdatePathing(from: _motor.CurrentPosition, to: _chaseStrategies[ChasePattern].GetTarget());
        }
        
        if (_pathFinder.TryGetNextNode(out Vector2Int next))
        {
            var direction = next - _motor.CurrentPosition;
            if (direction.magnitude > 1f)
            {
                
            }
            _motor.TryMoveTowards(direction);
        }
    }
}