using System;
using System.Threading.Tasks;

namespace Spp.Common.Miscellaneous;

public readonly struct AnyOf<T0, T1> : IAnyOf<T0, T1>
{
    private readonly byte _typeIndex;
    private readonly T0 _value0 = default!;
    private readonly T1 _value1 = default!;

    public AnyOf(T0 value)
    {
        _value0 = value;
        _typeIndex = 0;
    }

    public AnyOf(T1 value)
    {
        _value1 = value;
        _typeIndex = 1;
    }

    public TOut Switch<TOut>(
        Func<T0, TOut> case0,
        Func<T1, TOut> case1)
    {
        return _typeIndex switch
        {
            0 => case0(_value0),
            1 => case1(_value1),
            _ => throw new InvalidOperationException($"Invalid type index: {_typeIndex}.")
        };
    }

    public async Task<TOut> Switch<TOut>(
        Func<T0, Task<TOut>> case0,
        Func<T1, Task<TOut>> case1)
    {
        return _typeIndex switch
        {
            0 => await case0(_value0),
            1 => await case1(_value1),
            _ => throw new InvalidOperationException($"Invalid type index: {_typeIndex}.")
        };
    }

    public static implicit operator AnyOf<T0, T1>(T0 value)
    {
        return new AnyOf<T0, T1>(value);
    }

    public static implicit operator AnyOf<T0, T1>(T1 value)
    {
        return new AnyOf<T0, T1>(value);
    }
}

public readonly struct AnyOf<T0, T1, T2> : IAnyOf<T0, T1, T2>
{
    private readonly byte _typeIndex;
    private readonly T0 _value0 = default!;
    private readonly T1 _value1 = default!;
    private readonly T2 _value2 = default!;

    public AnyOf(T0 value)
    {
        _value0 = value;
        _typeIndex = 0;
    }

    public AnyOf(T1 value)
    {
        _value1 = value;
        _typeIndex = 1;
    }

    public AnyOf(T2 value)
    {
        _value2 = value;
        _typeIndex = 2;
    }

    public TOut Switch<TOut>(
        Func<T0, TOut> case0,
        Func<T1, TOut> case1,
        Func<T2, TOut> case2)
    {
        return _typeIndex switch
        {
            0 => case0(_value0),
            1 => case1(_value1),
            2 => case2(_value2),
            _ => throw new InvalidOperationException($"Invalid type index: {_typeIndex}.")
        };
    }

    public async Task<TOut> Switch<TOut>(
        Func<T0, Task<TOut>> case0,
        Func<T1, Task<TOut>> case1,
        Func<T2, Task<TOut>> case2)
    {
        return _typeIndex switch
        {
            0 => await case0(_value0),
            1 => await case1(_value1),
            2 => await case2(_value2),
            _ => throw new InvalidOperationException($"Invalid type index: {_typeIndex}.")
        };
    }

    public static implicit operator AnyOf<T0, T1, T2>(T0 value)
    {
        return new AnyOf<T0, T1, T2>(value);
    }

    public static implicit operator AnyOf<T0, T1, T2>(T1 value)
    {
        return new AnyOf<T0, T1, T2>(value);
    }

    public static implicit operator AnyOf<T0, T1, T2>(T2 value)
    {
        return new AnyOf<T0, T1, T2>(value);
    }
}

