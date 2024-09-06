using System;
using System.Collections.Generic;

public struct MapTile
{
    static List<MapTile> TilePositions = new List<MapTile>();
    public int matrix { get; }
    public int row { get; }
    public int col { get; }
    public Cartesian2 position { get; }
    public Cartesian2 size { get; }
    private MapTile(int matrix, int row, int col)
    {
        this.matrix = matrix;
        this.row = row;
        this.col = col;
        this.size = new Cartesian2(1, 1);
        var sumX = Math.Pow(2, matrix);
        var sumZ = Math.Pow(2, matrix - 1);
        var x = col - sumX / 2 + 0.5;
        var z = sumZ / 2 - row - 0.5;
        x = x / sumZ;
        z = z / sumZ;
        this.position = new Cartesian2(x, z);
        this.size = new Cartesian2(1 / sumZ, 1 / sumZ);
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