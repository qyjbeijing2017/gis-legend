using System;
using UnityEngine;

[Serializable]
public struct Ellipsoid {
    [SerializeField]
    private double _equatorRadius;
    [SerializeField]
    private double _polarRadius;
    public double equatorRadius => _equatorRadius;
    public double polarRadius => _polarRadius;

    public double equatorLength {
        get {
            return 2 * Math.PI * equatorRadius;
        }
    }

    public double polarLength {
        get {
            return 2 * Math.PI * polarRadius;
        }
    }

    public double flattening {
        get {
            return (equatorRadius - polarRadius) / equatorRadius;
        }
    }

    public double inverseFlattening {
        get {
            return 1 / flattening;
        }
    }

    public double e2 {
        get {
            var f = flattening;
            return 2 * f - f * f;
        }
    }

    // equatorLength / polarLength
    public double eccentricity {
        get {
            return Math.Sqrt(1 - Math.Pow(polarRadius, 2) / Math.Pow(equatorRadius, 2));
        }
    }

    public Ellipsoid(double equatorRadius, double polarRadius) {
        _equatorRadius = equatorRadius;
        _polarRadius = polarRadius;
    }

    static public Ellipsoid WGS84 {
        get {
            return new Ellipsoid(6378137, 6356752.314245);
        }
    }
    static public Ellipsoid GRS80 {
        get {
            return new Ellipsoid(6378137, 6356752.314140);
        }
    }
    static public Ellipsoid WGS72 {
        get {
            return new Ellipsoid(6378135, 6356750.5);
        }
    }
    static public Ellipsoid International1924 {
        get {
            return new Ellipsoid(6378388, 6356911.946);
        }
    }
    static public Ellipsoid Clarke1880 {
        get {
            return new Ellipsoid(6378249.145, 6356514.86955);
        }
    }
    static public Ellipsoid Clarke1866 {
        get {
            return new Ellipsoid(6378206.4, 6356583.8);
        }
    }

}