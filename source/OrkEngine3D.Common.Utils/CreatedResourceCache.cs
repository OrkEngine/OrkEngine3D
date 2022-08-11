/*
    MIT License

Copyright (c) 2022 OrkEngine

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace OrkEngine3D.Common.Utils;

public static class CreatedResourceCache
{
    private static readonly Dictionary<object, object> s_cache = new Dictionary<object, object>();
    public static void ClearCache() => s_cache.Clear();

    public static bool TryGetCachedItem<TKey, TValue>(TKey key, out TValue value)
    {
        object fromCache;
        if (!s_cache.TryGetValue(key, out fromCache))
        {
            value = default(TValue);
            return false;
        }
        else
        {
            value = (TValue)fromCache;
            return true;
        }
    }

    public static void CacheItem<TKey, TValue>(TKey key, TValue value)
    {
        s_cache.Add(key, value);
    }
}
