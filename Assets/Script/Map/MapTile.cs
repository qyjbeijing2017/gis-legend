using System;
using System.Collections.Generic;

public struct MapTile
{
    static List<MapTile> TilePositions = new List<MapTile>();
    public int matrix { get; }
    public int row { get; }
    public int col { get; }

    public int totalCol { get;}

    public int totalRow { get; }

    public MapTile parent { get { return GetTilePosition(matrix - 1, row / 2, col / 2); } }

    public MapTile child00 { get { return GetTilePosition(matrix + 1, row * 2, col * 2); } }

    public MapTile child01 { get { return GetTilePosition(matrix + 1, row * 2, col * 2 + 1); } }

    public MapTile child10 { get { return GetTilePosition(matrix + 1, row * 2 + 1, col * 2); } }

    public MapTile child11 { get { return GetTilePosition(matrix + 1, row * 2 + 1, col * 2 + 1); } }

    private MapTile(int matrix, int row, int col)
    {
        this.matrix = matrix;
        this.row = row;
        this.col = col;
        totalCol = (int)Math.Pow(2, matrix);
        totalRow = (int)Math.Pow(2, matrix);
    }
    public static MapTile GetTilePosition(int TileMatrix, int TileRow, int TileCol)
    {
        foreach (MapTile position in TilePositions)
        {
            if (position.matrix == TileMatrix && position.row == TileRow && position.col == TileCol)
            {
                return position;
            }
        }
        MapTile tilePosition = new MapTile(TileMatrix, TileRow, TileCol);
        TilePositions.Add(tilePosition);
        return tilePosition;
    }
}