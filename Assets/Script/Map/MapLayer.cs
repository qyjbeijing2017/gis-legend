using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "MapLayer", menuName = "ScriptableObjects/MapLayer")]
public class MapLayer : ScriptableObject
{
    [SerializeField]
    private string Url = "https://t{RandomIndex}.tianditu.gov.cn/vec_c/wmts?service=wmts&request=GetTile&version=1.0.0&LAYER=vec&tileMatrixSet=w&TileMatrix={TileMatrix}&TileRow={TileRow}&TileCol={TileCol}&style=default&format=tiles&tk={Token}";
    [SerializeField]
    private string Token = "448278313c014248463f423b2742025e";

    [SerializeField]
    private int RangeMax = 8;
    private Dictionary<MapTile, Texture2D> tiles = new Dictionary<MapTile, Texture2D>();

    public bool HasTile(MapTile position)
    {
        return tiles.ContainsKey(position);
    }

    public Texture2D GetTile(MapTile position)
    {
        return tiles[position];
    }

    public IEnumerator LoadTile(MapTile position)
    {
        if (!HasTile(position))
        {
            yield return Request(position);
        }
    }

    private IEnumerator Request(MapTile position)
    {
        string url = Url
            .Replace("{Token}", Token)
            .Replace("{TileMatrix}", position.matrix.ToString())
            .Replace("{TileRow}", position.row.ToString())
            .Replace("{TileCol}", position.col.ToString())
            .Replace("{RandomIndex}", Random.Range(0, RangeMax).ToString());

        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
            }
            else
            {
                var result = DownloadHandlerTexture.GetContent(request);
                tiles.Add(position, result);
            }
        }
    }
}
