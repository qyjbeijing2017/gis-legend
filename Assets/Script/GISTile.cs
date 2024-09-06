using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GISTile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator LinkTile(MapTile position)
    {
        Global.Instance.OnPositionChanged += OnPositionChanged;
        yield return Global.Instance.Layers[0].LoadTile(position);
        this.transform.localPosition = new Vector3(
            (float)(position.position.x - Global.Instance.relativePosition.x),
            0, 
            (float)(position.position.y - Global.Instance.relativePosition.z)
        );
        this.transform.localScale = new Vector3(
            (float)position.size.x / Global.Instance.TileSize, 
            1, 
            (float)position.size.y / Global.Instance.TileSize
        );
        GetComponent<Renderer>().material.mainTexture = Global.Instance.Layers[0].GetTile(position);
    }

    void OnPositionChanged(Cartesian3 oldPosition, Cartesian3 newPosition)
    {
        var delta = newPosition - oldPosition;
        transform.position += new Vector3((float)delta.x, 0, (float)delta.z);
    }

    void OnDisable()
    {
        Global.Instance.OnPositionChanged -= OnPositionChanged;
    }
}
