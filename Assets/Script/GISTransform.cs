using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GISTransform : MonoBehaviour
{
    public Cartographic cartographic;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.parent = Global.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
