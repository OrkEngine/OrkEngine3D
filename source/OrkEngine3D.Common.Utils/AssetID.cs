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

public struct AssetID : IEquatable<AssetID>
{
    private string _value;
    public string Value { get { return _value; } set { _value = value; } }

    public bool IsEmpty => string.IsNullOrEmpty(_value);

    public AssetID(string value)
    {
        _value = value.Replace("\\", "/");
    }

    public bool Equals(AssetID other)
    {
        return other._value.Equals(_value);
    }

    public override bool Equals(object obj)
    {
        return obj is AssetID && ((AssetID)(obj)).Equals(this);
    }

    public override int GetHashCode()
    {
        return _value != null ? _value.GetHashCode() : 0;
    }

    public override string ToString()
    {
        return $"ID:{_value}";
    }

    public static implicit operator AssetID(string s) => new AssetID(s);
    public static implicit operator string(AssetID id) => id._value;
}
