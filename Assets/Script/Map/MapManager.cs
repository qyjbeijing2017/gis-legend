using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapManager", menuName = "ScriptableObjects/MapManager")]
public class MapManager : ScriptableObject
{
    [SerializeField]
    List<MapLayer> layers = new List<MapLayer>();
    [SerializeField]
    MapLayer terrain;

    public IEnumerator LoadTile(MapTile position) {
        foreach(var layer in layers) {
            yield return layer.LoadTile(position);
        }
        if(terrain != null) {
            yield return terrain.LoadTile(position);
        }
    }
}
