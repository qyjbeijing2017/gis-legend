using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoSingleton<Global>
{
    [SerializeField]
    public List<MapLayer> Layers = new List<MapLayer>();
    [SerializeField]
    public Cartesian3 _relativePosition = new Cartesian3(0, 0, 0);

    public Action<Cartesian3, Cartesian3> OnPositionChanged;
    public Cartesian3 relativePosition {
        get {
            return _relativePosition;
        }
        set {
            if (OnPositionChanged != null) {
                OnPositionChanged(_relativePosition, value);
            }
            _relativePosition = value;
        }
    }

    [SerializeField]
    public ObjectPool TilePool;
    [SerializeField]
    public float TileSize = 10;
    [SerializeField]
    public LayerMask TerrainLayer;
    // Start is called before the first frame update
    void Start()
    {
        TilePool.Initialize();
        TilePool.parent = transform;
        CreateTotalMatrix(3);
    }

    void CreateTotalMatrix(int matrix) {
        for (int row = 0; row < Math.Pow(2, matrix - 1); row++)
        {
            for (int col = 0; col < Math.Pow(2, matrix); col++)
            {
                StartCoroutine(CreateTile(MapTile.GetTilePosition(matrix, row, col)));
            }
        }
    }

    IEnumerator CreateTile(MapTile position)
    {
        var tile = TilePool.GetObject().GetComponent<GISTile>();
        yield return tile.LinkTile(position);
        yield return new WaitForSeconds(60);
        TilePool.ReturnObject(tile.gameObject);
    }

    public Cartesian3 WorldToCartesian(Vector3 position) {
        return new Cartesian3(
            position.x + _relativePosition.x,
            position.y + _relativePosition.y,
            position.z + _relativePosition.z
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
