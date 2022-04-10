using System.Collections;

namespace GBS_Essential_Core.Extensions;

public static class Ext
{
    public static void ForEach<T>(this IEnumerable<T> x, Action<T> action)
    {
        foreach (var k in x) action(k);
    }

    public static void ForEach<T>(this IEnumerable<T> x, Action<T, long> action)
    {
        long idx = 0;
        foreach (var k in x) action(k, idx++);
    }
}