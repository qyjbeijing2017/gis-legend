using System;
using System.Collections;
using UnityEngine;

public class GISTile : MonoBehaviour
{
    public float TileSize = 0.1f;
    MapTile _mapTile { get; set; }
    bool _isLoaded = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!_isLoaded)
        {
            return;
        }

        var sizeLevel = (float)Math.Pow(2, Global.Instance.level - _mapTile.matrix);
        this.transform.localScale = new Vector3(
            sizeLevel * TileSize,
            1,
            sizeLevel * TileSize
        );

        var positionLevel = (float)Math.Pow(2, Global.Instance.level - 1);
        this.transform.localPosition = new Vector3(
            (float)(_mapTile.center.longitude - Global.Instance.RelativePosition.longitude) / 180 * positionLevel,
            0,
            (float)(_mapTile.center.latitude - Global.Instance.RelativePosition.latitude) / 90 * positionLevel
        );
    }

    public IEnumerator LinkTile(MapTile mapTile)
    {
        yield return Global.Instance.Layers[0].LoadTile(mapTile);
        GetComponent<Renderer>().material.mainTexture = Global.Instance.Layers[0].GetTile(mapTile);
        this._mapTile = mapTile;
        this._isLoaded = true;
    }

    public void OnReturnToPool()
    {
        _isLoaded = false;
    }

    void OnDisable()
    {
    }
}
