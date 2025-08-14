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
    }
    
    public static Interval<T> Order(Interval<T> a)
    {
        return a.IsOrder ? a : new Interval<T>(a.End, a.Start, a.ContainsStart,a.ContainsEnd);
    }
    
    public static bool Contains(Interval<T> interval, T value)
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
        return Interval<double>.Contains(interval, value);
    }
    
    public static bool Contains(this Interval<double> interval, double value)
    {
        return Interval<double>.Contains(interval, value);
    }
}