public readonly struct AnyOf<T0, T1, T2, T3> : IAnyOf<T0, T1, T2, T3>
{
    private readonly byte _typeIndex;
    private readonly T0 _value0 = default!;
    private readonly T1 _value1 = default!;
    private readonly T2 _value2 = default!;
    private readonly T3 _value3 = default!;

    public AnyOf(T0 value)
    {
        _value0 = value;
        _typeIndex = 0;
    }

    public AnyOf(T1 value)
    {
        _value1 = value;
        _typeIndex = 1;
    }

    public AnyOf(T2 value)
    {
        _value2 = value;
        _typeIndex = 2;
    }

    public AnyOf(T3 value)
    {
        _value3 = value;
        _typeIndex = 3;
    }

    public TOut Switch<TOut>(
        Func<T0, TOut> case0,
        Func<T1, TOut> case1,
        Func<T2, TOut> case2,
        Func<T3, TOut> case3)
    {
        return _typeIndex switch
        {
            0 => case0(_value0),
            1 => case1(_value1),
            2 => case2(_value2),
            3 => case3(_value3),
            _ => throw new InvalidOperationException($"Invalid type index: {_typeIndex}.")
        };
    }

    public async Task<TOut> Switch<TOut>(
        Func<T0, Task<TOut>> case0,
        Func<T1, Task<TOut>> case1,
        Func<T2, Task<TOut>> case2,
        Func<T3, Task<TOut>> case3)
    {
        return _typeIndex switch
        {
            0 => await case0(_value0),
            1 => await case1(_value1),
            2 => await case2(_value2),
            3 => await case3(_value3),
            _ => throw new InvalidOperationException($"Invalid type index: {_typeIndex}.")
        };
    }

    public static implicit operator AnyOf<T0, T1, T2, T3>(T0 value)
    {
        return new AnyOf<T0, T1, T2, T3>(value);
    }

    public static implicit operator AnyOf<T0, T1, T2, T3>(T1 value)
    {
        return new AnyOf<T0, T1, T2, T3>(value);
    }

    public static implicit operator AnyOf<T0, T1, T2, T3>(T2 value)
    {
        return new AnyOf<T0, T1, T2, T3>(value);
    }

    public static implicit operator AnyOf<T0, T1, T2, T3>(T3 value)
    {
        return new AnyOf<T0, T1, T2, T3>(value);
    }
}

public readonly struct AnyOf<T0, T1, T2, T3, T4> : IAnyOf<T0, T1, T2, T3, T4>
{
    private readonly byte _typeIndex;
    private readonly T0 _value0 = default!;
    private readonly T1 _value1 = default!;
    private readonly T2 _value2 = default!;
    private readonly T3 _value3 = default!;
    private readonly T4 _value4 = default!;

    public AnyOf(T0 value)
    {
        _value0 = value;
        _typeIndex = 0;
    }

    public AnyOf(T1 value)
    {
        _value1 = value;
        _typeIndex = 1;
    }

    public AnyOf(T2 value)
    {
        _value2 = value;
        _typeIndex = 2;
    }

    public AnyOf(T3 value)
    {
        _value3 = value;
        _typeIndex = 3;
    }

    public AnyOf(T4 value)
    {
        _value4 = value;
        _typeIndex = 4;
    }

    public TOut Switch<TOut>(
        Func<T0, TOut> case0,
        Func<T1, TOut> case1,
        Func<T2, TOut> case2,
        Func<T3, TOut> case3,
        Func<T4, TOut> case4)
    {
        return _typeIndex switch
        {
            0 => case0(_value0),
            1 => case1(_value1),
            2 => case2(_value2),
            3 => case3(_value3),
            4 => case4(_value4),
            _ => throw new InvalidOperationException($"Invalid type index: {_typeIndex}.")
        };
    }

    public async Task<TOut> Switch<TOut>(
        Func<T0, Task<TOut>> case0,
        Func<T1, Task<TOut>> case1,
        Func<T2, Task<TOut>> case2,
        Func<T3, Task<TOut>> case3,
        Func<T4, Task<TOut>> case4)
    {
        return _typeIndex switch
        {
            0 => await case0(_value0),
            1 => await case1(_value1),
            2 => await case2(_value2),
            3 => await case3(_value3),
            4 => await case4(_value4),
            _ => throw new InvalidOperationException($"Invalid type index: {_typeIndex}.")
        };
    }

    public static implicit operator AnyOf<T0, T1, T2, T3, T4>(T0 value)
    {
        return new AnyOf<T0, T1, T2, T3, T4>(value);
    }

    public static implicit operator AnyOf<T0, T1, T2, T3, T4>(T1 value)
    {
        return new AnyOf<T0, T1, T2, T3, T4>(value);
    }

    public static implicit operator AnyOf<T0, T1, T2, T3, T4>(T2 value)
    {
        return new AnyOf<T0, T1, T2, T3, T4>(value);
    }

    public static implicit operator AnyOf<T0, T1, T2, T3, T4>(T3 value)
    {
        return new AnyOf<T0, T1, T2, T3, T4>(value);
    }

    public static implicit operator AnyOf<T0, T1, T2, T3, T4>(T4 value)
    {
        return new AnyOf<T0, T1, T2, T3, T4>(value);
    }
}
