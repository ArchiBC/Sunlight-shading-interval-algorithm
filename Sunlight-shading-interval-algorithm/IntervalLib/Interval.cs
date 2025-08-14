using System.Numerics;

namespace IntervalLib;

public struct Interval<T>(T start, T end, bool containsStart = true, bool containsEnd = true) where T: INumber<T>
{
    private T _start = start;
    private T _end = end;
    private bool _containsStart = containsStart;
    private bool _containsEnd = containsEnd;

    public T Start => _start;

    public T End => _end;

    public bool ContainsStart => _containsStart;
    
    public bool ContainsEnd => _containsEnd;
    
    public bool IsOrder => Start < End;

    public void Order()
    {
        if (IsOrder) return;
        Reverse();
    }
    public void Reverse()
    {
        (_start, _end) = (_end, _start);
        (_containsStart, _containsEnd) = (_containsEnd, _containsStart);
    }
    
    public static Interval<T> Order(Interval<T> a)
    {
        return a.IsOrder ? a : new Interval<T>(a.End, a.Start, a.ContainsStart,a.ContainsEnd);
    }
    
    public static bool IsContains(Interval<T> interval, T value)
    {
        var order = Order(interval);
        if (value < order.Start || value > order.End ) return false;
        if (value > order.Start && value < order.End) return true;
        if (value == order.Start && order.ContainsStart) return true;
        if (value == order.End && order.ContainsEnd) return true;
        return false;
    }
    
    public static Interval<T> Reverse(Interval<T> interval)
    {
        return new Interval<T>(interval.End, interval.Start);
    }
    public static bool IsConnect (Interval<T> a, Interval<T> b)
    {
        var orderA = Order(a);
        var orderB = Order(b);
        var containsA = IsContains(orderA, orderB.Start) || IsContains(orderA, orderB.End);
        var containsB = IsContains(orderB, orderA.Start) || IsContains(orderB, orderA.End);
        return containsA || containsB;
    }
    
    public static Interval<T> Union (Interval<T> a, Interval<T> b)
    {
        var orderA = Order(a);
        var orderB = Order(b);
        if (!IsConnect(orderA, orderB))
        {
            throw new Exception("区间未连接，无法合并");
        }
        
        return new Interval<T>(
            orderA.Start<orderB.Start ? orderA.Start : orderB.Start, 
            orderB.End<orderA.End ? orderA.End : orderB.End,
            orderA.Start<orderB.Start ? orderA.ContainsStart : orderB.ContainsStart,
            orderB.End<orderA.End ? orderA.ContainsEnd : orderB.ContainsEnd
            );
    }
    
    public static Interval<T> Intersect (Interval<T> a, Interval<T> b)
    {
        var orderA = Order(a);
        var orderB = Order(b);
        if (!IsConnect(orderA, orderB))
        {
            throw new Exception("区间未连接，无法取交集");
        }

        return new Interval<T>(
            orderA.Start > orderB.Start ? orderA.Start : orderB.Start,
            orderB.End > orderA.End ? orderA.End : orderB.End,
            orderA.Start > orderB.Start ? orderA.ContainsStart : orderB.ContainsStart,
            orderB.End > orderA.End ? orderA.ContainsEnd : orderB.ContainsEnd
        );
    }
    
    
    public static Interval<T> operator +(Interval<T> interval, T value)
    {
        return new Interval<T>(interval.Start + value, interval.End + value);
    }
    
    public static Interval<T> operator +(Interval<T> a, Interval<T> b)
    {
        return new Interval<T>(a.Start + a.Start, b.End + b.End);
    }
    
    public static Interval<T> operator -(Interval<T> interval, T value)
    {
        return new Interval<T>(interval.Start - value, interval.End - value);
    }
    
    public static Interval<T> operator -(Interval<T> a, Interval<T> b)
    {
        return new Interval<T>(a.Start - a.Start, b.End - b.End);
    }
    
    public static Interval<T> operator *(Interval<T> interval, T value)
    {
        return new Interval<T>(interval.Start * value, interval.End * value);
    }
    
    public static Interval<T> operator /(Interval<T> interval, T value)
    {
        return new Interval<T>(interval.Start / value, interval.End / value);
    }
}

public static class IntervalCal
{ 
    public static bool Contains(this double value, Interval<double> interval)
    {
        return Interval<double>.IsContains(interval, value);
    }
    
    public static bool Contains(this Interval<double> interval, double value)
    {
        return Interval<double>.IsContains(interval, value);
    }
    
    public static Interval<double> Union(this Interval<double> a, Interval<double> b)
    {
        return Interval<double>.Union(a, b);
    }
    
    public static Interval<double> Intersect(this Interval<double> a, Interval<double> b)
    {
        return Interval<double>.Intersect(a, b);
    }
    
    public static bool IsConnect(this Interval<double> a, Interval<double> b)
    {
        return Interval<double>.IsConnect(a, b);
    }
    
    public static Interval<double> Reverse(this Interval<double> interval)
    {
        return Interval<double>.Reverse(interval);
    }
    
    public static Interval<double> Order(this Interval<double> interval)
    {
        return Interval<double>.Order(interval);
    }
    
    
}





