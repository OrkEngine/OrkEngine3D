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

using System;
using System.Collections.Generic;
using System.Reflection;

namespace OrkEngine3D.Core;

public class TypeCache<T>
{
    private readonly Dictionary<Type, T> _items = new Dictionary<Type, T>();
    private readonly Dictionary<Type, T> _fallbackItems = new Dictionary<Type, T>();

    public void AddItem(Type t, T item)
    {
        _items.Add(t, item);
        _fallbackItems.Clear();
    }

    public T GetItem(Type type)
    {
        T d;
        if (!_items.TryGetValue(type, out d))
        {
            d = GetFallbackItem(type);
            if (d == null)
            {
                throw new InvalidOperationException($"No T compatible with {type.Name}");
            }
        }

        return d;
    }

    private T GetFallbackItem(Type type)
    {
        T item = default(T);
        if (_fallbackItems.TryGetValue(type, out item))
        {
            return item;
        }
        else
        {
            int hierarchyDistance = int.MaxValue;
            foreach (var kvp in _items)
            {
                if (kvp.Key.IsAssignableFrom(type))
                {
                    int newHD = GetHierarchyDistance(kvp.Key.GetTypeInfo(), type.GetTypeInfo());
                    if (newHD < hierarchyDistance)
                    {
                        hierarchyDistance = newHD;
                        item = kvp.Value;
                    }
                }
            }

            _fallbackItems.Add(type, item);
            return item;
        }
    }

    private int GetHierarchyDistance(TypeInfo baseType, TypeInfo derived)
    {
        int distance = 0;
        while ((derived = derived.BaseType?.GetTypeInfo()) != null)
        {
            distance++;
            if (derived == baseType)
            {
                return distance;
            }
        }

        throw new InvalidOperationException($"{baseType.Name} is not a superclass of {derived.Name}");
    }
}
