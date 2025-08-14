using System.Collections;
using System.Numerics;
using System.Linq;

namespace IntervalLib;

public class IntervalArray<T> : IEnumerable<Interval<T>> where T : INumber<T>
{
    private readonly IEnumerable<T> _list;
    private readonly IEnumerable<bool> _contains;

    private List<Interval<T>> _intervalArray = [];

    public IntervalArray(IEnumerable<T> array, bool contain = true)
    {
        _list = array;
        _contains = Enumerable.Repeat(contain, _list.Count());
    }
    
    public IntervalArray(IEnumerable<Interval<T>> intervalArray, bool Union = true)
    {
        return;
        //_list = array;
        //_contains = Enumerable.Repeat(contain, _list.Count());
    }

    public bool IsValid()
    {
        var isSort = _list.Order().SequenceEqual(_list);
        var isEven = _list.Count()%2 == 0;
        var isContain = _contains.Count() == _list.Count();
        return isSort && isEven && isContain;
    }

    public void CreateIntervalArray()
    {
        if (!IsValid())
        {
            throw new Exception("非法的区间范围");
        }
        List<Interval<T>> list = [];
        for (var i = 0; i < _list.Count(); i += 2)
        {
            list.Add(new Interval<T>(_list.ElementAt(i), _list.ElementAt(i + 1), _contains.ElementAt(i), _contains.ElementAt(i + 1)));
        }
    }
    
    IEnumerator<Interval<T>> IEnumerable<Interval<T>>.GetEnumerator()
    {
       return _intervalArray.GetEnumerator();
    }

    public IEnumerator GetEnumerator()
    {
        return _intervalArray.GetEnumerator();
    }
}