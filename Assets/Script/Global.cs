using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoSingleton<Global>
{
    public List<MapLayer> Layers = new List<MapLayer>();
    public Cartographic RelativePosition = new Cartographic();
    [SerializeField]
    [Range(1, 18)]
    private uint _level = 1;
    public uint level
    {
        get { return _level; }
        set
        {
            if (value < 1)
            {
                value = 1;
            }
            if (value > 18)
            {
                value = 18;
            }
            _level = value;
        }
    }

    [SerializeField]
    public ObjectPool TilePool;
    [SerializeField]
    public LayerMask TerrainLayer;
    // Start is called before the first frame update
    void Start()
    {
        TilePool.Initialize();
        TilePool.parent = transform;
        CreateTotalMatrix(1);
    }

    void CreateTotalMatrix(int matrix)
    {
        for (int row = 0; row < Math.Pow(2, matrix - 1); row++)
        {
            for (int col = 0; col < Math.Pow(2, matrix); col++)
            {
                StartCoroutine(CreateTile(MapTile.GetTilePosition(matrix, row, col)));
            }
        }
    }

    public IEnumerator CreateTile(MapTile position)
    {
        var tile = TilePool.GetObject().GetComponent<GISTile>();
        yield return tile.LinkTile(position);
        yield return new WaitForSeconds(60);
        TilePool.ReturnObject(tile.gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
