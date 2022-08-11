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

using Newtonsoft.Json;
using OrkEngine3D.Common.Utils;

namespace OrkEngine3D.Core;

public struct RefOrImmediate<T>
{
    [JsonProperty]
    private AssetRef<T> _ref;
    [JsonProperty]
    private T _value;

    public bool HasValue => _ref == null;

    public AssetRef<T> GetRef()
    {
        return _ref;
    }

    [JsonConstructor]
    public RefOrImmediate(AssetRef<T> reference, T value)
    {
        _ref = reference;
        _value = value;
    }

    public T Get(AssetDatabase ad)
    {
        if (_ref != null)
        {
            return ad.LoadAsset(_ref);
        }
        else
        {
            return _value;
        }
    }

    public static implicit operator RefOrImmediate<T>(T value) => new RefOrImmediate<T>(null, value);
    public static implicit operator RefOrImmediate<T>(AssetRef<T> reference) => new RefOrImmediate<T>(reference, default);
    public static implicit operator RefOrImmediate<T>(AssetID id) => new RefOrImmediate<T>(new AssetRef<T>(id), default);
    public static implicit operator RefOrImmediate<T>(string reference) => new RefOrImmediate<T>(new AssetRef<T>(reference), default);
}
