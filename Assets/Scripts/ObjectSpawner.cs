using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _spawnRate = 10f;
    private float _nextSpawn;

    private GameMap _gameMap;

    private void Start()
    {
        _gameMap = FindFirstObjectByType<GameMap>();
        int numSpawns = (int)(10 * _spawnRate);
        for (; numSpawns >= 0; numSpawns--)
        {
            SpawnObject();
        }
    }

    private void Update()
    {
        if (_nextSpawn <= 0)
        {
            SpawnObject();
            _nextSpawn = GetSpawnTime();
        }

        _nextSpawn -= Time.deltaTime;
    }

    public float GetSpawnTime()
    {
        return 1 / _spawnRate;
    }

    public void SpawnObject()
    {
        int maxSpawnAttempts = 3;
        for (int i = 0; i < maxSpawnAttempts; i++)
        {
            int x = Random.Range(0, _gameMap.GetMapWidth());
            int z = Random.Range(0, _gameMap.GetMapHeight());
            Vector2Int gridPosition = new(x, z);

            if (_gameMap.IsTileEmpty(gridPosition))
            {
                Vector3 worldPosition = new Vector3(x + 0.5f, 0, z + 0.5f);

                Instantiate(_prefab, worldPosition, Quaternion.identity);
                return;
            }
        }
    }
}