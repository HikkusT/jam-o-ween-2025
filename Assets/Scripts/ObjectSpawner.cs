using System;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _spawnRate = 10f;
    private float _nextSpawn;
    
    public int Intensity => _rogueLikeManager.GetIntensity();
    public float SpawnRate => _spawnRate;

    private GameMap _gameMap;
    private RogueLikeManager _rogueLikeManager;

    private void Start()
    {
        _gameMap = FindFirstObjectByType<GameMap>();
        _rogueLikeManager = FindFirstObjectByType<RogueLikeManager>();
        
        int numSpawns = (int)(10 * _spawnRate);
        for (; numSpawns >= 0; numSpawns--)
        {
            SpawnObject();
        }

        _nextSpawn = GetSpawnTime();
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

    public abstract float GetSpawnTime();

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