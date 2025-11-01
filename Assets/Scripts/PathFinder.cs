using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{
    private static readonly Vector2Int[] DIRECTIONS = {
        Vector2Int.right, 
        Vector2Int.left, 
        Vector2Int.up, 
        Vector2Int.down
    };
    
    private readonly GameMap _map;
    private readonly Queue<Vector2Int> _path = new();
    private Vector2Int _currentTarget;
    
    private readonly Queue<Vector2Int> _toVisit = new();
    private readonly Dictionary<Vector2Int, Vector2Int> _visitedFrom = new();
    private readonly List<Vector2Int> _reversePath = new();
    
    public PathFinder(GameMap map)
    {
        _map = map;
    }

    public void UpdatePathing(Vector2Int from, Vector2Int to)
    {
        if (to == _currentTarget) return;

        _currentTarget = to;
        _path.Clear();
        _toVisit.Clear();
        _visitedFrom.Clear();

        if (!_map.IsTileEmpty(to))
        {
            Debug.LogError("Pathfinding to invalid tile!!!");
            return;
        }
        
        _toVisit.Enqueue(from);
        _visitedFrom[from] = from;

        bool found = false;

        while (_toVisit.Count > 0)
        {
            var current = _toVisit.Dequeue();

            if (current == to)
            {
                found = true;
                break;
            }

            foreach (var dir in DIRECTIONS)
            {
                var next = current + dir;
                if (_visitedFrom.ContainsKey(next))
                    continue;
                if (!_map.IsTileEmpty(next))
                    continue;

                _visitedFrom[next] = current;
                _toVisit.Enqueue(next);
            }
        }

        if (!found)
        {
            Debug.LogError("Failed to find path");
            return;
        }
        
        var step = to;
        while (step != from)
        {
            _reversePath.Add(step);
            step = _visitedFrom[step];
        }
        for (int i = _reversePath.Count - 1; i >= 0; i--)
        {
            _path.Enqueue(_reversePath[i]);
        }
    }

    public bool TryGetNextNode(out Vector2Int node) => _path.TryDequeue(out node);
}