using System;
using System.Threading.Tasks;

namespace Spp.Common.Miscellaneous;

public abstract class BaseAnyOf<T0, T1>(AnyOf<T0, T1> inner) : IAnyOf<T0, T1>
{
    public TOut Switch<TOut>(
        Func<T0, TOut> case0,
        Func<T1, TOut> case1)
    {
        return inner.Switch(case0, case1);
    }

    public Task<TOut> Switch<TOut>(
        Func<T0, Task<TOut>> case0,
        Func<T1, Task<TOut>> case1)
    {
        return inner.Switch(case0, case1);
    }
}

public abstract class BaseAnyOf<T0, T1, T2>(AnyOf<T0, T1, T2> inner) : IAnyOf<T0, T1, T2>
{
    public TOut Switch<TOut>(
        Func<T0, TOut> case0,
        Func<T1, TOut> case1,
        Func<T2, TOut> case2)
    {
        return inner.Switch(case0, case1, case2);
    }

    public Task<TOut> Switch<TOut>(
        Func<T0, Task<TOut>> case0,
        Func<T1, Task<TOut>> case1,
        Func<T2, Task<TOut>> case2)
    {
        return inner.Switch(case0, case1, case2);
    }
}

public abstract class BaseAnyOf<T0, T1, T2, T3>(AnyOf<T0, T1, T2, T3> inner) : IAnyOf<T0, T1, T2, T3>
{
    public TOut Switch<TOut>(
        Func<T0, TOut> case0,
        Func<T1, TOut> case1,
        Func<T2, TOut> case2,
        Func<T3, TOut> case3)
    {
        return inner.Switch(case0, case1, case2, case3);
    }

    public Task<TOut> Switch<TOut>(
        Func<T0, Task<TOut>> case0,
        Func<T1, Task<TOut>> case1,
        Func<T2, Task<TOut>> case2,
        Func<T3, Task<TOut>> case3)
    {
        return inner.Switch(case0, case1, case2, case3);
    }
}

public abstract class BaseAnyOf<T0, T1, T2, T3, T4>(AnyOf<T0, T1, T2, T3, T4> inner) : IAnyOf<T0, T1, T2, T3, T4>
{
    public TOut Switch<TOut>(
        Func<T0, TOut> case0,
        Func<T1, TOut> case1,
        Func<T2, TOut> case2,
        Func<T3, TOut> case3,
        Func<T4, TOut> case4)
    {
        return inner.Switch(case0, case1, case2, case3, case4);
    }

    public Task<TOut> Switch<TOut>(
        Func<T0, Task<TOut>> case0,
        Func<T1, Task<TOut>> case1,
        Func<T2, Task<TOut>> case2,
        Func<T3, Task<TOut>> case3,
        Func<T4, Task<TOut>> case4)
    {
        return inner.Switch(case0, case1, case2, case3, case4);
    }
}
