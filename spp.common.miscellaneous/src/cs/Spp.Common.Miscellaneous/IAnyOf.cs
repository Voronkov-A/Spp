using System;
using System.Threading.Tasks;

namespace Spp.Common.Miscellaneous;

public interface IAnyOf<T0, T1>
{
    TOut Switch<TOut>(
        Func<T0, TOut> case0,
        Func<T1, TOut> case1);

    Task<TOut> Switch<TOut>(
        Func<T0, Task<TOut>> case0,
        Func<T1, Task<TOut>> case1);
}

public interface IAnyOf<T0, T1, T2>
{
    TOut Switch<TOut>(
        Func<T0, TOut> case0,
        Func<T1, TOut> case1,
        Func<T2, TOut> case2);

    Task<TOut> Switch<TOut>(
        Func<T0, Task<TOut>> case0,
        Func<T1, Task<TOut>> case1,
        Func<T2, Task<TOut>> case2);
}

public interface IAnyOf<T0, T1, T2, T3>
{
    TOut Switch<TOut>(
        Func<T0, TOut> case0,
        Func<T1, TOut> case1,
        Func<T2, TOut> case2,
        Func<T3, TOut> case3);

    Task<TOut> Switch<TOut>(
        Func<T0, Task<TOut>> case0,
        Func<T1, Task<TOut>> case1,
        Func<T2, Task<TOut>> case2,
        Func<T3, Task<TOut>> case3);
}

public interface IAnyOf<T0, T1, T2, T3, T4>
{
    TOut Switch<TOut>(
        Func<T0, TOut> case0,
        Func<T1, TOut> case1,
        Func<T2, TOut> case2,
        Func<T3, TOut> case3,
        Func<T4, TOut> case4);

    Task<TOut> Switch<TOut>(
        Func<T0, Task<TOut>> case0,
        Func<T1, Task<TOut>> case1,
        Func<T2, Task<TOut>> case2,
        Func<T3, Task<TOut>> case3,
        Func<T4, Task<TOut>> case4);
}
