using System;
using UnityEngine;

[Serializable]
public struct Cartographic {
    [SerializeField]
    private double _longitude;
    [SerializeField]
    private double _latitude;
    [SerializeField]
    private double _height;
    public double longitude {
        get {
            return _longitude;
        }
        set {
            _longitude = value;
        }
    }
    public double latitude {
        get {
            return _latitude;
        }
        set {
            _latitude = value;
        }
    }
    public double height {
        get {
            return _height;
        }
        set {
            _height = value;
        }
    }

    public double longitudeRadius {
        get {
            return longitude * 180.0 / Math.PI;
        }
        set {
            longitude = value * Math.PI / 180.0;
        }
    }

    public double latitudeRadius {
        get {
            return latitude * 180.0 / Math.PI;
        }
        set {
            latitude = value * Math.PI / 180.0;
        }
    }

    public Cartographic(double longitude = 0, double latitude = 0, double height = 0) {
        this._longitude = longitude;
        this._latitude = latitude;
        this._height = height;
    }

    public static Cartographic operator +(Cartographic a, Cartographic b) {
        return new Cartographic(a.longitude + b.longitude, a.latitude + b.latitude, a.height + b.height);
    }

    public static Cartographic operator -(Cartographic a, Cartographic b) {
        return new Cartographic(a.longitude - b.longitude, a.latitude - b.latitude, a.height - b.height);
    }

    public static Cartographic operator *(Cartographic a, double b) {
        return new Cartographic(a.longitude * b, a.latitude * b, a.height * b);
    }

    public static Cartographic operator /(Cartographic a, double b) {
        return new Cartographic(a.longitude / b, a.latitude / b, a.height / b);
    }

    public static bool operator ==(Cartographic a, Cartographic b) {
        return a.longitude == b.longitude && a.latitude == b.latitude && a.height == b.height;
    }

    public static bool operator !=(Cartographic a, Cartographic b) {
        return a.longitude != b.longitude || a.latitude != b.latitude || a.height != b.height;
    }

    public static Cartographic Zero {
        get {
            return new Cartographic(0, 0, 0);
        }
    }

    public override bool Equals(object obj) {
        if (obj is Cartographic) {
            return this == (Cartographic)obj;
        }
        return false;
    }

    public override int GetHashCode() {
        return longitude.GetHashCode() ^ latitude.GetHashCode() ^ height.GetHashCode();
    }

    public static Cartographic fromRadius(double longitude, double latitude, double height) {
        return new Cartographic(longitude * Math.PI / 180.0, latitude * Math.PI / 180.0, height);
    }
}