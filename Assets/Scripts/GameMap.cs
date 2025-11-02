using UnityEngine;

public class GameMap : MonoBehaviour
{
    [SerializeField] private Texture2D _map;
    
    [Header("World Elements")]
    [SerializeField] private GameObject _pillarPrefab;
    [SerializeField] private GameObject _pelletPrefab;

    public bool[,] Geometry { get; private set; }

    public bool IsTileEmpty(Vector2Int position)
    {
        if (position.x < 0 || position.x >= _map.width || position.y < 0 || position.y >= _map.height) return false;
        
        return !Geometry[position.x, position.y];
    }

    [ContextMenu("Load")]
    public void BakeWorld()
    {
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }
        
        Geometry = new bool[_map.width, _map.height];
        for (int x = 0; x < _map.width; x++)
        {
            for (int y = 0; y < _map.height; y++)
            {
                if (_map.GetPixel(x, y) == Color.black)
                {
                    Geometry[x, y] = true;
                    Instantiate(_pillarPrefab, new Vector3(x + 0.5f, 0, y + 0.5f), Quaternion.identity, transform);
                }
                else
                {
                    Geometry[x, y] = false;
                }
            }
        }
    }

    private void SpawnInitialPellets()
    {
        GameObject pelletsParent = new GameObject("Pellets");
        for (int x = 0; x < _map.width; x++)
        {
            for (int y = 0; y < _map.height; y++)
            {
                if (IsTileEmpty(new Vector2Int(x, y)))
                {
                    Instantiate(_pelletPrefab,  new Vector3(x + 0.5f, 0, y + 0.5f), Quaternion.identity, pelletsParent.transform);
                }
            }
        }
    }

    public int GetMapWidth()
    {
        return _map.width;
    }

    public int GetMapHeight()
    {
        return _map.height;
    }
    

    private void Awake()
    {
        BakeWorld();
        //SpawnInitialPellets();
    }
}
