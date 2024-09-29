
using System;

[Serializable]
public struct Cartesian3
{
    public double x;
    public double y;
    public double z;
    public Cartesian3(double x = 0, double y = 0, double z = 0)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static Cartesian3 operator +(Cartesian3 a, Cartesian3 b)
    {
        return new Cartesian3(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static Cartesian3 operator -(Cartesian3 a, Cartesian3 b)
    {
        return new Cartesian3(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    public static Cartesian3 operator *(Cartesian3 a, double b)
    {
        return new Cartesian3(a.x * b, a.y * b, a.z * b);
    }

    public static Cartesian3 operator /(Cartesian3 a, double b)
    {
        return new Cartesian3(a.x / b, a.y / b, a.z / b);
    }

    public static bool operator ==(Cartesian3 a, Cartesian3 b)
    {
        return a.x == b.x && a.y == b.y && a.z == b.z;
    }

    public static bool operator !=(Cartesian3 a, Cartesian3 b)
    {
        return a.x != b.x || a.y != b.y || a.z != b.z;
    }

    public static Cartesian3 Zero
    {
        get
        {
            return new Cartesian3(0, 0, 0);
        }
    }

    public override bool Equals(object obj)
    {
        if (obj is Cartesian3)
        {
            return this == (Cartesian3)obj;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
    }
}
