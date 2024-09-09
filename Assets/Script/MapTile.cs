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
    public Cartographic center { get; }
    private MapTile(int matrix, int row, int col)
    {
        this.matrix = matrix;
        this.row = row;
        this.col = col;
        totalCol = (int)Math.Pow(2, matrix);
        totalRow = (int)Math.Pow(2, matrix - 1);
        double lon = 360.0 / totalCol * (col + 0.5) - 180;
        double lat = 180.0 / totalRow * (row + 0.5) - 90;
        this.center = new Cartographic(lon, lat);
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