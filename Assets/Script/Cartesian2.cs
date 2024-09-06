using System;

[Serializable]
public struct Cartesian2
{
    public double x;
    public double y;

    public Cartesian2(double x = 0, double y = 0)
    {
        this.x = x;
        this.y = y;
    }

    public static Cartesian2 operator +(Cartesian2 a, Cartesian2 b)
    {
        return new Cartesian2(a.x + b.x, a.y + b.y);
    }

    public static Cartesian2 operator -(Cartesian2 a, Cartesian2 b)
    {
        return new Cartesian2(a.x - b.x, a.y - b.y);
    }

    public static Cartesian2 operator *(Cartesian2 a, double b)
    {
        return new Cartesian2(a.x * b, a.y * b);
    }

    public static Cartesian2 operator /(Cartesian2 a, double b)
    {
        return new Cartesian2(a.x / b, a.y / b);
    }

    public static bool operator ==(Cartesian2 a, Cartesian2 b)
    {
        return a.x == b.x && a.y == b.y;
    }

    public static bool operator !=(Cartesian2 a, Cartesian2 b)
    {
        return a.x != b.x || a.y != b.y;
    }

    public static Cartesian2 Zero
    {
        get
        {
            return new Cartesian2(0, 0);
        }
    }

    public override bool Equals(object obj)
    {
        if (obj is Cartesian2)
        {
            return this == (Cartesian2)obj;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return x.GetHashCode() ^ y.GetHashCode();
    }

    
}