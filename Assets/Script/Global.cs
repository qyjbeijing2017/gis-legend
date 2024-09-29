using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoSingleton<Global>
{
    [SerializeField]
    public Ellipsoid Ellipsoid = Ellipsoid.WGS84;

    [SerializeField]
    MapManager mapManager;

    [SerializeField]
    ObjectPool tilePool;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Cartesian3 SphericalToWorld(Cartographic cartographic)
    {
        return new Cartesian3(
            Ellipsoid.equatorLength * cartographic.longitude / 360,
            Ellipsoid.equatorLength * Math.Log(Math.Tan(Math.PI / 4 + Math.PI * cartographic.latitude / 360)) / Math.PI,
            cartographic.height
        );
    }

    public Cartographic WorldToSpherical(Cartesian3 world)
    {
        return new Cartographic(
            360 * world.x / Ellipsoid.equatorLength,
            360 * Math.Atan(Math.Exp(world.y * Math.PI / Ellipsoid.equatorLength)) / Math.PI - 90,
            world.z
        );
    }
}
